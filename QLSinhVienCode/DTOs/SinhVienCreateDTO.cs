namespace QLSinhVienCode.DTOs
{
    public class SinhVienCreateDTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string MatKhau { get; set; } // Mật khẩu cho tài khoản mới
        public DateTime? NgaySinh { get; set; }
        public string? Lop { get; set; }
        public string? Nganh { get; set; }
        public string? Email { get; set; }
    }
}
