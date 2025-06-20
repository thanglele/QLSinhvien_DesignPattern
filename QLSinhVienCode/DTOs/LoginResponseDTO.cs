namespace QLSinhVienCode.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string VaiTro { get; set; }
        public string HoTen { get; set; }
        public string ActorId { get; set; } // Sẽ là MaSV hoặc MaGV
    }
}
