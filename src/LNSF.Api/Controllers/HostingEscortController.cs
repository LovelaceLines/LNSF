using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostingEscortController(IHostingEscortRepository repository) : ControllerBase
{
	[Authorize]
	[HttpGet]
	public async Task<ActionResult<QueryResult<HostingEscort>>> Query([FromQuery] BaseFilter filter) =>
		Ok(await repository.Query(filter));
}
