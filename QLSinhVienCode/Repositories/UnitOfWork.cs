using QLSinhVienCode.Data;
using QLSinhVienCode.Models;

namespace QLSinhVienCode.Repositories
{
    // Các interface cho repository đặc thù
    public interface IGiangVienRepository : IGenericRepository<GiangVien> { }
    public interface IMonHocRepository : IGenericRepository<MonHoc> { }

    // Các lớp triển khai repository đặc thù
    public class GiangVienRepository : GenericRepository<GiangVien>, IGiangVienRepository
    {
        public GiangVienRepository(ApplicationDbContext context) : base(context) { }
    }

    public class MonHocRepository : GenericRepository<MonHoc>, IMonHocRepository
    {
        public MonHocRepository(ApplicationDbContext context) : base(context) { }
    }


    // Cập nhật UnitOfWork
    public interface IUnitOfWork : IDisposable
    {
        ISinhVienRepository SinhViens { get; }
        IGiangVienRepository GiangViens { get; }
        IMonHocRepository MonHocs { get; }
        IGenericRepository<BangDiem> BangDiems { get; }
        IGenericRepository<TaiKhoan> TaiKhoans { get; }
        IGenericRepository<Khoa> Khoas { get; }
        Task<int> CompleteAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ISinhVienRepository SinhViens { get; private set; }
        public IGiangVienRepository GiangViens { get; private set; }
        public IMonHocRepository MonHocs { get; private set; }
        public IGenericRepository<BangDiem> BangDiems { get; private set; }
        public IGenericRepository<TaiKhoan> TaiKhoans { get; private set; }
        public IGenericRepository<Khoa> Khoas { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            SinhViens = new SinhVienRepository(_context);
            GiangViens = new GiangVienRepository(_context);
            MonHocs = new MonHocRepository(_context);
            BangDiems = new GenericRepository<BangDiem>(_context);
            TaiKhoans = new GenericRepository<TaiKhoan>(_context);
            Khoas = new GenericRepository<Khoa>(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
