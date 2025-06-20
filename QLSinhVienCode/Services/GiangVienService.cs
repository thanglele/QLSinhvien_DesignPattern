using QLSinhVienCode.DTOs;
using QLSinhVienCode.Models;
using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Services
{
    // Service cho Giảng viên
    public interface IGiangVienService
    {
        Task<IEnumerable<GiangVien>> GetGiangViensAsync();
        Task<GiangVien> CreateGiangVienAsync(GiangVienDTO dto);
        Task<bool> DeleteGiangVienAsync(string id);
    }
    public class GiangVienService : IGiangVienService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GiangVienService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<IEnumerable<GiangVien>> GetGiangViensAsync() => await _unitOfWork.GiangViens.GetAllAsync();
        public async Task<GiangVien> CreateGiangVienAsync(GiangVienDTO dto)
        {
            var gv = new GiangVien { MaGV = dto.MaGV, HoTen = dto.HoTen, BoMon = dto.BoMon, Email = dto.Email };
            await _unitOfWork.GiangViens.AddAsync(gv);
            await _unitOfWork.CompleteAsync();
            return gv;
        }
        public async Task<bool> DeleteGiangVienAsync(string id)
        {
            var gv = await _unitOfWork.GiangViens.GetByIdAsync(id);
            if (gv == null) return false;
            _unitOfWork.GiangViens.Delete(gv);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
