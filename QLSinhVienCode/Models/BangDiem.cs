using System;
using System.Collections.Generic;

namespace QLSinhVienCode.Models;

public partial class BangDiem
{
    public int Id { get; set; }

    public string? MaSv { get; set; }

    public string? MaMon { get; set; }

    public double? Diem { get; set; }

    public int? LanThi { get; set; }

    public DateOnly? NgayNhap { get; set; }

    public virtual MonHoc? MaMonNavigation { get; set; }

    public virtual SinhVien? MaSvNavigation { get; set; }
}
