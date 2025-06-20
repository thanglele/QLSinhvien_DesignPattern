// PATTERN 1: Singleton
// File: Patterns/Singleton/FileLogger.cs
namespace QLSinhVienCode.Patterns.Singleton
{
    /// <summary>
    /// Singleton Pattern: Đảm bảo rằng chỉ có một đối tượng FileLogger
    /// được tạo trong suốt vòng đời của ứng dụng.
    /// Hữu ích cho việc quản lý tài nguyên chia sẻ như ghi log, kết nối CSDL, ...
    /// Trong ASP.NET Core, DI container sẽ quản lý việc này khi đăng ký với AddSingleton().
    /// </summary>
    public class FileLogger 
    { 
        public void Log(string message) 
        { 
            Console.WriteLine($"LOG: {message}"); 
        } 
    }
}