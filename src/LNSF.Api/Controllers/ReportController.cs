using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService) => 
        _reportService = reportService;

    [HttpGet("export-people-report")]
    public async Task<ActionResult> Export([FromQuery] PeopleFilter filter)
    {
        var report = await _reportService.ExportPeopleReport(filter);
        return Ok(report);
    }
}
