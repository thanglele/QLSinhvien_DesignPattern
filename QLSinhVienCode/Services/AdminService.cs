using QLSinhVienCode.DTOs;
using QLSinhVienCode.Models;
using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Services
{
    // --- Service cho các nghiệp vụ của Admin ---
    public interface IAdminService
    {
        Task<SinhVien> CreateSinhVienWithAccountAsync(SinhVienCreateDTO dto);
        Task<bool> UpdateSinhVienAsync(string maSV, SinhVienUpdateDTO dto);
        Task<bool> DeleteSinhVienAsync(string maSV);
        Task<GiangVien> CreateGiangVienWithAccountAsync(GiangVienCreateDTO dto);
        Task<bool> UpdateGiangVienAsync(string maGV, GiangVienUpdateDTO dto);
        Task<bool> DeleteGiangVienAsync(string maGV);
    }
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        // --- Sinh Viên ---
        public async Task<SinhVien> CreateSinhVienWithAccountAsync(SinhVienCreateDTO dto)
        {
            await _unitOfWork.TaiKhoans.AddAsync(new TaiKhoan { TenDangNhap = dto.MaSV, MatKhau = dto.MatKhau, VaiTro = "SinhVien" });
            var sinhVien = new SinhVien { MaSV = dto.MaSV, HoTen = dto.HoTen, NgaySinh = dto.NgaySinh, Lop = dto.Lop, Nganh = dto.Nganh, Email = dto.Email, TenDangNhap = dto.MaSV };
            await _unitOfWork.SinhViens.AddAsync(sinhVien);
            await _unitOfWork.CompleteAsync();
            return sinhVien;
        }
        public async Task<bool> UpdateSinhVienAsync(string maSV, SinhVienUpdateDTO dto)
        {
            var sv = await _unitOfWork.SinhViens.GetByIdAsync(maSV);
            if (sv == null) return false;
            sv.HoTen = dto.HoTen; sv.NgaySinh = dto.NgaySinh; sv.Lop = dto.Lop; sv.Nganh = dto.Nganh; sv.Email = dto.Email;
            _unitOfWork.SinhViens.Update(sv);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteSinhVienAsync(string maSV)
        {
            var sv = await _unitOfWork.SinhViens.GetByIdAsync(maSV);
            if (sv == null) return false;
            var tk = await _unitOfWork.TaiKhoans.GetByIdAsync(sv.TenDangNhap);
            _unitOfWork.SinhViens.Delete(sv);
            if (tk != null) _unitOfWork.TaiKhoans.Delete(tk);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // --- Giảng Viên ---
        public async Task<GiangVien> CreateGiangVienWithAccountAsync(GiangVienCreateDTO dto)
        {
            await _unitOfWork.TaiKhoans.AddAsync(new TaiKhoan { TenDangNhap = dto.MaGV, MatKhau = dto.MatKhau, VaiTro = "GiangVien" });
            var gv = new GiangVien { MaGV = dto.MaGV, HoTen = dto.HoTen, BoMon = dto.BoMon, Email = dto.Email, TenDangNhap = dto.MaGV };
            await _unitOfWork.GiangViens.AddAsync(gv);
            await _unitOfWork.CompleteAsync();
            return gv;
        }
        public async Task<bool> UpdateGiangVienAsync(string maGV, GiangVienUpdateDTO dto)
        {
            var gv = await _unitOfWork.GiangViens.GetByIdAsync(maGV);
            if (gv == null) return false;
            gv.HoTen = dto.HoTen; gv.BoMon = dto.BoMon; gv.Email = dto.Email;
            _unitOfWork.GiangViens.Update(gv);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteGiangVienAsync(string maGV)
        {
            var gv = await _unitOfWork.GiangViens.GetByIdAsync(maGV);
            if (gv == null) return false;
            var tk = await _unitOfWork.TaiKhoans.GetByIdAsync(gv.TenDangNhap);
            _unitOfWork.GiangViens.Delete(gv);
            if (tk != null) _unitOfWork.TaiKhoans.Delete(tk);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
