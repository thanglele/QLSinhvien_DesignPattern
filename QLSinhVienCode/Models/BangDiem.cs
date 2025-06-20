using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSinhVienCode.Models
{
    [Table("BangDiem")]
    public class BangDiem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }

        [Required]
        [Column("MaSV")]
        [StringLength(20)]
        public string MaSV { get; set; }

        [Required]
        [Column("MaMon")]
        [StringLength(20)]
        public string MaMon { get; set; }

        [Column("Diem")]
        public double? Diem { get; set; }

        [Column("LanThi")]
        public int? LanThi { get; set; }

        [Column("NgayNhap")]
        public DateTime? NgayNhap { get; set; }

        [ForeignKey("MaSV")]
        public virtual SinhVien? SinhVien { get; set; }

        [ForeignKey("MaMon")]
        public virtual MonHoc? MonHoc { get; set; }
    }
}