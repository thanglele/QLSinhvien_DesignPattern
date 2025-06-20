using QLSinhVienCode.Models;

namespace QLSinhVienCode.Patterns.Builder
{
    // Builder: Xây dựng đối tượng MonHoc phức tạp.
    public interface IMonHocBuilder
    {
        IMonHocBuilder WithMaMon(string maMon);
        IMonHocBuilder WithTenMon(string tenMon);
        IMonHocBuilder WithSoTinChi(int soTinChi);
        IMonHocBuilder InHocKy(int hocKy);
        IMonHocBuilder AssignToKhoa(string maKhoa);
        IMonHocBuilder AssignToGiangVien(string maGV);
        MonHoc Build();
    }
    public class MonHocBuilder : IMonHocBuilder
    {
        private MonHoc _monHoc = new MonHoc();
        public MonHoc Build() => _monHoc;
        public IMonHocBuilder InHocKy(int hocKy) { _monHoc.HocKy = hocKy; return this; }
        public IMonHocBuilder WithMaMon(string maMon) { _monHoc.MaMon = maMon; return this; }
        public IMonHocBuilder WithTenMon(string tenMon) { _monHoc.TenMon = tenMon; return this; }
        public IMonHocBuilder WithSoTinChi(int soTinChi) { _monHoc.SoTinChi = soTinChi; return this; }
        public IMonHocBuilder AssignToKhoa(string maKhoa) { _monHoc.MaKhoa = maKhoa; return this; }
        public IMonHocBuilder AssignToGiangVien(string maGV) { _monHoc.MaGV = maGV; return this; }
    }
}
