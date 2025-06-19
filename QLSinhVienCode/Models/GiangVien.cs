using System;
using System.Collections.Generic;

namespace QLSinhVienCode.Models;

public partial class GiangVien
{
    public string MaGv { get; set; } = null!;

    public string HoTen { get; set; } = null!;

    public string? BoMon { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? TenDangNhap { get; set; }

    public virtual ICollection<MonHoc> MonHocs { get; set; } = new List<MonHoc>();

    public virtual TaiKhoan? TenDangNhapNavigation { get; set; }
}
