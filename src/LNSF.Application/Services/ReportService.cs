using FastReport.Export.PdfSimple;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Application;

public class ReportService : IReportService
{
    private readonly IPeopleService _peopleService;

    public ReportService(IPeopleService peopleService) => 
        _peopleService = peopleService;

    public async Task<FileContentResult> ExportPeopleReport(PeopleFilter filter)
    {
        var peoples = await _peopleService.Query(filter);

        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports", "people.frx");
        
        var report = new FastReport.Report().Report;
        
        if (File.Exists(outputPath))
            report.Load(outputPath);

        report.Dictionary.RegisterBusinessObject(peoples, "People", 10, true);
        // report.Report.Save(outputPath);
        report.Prepare();

        var pdf = new PDFSimpleExport();
        using var ms = new MemoryStream();
        pdf.Export(report, ms);
        ms.Flush();
        
        var contentType = "application/pdf";
        var fileName = "people.pdf";
        var fileContents = ms.ToArray();

        var response = new FileContentResult(fileContents, contentType)
        {
            FileDownloadName = fileName
        };

        return response;
    }
}
