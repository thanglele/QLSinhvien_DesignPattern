using QLSinhVienCode.Models;

namespace QLSinhVienCode.Patterns.Adapter
{
    // Dữ liệu từ hệ thống cũ
    public class LegacyStudent 
    { 
        public string StudentID; 
        public string FullName; 
    }
    // Adapter: Chuyển đổi dữ liệu từ hệ thống cũ sang hệ thống mới.
    public class LegacyStudentAdapter
    {
        private readonly LegacyStudent _legacyStudent;
        public LegacyStudentAdapter(LegacyStudent legacyStudent) 
        { 
            _legacyStudent = legacyStudent; 
        }
        public SinhVien ToNewStudent() => new SinhVien 
        { 
            MaSV = _legacyStudent.StudentID, 
            HoTen = _legacyStudent.FullName 
        };
    }
}
