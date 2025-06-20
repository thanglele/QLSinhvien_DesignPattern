using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Bật dòng này để bảo vệ API
    public class AdminController : ControllerBase
    {
        private readonly ISinhVienService _sinhVienService;
        private readonly IGiangVienService _giangVienService;

        public AdminController(ISinhVienService sinhVienService, IGiangVienService giangVienService)
        {
            _sinhVienService = sinhVienService;
            _giangVienService = giangVienService;
        }

        // --- Sinh Vien Endpoints ---
        [HttpGet("sinhvien")]
        public async Task<IActionResult> GetSinhViens() => Ok(await _sinhVienService.GetSinhViensAsync());

        [HttpPost("sinhvien")]
        public async Task<IActionResult> CreateSinhVien([FromBody] SinhVienDTO dto) { /* ... */ return Ok(); }

        [HttpPut("sinhvien/{id}")]
        public async Task<IActionResult> UpdateSinhVien(string id, [FromBody] SinhVienDTO dto) { /* ... */ return Ok(); }

        [HttpDelete("sinhvien/{id}")]
        public async Task<IActionResult> DeleteSinhVien(string id) { /* ... */ return Ok(); }

        // --- Giang Vien Endpoints ---
        [HttpGet("giangvien")]
        public async Task<IActionResult> GetGiangViens() => Ok(await _giangVienService.GetGiangViensAsync());

        // ... Các endpoint khác cho Giảng viên, Môn học ...
    }
}
