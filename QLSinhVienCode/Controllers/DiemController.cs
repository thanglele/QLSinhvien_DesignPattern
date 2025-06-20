using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    // Controller cho Điểm
    [ApiController]
    [Route("api/diem")]
    public class DiemController : ControllerBase
    {
        private readonly IDiemService _service;
        public DiemController(IDiemService service) { _service = service; }

        [HttpPost]
        public async Task<IActionResult> NhapDiem([FromBody] NhapDiemDTO dto)
        {
            var diem = await _service.NhapDiemAsync(dto);
            return Ok(diem);
        }

        [HttpGet]
        public async Task<IActionResult> XemDiem([FromQuery] string role, [FromQuery] string actorId)
        {
            // Ví dụ: /api/diem?role=sinhvien&actorId=2251172244
            var result = await _service.XemDiemAsync(role, actorId);
            return Ok(result);
        }
    }
}
