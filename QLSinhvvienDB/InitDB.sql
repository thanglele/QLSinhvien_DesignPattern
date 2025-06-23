-- 1. T?o SQL Login trên server
CREATE LOGIN application WITH PASSWORD = '@pplication12345@@';

-- 2. Gán quy?n sysadmin (toàn quy?n trên server)
ALTER SERVER ROLE sysadmin ADD MEMBER application;


--CREATE DATABASE QLSinhVien
Use QLSinhVien

-- B?ng tài kho?n
CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(100) NOT NULL,
    VaiTro VARCHAR(20) CHECK (VaiTro IN ('Admin', 'GiangVien', 'SinhVien')) NOT NULL
);
-- B?ng sinh viên
CREATE TABLE SinhVien (
    MaSV VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
	DiaChi NVARCHAR(225),
    Lop NVARCHAR(20),
    Nganh NVARCHAR(100),
    Email VARCHAR(100),
    SoDienThoai VARCHAR(15),
    TenDangNhap VARCHAR(50),
    FOREIGN KEY (TenDangNhap) REFERENCES TaiKhoan(TenDangNhap)
);
-- B?ng gi?ng viên
CREATE TABLE GiangVien (
    MaGV VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    BoMon NVARCHAR(100),
    Email VARCHAR(100),
    SoDienThoai VARCHAR(15),
    TenDangNhap VARCHAR(50),
    FOREIGN KEY (TenDangNhap) REFERENCES TaiKhoan(TenDangNhap)
);
-- B?ng khoa
CREATE TABLE Khoa (
    MaKhoa VARCHAR(10) PRIMARY KEY,
    TenKhoa NVARCHAR(100) NOT NULL
);
-- B?ng môn h?c
CREATE TABLE MonHoc (
    MaMon VARCHAR(20) PRIMARY KEY,
    TenMon NVARCHAR(100) NOT NULL,
    SoTinChi INT CHECK (SoTinChi > 0),
    HocKy INT,
    LoaiMonHoc NVARCHAR(50),
    SoTiet INT,
    GhiChu NVARCHAR(255),
    MaKhoa VARCHAR(10),
    MaGV VARCHAR(20),
    FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa),
    FOREIGN KEY (MaGV) REFERENCES GiangVien(MaGV)
);
-- B?ng ?i?m
CREATE TABLE BangDiem (
    ID INT IDENTITY PRIMARY KEY,
    MaSV VARCHAR(20),
    MaMon VARCHAR(20),
    Diem FLOAT CHECK (Diem >= 0 AND Diem <= 10),
    LanThi INT DEFAULT 1,
    NgayNhap DATE DEFAULT GETDATE(),
    FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    FOREIGN KEY (MaMon) REFERENCES MonHoc(MaMon)
);
