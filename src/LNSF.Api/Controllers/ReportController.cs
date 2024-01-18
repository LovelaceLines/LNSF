using LNSF.Application.Interfaces;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IPeopleService _peopleService;

    private const string contentType = "application/pdf";

    public ReportController(IReportService reportService,
        IPeopleService peopleService)
    {
        _reportService = reportService;
        _peopleService = peopleService;
    }

    [HttpGet("export-people")]
    public async Task<IActionResult> Get([FromQuery] PeopleFilter filter)
    {
        var peoples = await _peopleService.Query(filter);
        var fileNameFRX = "people.frx";
        var referenceName = "People";

        var fileContents = _reportService.ExportReport<PeopleDTO>(peoples, fileNameFRX, referenceName);

        var fileNamePDF = "people_" + DateTime.Now + ".pdf";

        return File(fileContents, contentType, fileNamePDF);
    }
}
