using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    // Controller cho Giảng viên
    [ApiController]
    [Route("api/giangvien")]
    public class GiangVienController : ControllerBase
    {
        private readonly IGiangVienService _service;
        public GiangVienController(IGiangVienService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetGiangViensAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GiangVienDTO dto)
        {
            var gv = await _service.CreateGiangVienAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = gv.MaGV }, gv);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteGiangVienAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
