using QLSinhVienCode.DTOs;
using QLSinhVienCode.Models;
using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Services
{
    public interface ISinhVienService
    {
        Task<IEnumerable<SinhVien>> GetSinhViensAsync();
        Task<SinhVien> CreateSinhVienAsync(SinhVienDTO dto);
        Task<IEnumerable<DiemChiTietDTO>> GetMyGradesAsync(string maSV);
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

        public async Task<IEnumerable<DiemChiTietDTO>> GetMyGradesAsync(string maSV)
        {
            var bangDiems = await _unitOfWork.BangDiems.FindAsync(d => d.MaSV == maSV);
            var maMons = bangDiems.Select(d => d.MaMon);
            var monHocs = (await _unitOfWork.MonHocs.FindAsync(m => maMons.Contains(m.MaMon)))
                            .ToDictionary(m => m.MaMon);

            return bangDiems.Select(d => new DiemChiTietDTO
            {
                MaMon = d.MaMon,
                TenMon = monHocs.ContainsKey(d.MaMon) ? monHocs[d.MaMon].TenMon : "N/A",
                SoTinChi = monHocs.ContainsKey(d.MaMon) ? monHocs[d.MaMon].SoTinChi : 0,
                Diem = d.Diem,
                LanThi = d.LanThi
            });
        }
    }
}
