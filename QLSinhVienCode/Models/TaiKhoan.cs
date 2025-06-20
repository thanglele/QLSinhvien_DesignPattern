using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSinhVienCode.Models
{
    [Table("TaiKhoan")]
    public class TaiKhoan
    {
        [Key]
        [Column("TenDangNhap")]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [Column("MatKhau")]
        [StringLength(100)]
        public string MatKhau { get; set; }

        [Required]
        [Column("VaiTro")]
        [StringLength(20)]
        public string VaiTro { get; set; }
    }
}