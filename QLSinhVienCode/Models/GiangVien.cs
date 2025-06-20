using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSinhVienCode.Models
{
    [Table("GiangVien")]
    public class GiangVien
    {
        [Key]
        [Column("MaGV")]
        [StringLength(20)]
        public string MaGV { get; set; }

        [Required]
        [Column("HoTen")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Column("BoMon")]
        [StringLength(100)]
        public string? BoMon { get; set; }

        [Column("Email")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("SoDienThoai")]
        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [Column("TenDangNhap")]
        [StringLength(50)]
        public string? TenDangNhap { get; set; }

        [ForeignKey("TenDangNhap")]
        public virtual TaiKhoan? TaiKhoan { get; set; }
    }
}