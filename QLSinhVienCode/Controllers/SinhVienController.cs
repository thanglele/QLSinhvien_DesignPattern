using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Repositories;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    [ApiController]
    [Route("api/sinhvien-role")]
    [Authorize(Roles = "SinhVien,Admin")]
    public class SinhVienRoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public SinhVienRoleController(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        [HttpGet("diem")]
        public async Task<IActionResult> GetMyGrades()
        {
            var actorId = User.FindFirst("actorId")?.Value;
            if (string.IsNullOrEmpty(actorId)) return Unauthorized();
            var diem = await _unitOfWork.BangDiems.FindAsync(d => d.MaSV == actorId);
            return Ok(diem);
        }

        [HttpGet("monhoc")]
        public async Task<IActionResult> GetAllMonHoc() => Ok(await _unitOfWork.MonHocs.GetAllAsync());
    }
}
