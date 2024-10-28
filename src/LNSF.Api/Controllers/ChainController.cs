using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChainController(IChainRepository repository) : ControllerBase
{
	[HttpGet("count-people-hosted")]
	public async Task<IActionResult> GetCountPeopleHosted([FromQuery] ChainCountPeopleHostedFilter filter) =>
		Ok(await repository.QueryCountPeopleHosted(filter));

	[HttpGet("people-will-hosted")]
	public async Task<IActionResult> GetPeopleWillHosted([FromQuery] ChainDayFilter filter) =>
		Ok(await repository.QueryPeopleWillHosted(filter));

	[HttpGet("people-will-birthday")]
	public async Task<IActionResult> GetPeopleWillBirthday([FromQuery] ChainDayFilter filter) =>
		Ok(await repository.QueryPeopleWillBirthday(filter));

	[HttpGet("count-type-treatment")]
	public async Task<ActionResult<List<TreatmentDTO>>> GetCountTypeTreatment([FromQuery] ChainIntervalCheckFilter filter) =>
		Ok(await repository.QueryCountTypeTreatment(filter));
}
