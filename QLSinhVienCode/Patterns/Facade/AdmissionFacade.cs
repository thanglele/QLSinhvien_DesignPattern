// PATTERN 8: Facade
// File: Patterns/Facade/AdmissionFacade.cs

using QLSinhVienCode.Models;
using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Patterns.Facade
{
    // Facade: Đơn giản hóa nghiệp vụ nhập học.
    public interface IAdmissionFacade { Task<bool> AdmitStudentAsync(string maSV, string hoTen, string tenDangNhap, string matKhau); }
    public class AdmissionFacade : IAdmissionFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdmissionFacade(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<bool> AdmitStudentAsync(string maSV, string hoTen, string tenDangNhap, string matKhau)
        {
            // 1. Tạo tài khoản
            var taiKhoan = new TaiKhoan { TenDangNhap = tenDangNhap, MatKhau = matKhau, VaiTro = "SinhVien" };
            await _unitOfWork.TaiKhoans.AddAsync(taiKhoan);

            // 2. Tạo sinh viên
            var sinhVien = new SinhVien { MaSV = maSV, HoTen = hoTen, TenDangNhap = tenDangNhap };
            await _unitOfWork.SinhViens.AddAsync(sinhVien);

            // 3. Lưu tất cả trong 1 transaction
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}