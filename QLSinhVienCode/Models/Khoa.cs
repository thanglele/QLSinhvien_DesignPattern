using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLSinhVienCode.Models
{
    [Table("Khoa")]
    public class Khoa
    {
        [Key]
        [Column("MaKhoa")]
        [StringLength(10)]
        public string MaKhoa { get; set; }

        [Required]
        [Column("TenKhoa")]
        [StringLength(100)]
        public string TenKhoa { get; set; }
    }
}