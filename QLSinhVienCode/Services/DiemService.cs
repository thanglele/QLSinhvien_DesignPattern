using QLSinhVienCode.DTOs;
using QLSinhVienCode.Models;
using QLSinhVienCode.Patterns.Observer;
using QLSinhVienCode.Patterns.Strategy;
using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Services
{
    // Service cho Điểm
    public interface IDiemService
    {
        Task<BangDiem> NhapDiemAsync(NhapDiemDTO dto);
        Task<object> XemDiemAsync(string role, string actorId);
    }
    public class DiemService : IDiemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GradeChangeNotifier _notifier;
        private readonly GradeViewingContext _viewingContext;

        public DiemService(IUnitOfWork unitOfWork, GradeChangeNotifier notifier)
        {
            _unitOfWork = unitOfWork;
            _notifier = notifier;
            _viewingContext = new GradeViewingContext();
        }

        public async Task<BangDiem> NhapDiemAsync(NhapDiemDTO dto)
        {
            var diem = new BangDiem { MaSV = dto.MaSV, MaMon = dto.MaMon, Diem = dto.Diem, NgayNhap = DateTime.UtcNow };
            await _unitOfWork.BangDiems.AddAsync(diem);
            await _unitOfWork.CompleteAsync();

            // Dùng Observer Pattern để thông báo
            _notifier.Notify(diem);

            return diem;
        }

        public async Task<object> XemDiemAsync(string role, string actorId)
        {
            _viewingContext.SetStrategy(role);
            return await _viewingContext.ViewGrades(_unitOfWork, actorId);
        }
    }
}
