using QLSinhVienCode.DTOs;
using QLSinhVienCode.Models;
using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Services
{
    public interface ISinhVienService
    {
        Task<IEnumerable<SinhVien>> GetSinhViensAsync();
        Task<SinhVien> CreateSinhVienAsync(SinhVienDTO dto);
    }
    public class SinhVienService : ISinhVienService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SinhVienService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<IEnumerable<SinhVien>> GetSinhViensAsync() => await _unitOfWork.SinhViens.GetAllAsync();
        public async Task<SinhVien> CreateSinhVienAsync(SinhVienDTO dto)
        {
            var sinhVien = new SinhVien { MaSV = dto.MaSV, HoTen = dto.HoTen, NgaySinh = dto.NgaySinh, Lop = dto.Lop, Nganh = dto.Nganh };
            await _unitOfWork.SinhViens.AddAsync(sinhVien);
            await _unitOfWork.CompleteAsync();
            return sinhVien;
        }
    }
}
