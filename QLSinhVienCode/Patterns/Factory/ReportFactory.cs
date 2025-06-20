// PATTERN 2 & 3: Factory Method & Abstract Factory
// File: Patterns/Factory/ReportFactory.cs
namespace QLSinhVienCode.Patterns.Factory
{
    // Abstract Factory & Factory Method: Tạo báo cáo
    public interface IReport { string Generate(object data); }
    public class PdfReport : IReport { public string Generate(object data) => "--- PDF REPORT ---"; }
    public class ExcelReport : IReport { public string Generate(object data) => "--- EXCEL REPORT ---"; }

    public abstract class ReportFactory
    {
        public abstract IReport CreateReport();
    }
    public class PdfReportFactory : ReportFactory { public override IReport CreateReport() => new PdfReport(); }
    public class ExcelReportFactory : ReportFactory { public override IReport CreateReport() => new ExcelReport(); }
}