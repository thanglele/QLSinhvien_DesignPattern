using System;
using System.Collections.Generic;

namespace QLSinhVienCode.Models;

public partial class Khoa
{
    public string MaKhoa { get; set; } = null!;

    public string TenKhoa { get; set; } = null!;

    public virtual ICollection<MonHoc> MonHocs { get; set; } = new List<MonHoc>();
}
