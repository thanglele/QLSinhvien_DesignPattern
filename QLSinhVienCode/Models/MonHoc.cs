using System;
using System.Collections.Generic;

namespace QLSinhVienCode.Models;

public partial class MonHoc
{
    public string MaMon { get; set; } = null!;

    public string TenMon { get; set; } = null!;

    public int? SoTinChi { get; set; }

    public int? HocKy { get; set; }

    public string? LoaiMonHoc { get; set; }

    public int? SoTiet { get; set; }

    public string? GhiChu { get; set; }

    public string? MaKhoa { get; set; }

    public string? MaGv { get; set; }

    public virtual ICollection<BangDiem> BangDiems { get; set; } = new List<BangDiem>();

    public virtual GiangVien? MaGvNavigation { get; set; }

    public virtual Khoa? MaKhoaNavigation { get; set; }
}
