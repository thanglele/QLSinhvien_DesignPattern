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
            // Dòng này đọc section "Jwt" từ appsettings.json và đăng ký JwtSettings với DI container
            // ĐÃ SỬA TỪ "JwtSettings" THÀNH "Jwt" ĐỂ KHỚP VỚI appsettings.json
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            // Cấu hình DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký các lớp nghiệp vụ và Design Patterns
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISinhVienService, SinhVienService>();
            builder.Services.AddScoped<IGiangVienService, GiangVienService>();
            builder.Services.AddScoped<IDiemService, DiemService>();
            builder.Services.AddScoped<IAuthService, AuthService>(); // AuthService sẽ inject IOptions<JwtSettings>

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

                // Thêm bước kiểm tra để đảm bảo cấu hình được tải đúng
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

                options.Events = new JwtBearerEvents
                {
                    // Log khi nhận token
                    OnMessageReceived = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                        if (string.IsNullOrEmpty(token))
                        {
                            logger.LogWarning("No token found in Authorization header.");
                        }
                        else
                        {
                            logger.LogInformation($"Token received: {token}");
                        }

                        return Task.CompletedTask;
                    },

                    // Log khi xác thực thất bại
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                        logger.LogError("Authentication failed.");
                        logger.LogError($"Error: {context.Exception.Message}");

                        return Task.CompletedTask;
                    }
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
