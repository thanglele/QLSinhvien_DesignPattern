using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSinhVienCode.Models
{
    [Table("MonHoc")]
    public class MonHoc
    {
        [Key]
        [Column("MaMon")]
        [StringLength(20)]
        public string MaMon { get; set; }

        [Required]
        [Column("TenMon")]
        [StringLength(100)]
        public string TenMon { get; set; }

        [Column("SoTinChi")]
        public int? SoTinChi { get; set; }

        [Column("HocKy")]
        public int? HocKy { get; set; }

        [Column("LoaiMonHoc")]
        [StringLength(50)]
        public string? LoaiMonHoc { get; set; }

        [Column("SoTiet")]
        public int? SoTiet { get; set; }

        [Column("GhiChu")]
        [StringLength(255)]
        public string? GhiChu { get; set; }

        [Column("MaKhoa")]
        [StringLength(10)]
        public string? MaKhoa { get; set; }

        [Column("MaGV")]
        [StringLength(20)]
        public string? MaGV { get; set; }

        [ForeignKey("MaKhoa")]
        public virtual Khoa? Khoa { get; set; }

        [ForeignKey("MaGV")]
        public virtual GiangVien? GiangVien { get; set; }
    }
}