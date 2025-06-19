using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QLSinhVienCode.Models;

namespace QLSinhVienCode.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BangDiem> BangDiems { get; set; }

        public virtual DbSet<GiangVien> GiangViens { get; set; }

        public virtual DbSet<Khoa> Khoas { get; set; }

        public virtual DbSet<MonHoc> MonHocs { get; set; }

        public virtual DbSet<SinhVien> SinhViens { get; set; }

        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangDiem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__BangDiem__3214EC27E82FCB33");

                entity.ToTable("BangDiem");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.LanThi).HasDefaultValue(1);
                entity.Property(e => e.MaMon)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.MaSv)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MaSV");
                entity.Property(e => e.NgayNhap).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MaMonNavigation).WithMany(p => p.BangDiems)
                    .HasForeignKey(d => d.MaMon)
                    .HasConstraintName("FK__BangDiem__MaMon__4AB81AF0");

                entity.HasOne(d => d.MaSvNavigation).WithMany(p => p.BangDiems)
                    .HasForeignKey(d => d.MaSv)
                    .HasConstraintName("FK__BangDiem__MaSV__49C3F6B7");
            });

            modelBuilder.Entity<GiangVien>(entity =>
            {
                entity.HasKey(e => e.MaGv).HasName("PK__GiangVie__2725AEF393762EA7");

                entity.ToTable("GiangVien");

                entity.Property(e => e.MaGv)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MaGV");
                entity.Property(e => e.BoMon).HasMaxLength(100);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.HoTen).HasMaxLength(100);
                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.GiangViens)
                    .HasForeignKey(d => d.TenDangNhap)
                    .HasConstraintName("FK__GiangVien__TenDa__3D5E1FD2");
            });

            modelBuilder.Entity<Khoa>(entity =>
            {
                entity.HasKey(e => e.MaKhoa).HasName("PK__Khoa__653904055E3A5AD5");

                entity.ToTable("Khoa");

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.TenKhoa).HasMaxLength(100);
            });

            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.MaMon).HasName("PK__MonHoc__3A5B29A83DDA16AD");

                entity.ToTable("MonHoc");

                entity.Property(e => e.MaMon)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.GhiChu).HasMaxLength(255);
                entity.Property(e => e.LoaiMonHoc).HasMaxLength(50);
                entity.Property(e => e.MaGv)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MaGV");
                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.TenMon).HasMaxLength(100);

                entity.HasOne(d => d.MaGvNavigation).WithMany(p => p.MonHocs)
                    .HasForeignKey(d => d.MaGv)
                    .HasConstraintName("FK__MonHoc__MaGV__440B1D61");

                entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.MonHocs)
                    .HasForeignKey(d => d.MaKhoa)
                    .HasConstraintName("FK__MonHoc__MaKhoa__4316F928");
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => e.MaSv).HasName("PK__SinhVien__2725081AF2EA654A");

                entity.ToTable("SinhVien");

                entity.Property(e => e.MaSv)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MaSV");
                entity.Property(e => e.DiaChi).HasMaxLength(225);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.GioiTinh).HasMaxLength(10);
                entity.Property(e => e.HoTen).HasMaxLength(100);
                entity.Property(e => e.Lop).HasMaxLength(20);
                entity.Property(e => e.Nganh).HasMaxLength(100);
                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TenDangNhapNavigation).WithMany(p => p.SinhViens)
                    .HasForeignKey(d => d.TenDangNhap)
                    .HasConstraintName("FK__SinhVien__TenDan__3A81B327");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.TenDangNhap).HasName("PK__TaiKhoan__55F68FC1D6C7576C");

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.MatKhau)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.VaiTro)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}