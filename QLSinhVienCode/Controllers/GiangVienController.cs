using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Repositories;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    [ApiController]
    [Route("api/giangvien-role")]
    [Authorize(Roles = "GiangVien,Admin")]
    public class GiangVienRoleController : ControllerBase
    {
        private readonly IGiangVienService _giangVienService;
        private readonly IUnitOfWork _unitOfWork;
        public GiangVienRoleController(IGiangVienService gvService, IUnitOfWork unitOfWork) { _giangVienService = gvService; _unitOfWork = unitOfWork; }

        // Nghiệp vụ của Giảng viên
        [HttpPut("sinhvien/{id}")] public async Task<IActionResult> UpdateSinhVien(string id, SinhVienUpdateDTO dto) => Ok(await _giangVienService.UpdateSinhVienAsGVAsync(id, dto));
        [HttpPost("diem")] public async Task<IActionResult> NhapDiem(DiemDTO dto) => Ok(await _giangVienService.NhapOrUpdateDiemAsync(dto));

        // Chức năng xem chung
        [HttpGet("monhoc")] public async Task<IActionResult> GetAllMonHoc() => Ok(await _unitOfWork.MonHocs.GetAllAsync());
        [HttpGet("sinhvien")] public async Task<IActionResult> GetAllSinhVien() => Ok(await _unitOfWork.SinhViens.GetAllAsync());
    }
}
