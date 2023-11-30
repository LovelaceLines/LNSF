using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Application.Interfaces;

public interface IReportService
{
    Task<FileContentResult> ExportPeopleReport(PeopleFilter filter);
}
