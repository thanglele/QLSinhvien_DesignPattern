using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Repositories;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IGiangVienService _giangVienService;
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IAdminService adminService, IUnitOfWork unitOfWork, IGiangVienService giangVienService)
        {
            _adminService = adminService;
            _unitOfWork = unitOfWork;
            _giangVienService = giangVienService;
        }

        // Sinh Viên
        [HttpGet("sinhvien")] public async Task<IActionResult> GetAllSinhVien() => Ok(await _unitOfWork.SinhViens.GetAllAsync());
        [HttpPost("sinhvien")] public async Task<IActionResult> CreateSinhVien(SinhVienCreateDTO dto) => Ok(await _adminService.CreateSinhVienWithAccountAsync(dto));
        [HttpPut("sinhvien/{id}")] public async Task<IActionResult> UpdateSinhVien(string id, SinhVienUpdateDTO dto) => Ok(await _adminService.UpdateSinhVienAsync(id, dto));
        [HttpDelete("sinhvien/{id}")] public async Task<IActionResult> DeleteSinhVien(string id) => Ok(await _adminService.DeleteSinhVienAsync(id));

        // Giảng Viên
        [HttpGet("giangvien")] public async Task<IActionResult> GetAllGiangVien() => Ok(await _unitOfWork.GiangViens.GetAllAsync());
        [HttpPost("giangvien")] public async Task<IActionResult> CreateGiangVien(GiangVienCreateDTO dto) => Ok(await _adminService.CreateGiangVienWithAccountAsync(dto));
        [HttpPut("giangvien/{id}")] public async Task<IActionResult> UpdateGiangVien(string id, GiangVienUpdateDTO dto) => Ok(await _adminService.UpdateGiangVienAsync(id, dto));
        [HttpDelete("giangvien/{id}")] public async Task<IActionResult> DeleteGiangVien(string id) => Ok(await _adminService.DeleteGiangVienAsync(id));

        // Môn Học
        [HttpGet("monhoc")] public async Task<IActionResult> GetAllMonHoc() => Ok(await _unitOfWork.MonHocs.GetAllAsync());
        [HttpPost("monhoc")] public async Task<IActionResult> CreateMonHoc(MonHocCreateUpdateDTO dto) { /*...*/ return Ok(); }
        [HttpPut("monhoc/{id}")] public async Task<IActionResult> UpdateMonHoc(string id, MonHocCreateUpdateDTO dto) { /*...*/ return Ok(); }
        [HttpDelete("monhoc/{id}")] public async Task<IActionResult> DeleteMonHoc(string id) { /*...*/ return Ok(); }

        // Điểm (Admin có quyền như Giảng viên)
        [HttpPost("diem")] public async Task<IActionResult> NhapDiem(DiemDTO dto) => Ok(await _giangVienService.NhapOrUpdateDiemAsync(dto));
    }
}
