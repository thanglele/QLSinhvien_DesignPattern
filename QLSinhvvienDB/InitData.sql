INSERT INTO TaiKhoan VALUES ('admin', '123456', 'Admin');

INSERT INTO Khoa VALUES ('CNTT', N'C�ng ngh? th�ng tin');

INSERT INTO GiangVien (MaGV, HoTen, BoMon, Email, SoDienThoai, TenDangNhap)
VALUES ('GV001', N'Tr?n V?n A', N'C�ng ngh? ph?n m?m', 'gv001@tlu.edu.vn', '0123456789', NULL);

INSERT INTO MonHoc (MaMon, TenMon, SoTinChi, HocKy, LoaiMonHoc, SoTiet, MaKhoa, MaGV)
VALUES ('MH001', N'C?u tr�c d? li?u', 3, 2, N'B?t bu?c', 45, 'CNTT', 'GV001');
