using QLSinhVienCode.DTOs;
using QLSinhVienCode.Models;
using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Services
{
    // --- Service cho các nghiệp vụ của Giảng Viên ---
    public interface IGiangVienService
    {
        Task<bool> UpdateSinhVienAsGVAsync(string maSV, SinhVienUpdateDTO dto);
        Task<BangDiem> NhapOrUpdateDiemAsync(DiemDTO dto);
    }
    public class GiangVienService : IGiangVienService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GiangVienService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<bool> UpdateSinhVienAsGVAsync(string maSV, SinhVienUpdateDTO dto)
        {
            // Logic tương tự AdminService.UpdateSinhVienAsync
            var sv = await _unitOfWork.SinhViens.GetByIdAsync(maSV);
            if (sv == null) return false;
            sv.HoTen = dto.HoTen; sv.NgaySinh = dto.NgaySinh; sv.Lop = dto.Lop; sv.Nganh = dto.Nganh; sv.Email = dto.Email;
            _unitOfWork.SinhViens.Update(sv);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<BangDiem> NhapOrUpdateDiemAsync(DiemDTO dto)
        {
            var existingDiem = (await _unitOfWork.BangDiems.FindAsync(d => d.MaSV == dto.MaSV && d.MaMon == dto.MaMon)).FirstOrDefault();
            if (existingDiem != null)
            {
                existingDiem.Diem = dto.Diem;
                _unitOfWork.BangDiems.Update(existingDiem);
            }
            else
            {
                existingDiem = new BangDiem { MaSV = dto.MaSV, MaMon = dto.MaMon, Diem = dto.Diem, NgayNhap = DateTime.UtcNow, LanThi = 1 };
                await _unitOfWork.BangDiems.AddAsync(existingDiem);
            }
            await _unitOfWork.CompleteAsync();
            return existingDiem;
        }
    }
}
