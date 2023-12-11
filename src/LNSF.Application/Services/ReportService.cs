using FastReport.Export.PdfSimple;
using LNSF.Application.Interfaces;

namespace LNSF.Application;

public class ReportService : IReportService
{
    public byte[] ExportReport<T>(List<T> list, string fileNameFRX, string referenceName)
    {
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports", fileNameFRX);
        
        var report = new FastReport.Report().Report;
        
        report.Load(outputPath);
        
        report.Dictionary.RegisterBusinessObject(list, referenceName, 10, true);
        
        // uncomment to save FRX file
        // report.Report.Save(outputPath);
        report.Prepare();
        
        using var ms = new MemoryStream();
        
        var pdf = new PDFSimpleExport();
        pdf.Export(report, ms);
        
        ms.Flush();
        
        return ms.ToArray();
    }
}
