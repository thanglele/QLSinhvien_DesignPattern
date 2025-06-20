using QLSinhVienCode.Repositories;

namespace QLSinhVienCode.Patterns.Strategy
{
    // Interface chung cho các chiến lược xem điểm
    public interface IGradeViewingStrategy
    {
        Task<object> Execute(IUnitOfWork unitOfWork, string actorId);
    }

    // Chiến lược cho Admin: Xem tất cả điểm
    public class AdminGradeViewingStrategy : IGradeViewingStrategy
    {
        public async Task<object> Execute(IUnitOfWork unitOfWork, string actorId)
        {
            return await unitOfWork.BangDiems.GetAllAsync();
        }
    }

    // Chiến lược cho Giảng viên: Chỉ xem điểm các môn mình dạy (logic giả định)
    public class LecturerGradeViewingStrategy : IGradeViewingStrategy
    {
        public async Task<object> Execute(IUnitOfWork unitOfWork, string actorId)
        {
            // Trong thực tế, cần tìm các môn giảng viên `actorId` dạy
            return await unitOfWork.BangDiems.FindAsync(d => d.MonHoc.MaGV == actorId);
        }
    }

    // Chiến lược cho Sinh viên: Chỉ xem điểm cá nhân
    public class StudentGradeViewingStrategy : IGradeViewingStrategy
    {
        public async Task<object> Execute(IUnitOfWork unitOfWork, string actorId)
        {
            return await unitOfWork.BangDiems.FindAsync(d => d.MaSV == actorId);
        }
    }

    // Context để lựa chọn và thực thi chiến lược phù hợp
    public class GradeViewingContext
    {
        private IGradeViewingStrategy _strategy;

        public void SetStrategy(string role)
        {
            _strategy = role.ToLower() switch
            {
                "admin" => new AdminGradeViewingStrategy(),
                "giangvien" => new LecturerGradeViewingStrategy(),
                "sinhvien" => new StudentGradeViewingStrategy(),
                _ => throw new ArgumentException("Vai trò không hợp lệ", nameof(role))
            };
        }

        public async Task<object> ViewGrades(IUnitOfWork unitOfWork, string actorId)
        {
            return await _strategy.Execute(unitOfWork, actorId);
        }
    }
}
