using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscortController(IEscortRepository repository, IEscortService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<Escort>>> Get([FromQuery] BaseFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<Escort>> Post(Escort escort) =>
		await service.Create(escort);

	[HttpPut]
	public async Task<ActionResult<Escort>> Put(Escort escortViewModel) =>
		await service.Update(escortViewModel);

	[HttpDelete("{id}")]
	public async Task<ActionResult<Escort>> Delete(int id) =>
		await service.Delete(id);
}
