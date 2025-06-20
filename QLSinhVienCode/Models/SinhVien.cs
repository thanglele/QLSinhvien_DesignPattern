using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSinhVienCode.Models
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [Column("MaSV")]
        [StringLength(20)]
        public string MaSV { get; set; }

        [Required]
        [Column("HoTen")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        [Column("GioiTinh")]
        [StringLength(10)]
        public string? GioiTinh { get; set; }

        [Column("DiaChi")]
        [StringLength(225)]
        public string? DiaChi { get; set; }

        [Column("Lop")]
        [StringLength(20)]
        public string? Lop { get; set; }

        [Column("Nganh")]
        [StringLength(100)]
        public string? Nganh { get; set; }

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

        public virtual ICollection<BangDiem> BangDiems { get; set; } = new List<BangDiem>();
    }
}