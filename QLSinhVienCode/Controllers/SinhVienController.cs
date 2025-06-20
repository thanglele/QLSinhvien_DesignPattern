using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    [ApiController]
    [Route("api/sinhvien")]
    public class SinhVienController : ControllerBase
    {
        private readonly ISinhVienService _sinhVienService;
        public SinhVienController(ISinhVienService sinhVienService) { _sinhVienService = sinhVienService; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sinhViens = await _sinhVienService.GetSinhViensAsync();
            return Ok(sinhViens);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SinhVienDTO dto)
        {
            var newSinhVien = await _sinhVienService.CreateSinhVienAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = newSinhVien.MaSV }, newSinhVien);
        }
    }
}
