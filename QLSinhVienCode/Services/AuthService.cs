using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Repositories;
using QLSinhVienCode.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QLSinhVienCode.Services
{
    public interface IAuthService { Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request); }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtSettings _jwtSettings;

        // Sửa constructor để inject IOptions<JwtSettings>
        // Đây là cách làm tiêu chuẩn và sẽ sửa lỗi DI của bạn
        public AuthService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettingsOptions)
        {
            _unitOfWork = unitOfWork;
            _jwtSettings = jwtSettingsOptions.Value; // Lấy đối tượng cấu hình từ thuộc tính .Value
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            var taiKhoan = await _unitOfWork.TaiKhoans.GetByIdAsync(request.TenDangNhap);
            if (taiKhoan == null || taiKhoan.MatKhau != request.MatKhau) return null;

            string actorId = taiKhoan.TenDangNhap;
            string hoTen = "Admin";

            if (taiKhoan.VaiTro == "SinhVien")
            {
                var sv = (await _unitOfWork.SinhViens.FindAsync(s => s.TenDangNhap == taiKhoan.TenDangNhap)).FirstOrDefault();
                if (sv != null) { hoTen = sv.HoTen; actorId = sv.MaSV; }
            }
            else if (taiKhoan.VaiTro == "GiangVien")
            {
                var gv = (await _unitOfWork.GiangViens.FindAsync(g => g.TenDangNhap == taiKhoan.TenDangNhap)).FirstOrDefault();
                if (gv != null) { hoTen = gv.HoTen; actorId = gv.MaGV; }
            }

            var token = GenerateJwtToken(taiKhoan.TenDangNhap, taiKhoan.VaiTro, actorId);

            return new LoginResponseDTO { Token = token, VaiTro = taiKhoan.VaiTro, HoTen = hoTen, ActorId = actorId };
        }

        private string GenerateJwtToken(string tenDangNhap, string vaiTro, string actorId)
        {
            if (string.IsNullOrEmpty(_jwtSettings.Key))
            {
                throw new InvalidOperationException("JWT Key không được cấu hình.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, tenDangNhap),
                new Claim(ClaimTypes.Role, vaiTro),
                new Claim("actorId", actorId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
