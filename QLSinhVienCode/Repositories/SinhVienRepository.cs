using QLSinhVienCode.Data;
using QLSinhVienCode.Models;

namespace QLSinhVienCode.Repositories
{
    public interface ISinhVienRepository : IGenericRepository<SinhVien> 
    {

    }
    public class SinhVienRepository : GenericRepository<SinhVien>, ISinhVienRepository 
    { 
        public SinhVienRepository(ApplicationDbContext context) : base(context) 
        {

        } 
    }
}
