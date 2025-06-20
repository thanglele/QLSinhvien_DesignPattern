using Microsoft.AspNetCore.Authentication.JwtBearer; // <-- ĐÃ THÊM USING NÀY
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; // <-- ĐÃ THÊM USING NÀY
using QLSinhVienCode.Data;
using QLSinhVienCode.Patterns.Facade;
using QLSinhVienCode.Patterns.Observer;
using QLSinhVienCode.Patterns.Singleton;
using QLSinhVienCode.Repositories;
using QLSinhVienCode.Services;
using QLSinhVienCode.Settings;
using System.Text; // <-- ĐÃ THÊM USING NÀY

namespace QLSinhVienCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -- Cấu hình JWT Settings bằng IOptions pattern --
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            // Cấu hình DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký các lớp nghiệp vụ và Design Patterns
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // --- ĐĂNG KÝ CÁC SERVICES ---
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ISinhVienService, SinhVienService>();
            builder.Services.AddScoped<IGiangVienService, GiangVienService>();
            builder.Services.AddScoped<IDiemService, DiemService>();
            builder.Services.AddScoped<IAdminService, AdminService>(); // <-- DÒNG CÒN THIẾU ĐÃ ĐƯỢC BỔ SUNG

            // --- ĐĂNG KÝ CÁC PATTERNS ---
            builder.Services.AddSingleton<FileLogger>();
            builder.Services.AddScoped<IAdmissionFacade, AdmissionFacade>();
            builder.Services.AddSingleton<IGradeObserver, AuditLogObserver>();
            builder.Services.AddSingleton<IGradeObserver, NotificationObserver>();
            builder.Services.AddScoped<GradeChangeNotifier>();

            // -- Cấu hình JWT Authentication --
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

                if (string.IsNullOrEmpty(jwtSettings?.Key))
                {
                    throw new InvalidOperationException("JWT Key is not configured in appsettings.json under the 'Jwt' section.");
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Các middleware này phải được đặt đúng thứ tự
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapControllers();
            app.Run();
        }
    }
}
