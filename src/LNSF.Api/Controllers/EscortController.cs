using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscortController(IEscortRepository repository, IEscortService service) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<Escort>>> Get([FromQuery] EscortFilter filter) =>
		Ok(await repository.Query(filter));

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<Escort>> Post(Escort escort) =>
		await service.Create(escort);

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<Escort>> Put(Escort escortViewModel) =>
		await service.Update(escortViewModel);

	[Authorize(Policy = "Base")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<Escort>> Delete(int id) =>
		await service.Delete(id);
}
