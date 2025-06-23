// PATTERN 9: Observer
// File: Patterns/Observer/GradeObserver.cs
using QLSinhVienCode.Models;
using System.Diagnostics;

namespace QLSinhVienCode.Patterns.Observer
{
    // Observer: Thông báo khi điểm thay đổi.
    public interface IGradeObserver
    {
        void OnGradeUpdated(BangDiem diem);
    }
    public class AuditLogObserver : IGradeObserver 
    { 
        public void OnGradeUpdated(BangDiem diem) => Console.WriteLine($"AUDIT: Điểm của SV {diem.MaSV} cho môn {diem.MaMon} đã được cập nhật."); 
    }
    public class NotificationObserver : IGradeObserver 
    { 
        public void OnGradeUpdated(BangDiem diem) => Console.WriteLine($"NOTIFICATION: Gửi thông báo điểm mới cho SV {diem.MaSV}."); 
    }
    public class GradeChangeNotifier
    {
        private readonly IEnumerable<IGradeObserver> _observers;
        public GradeChangeNotifier(IEnumerable<IGradeObserver> observers) 
        { 
            _observers = observers; 
        }
        public void Notify(BangDiem diem) 
        { 
            foreach (var o in _observers) 
                o.OnGradeUpdated(diem); 
        }
    }
}