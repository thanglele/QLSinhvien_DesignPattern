using System;
using System.Collections.Generic;

namespace QLSinhVienCode.Models;

public partial class SinhVien
{
    public string MaSv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? DiaChi { get; set; }

    public string? Lop { get; set; }

    public string? Nganh { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? TenDangNhap { get; set; }

    public virtual ICollection<BangDiem> BangDiems { get; set; } = new List<BangDiem>();

    public virtual TaiKhoan? TenDangNhapNavigation { get; set; }
}
