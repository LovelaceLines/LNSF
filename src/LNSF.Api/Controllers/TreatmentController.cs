using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreatmentController(ITreatmentRepository repository, ITreatmentService service) : ControllerBase
{
	[Authorize(Policy = "Base")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<Treatment>>> Query([FromQuery] TreatmentFilter filter) =>
		Ok(await repository.Query(filter));

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<Treatment>> Post(Treatment treatmentPost) =>
		Ok(await service.Create(treatmentPost));

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<Treatment>> Put(Treatment treatment) =>
		Ok(await service.Update(treatment));

	[Authorize(Policy = "Base")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<Treatment>> Delete(int id) =>
		Ok(await service.Delete(id));
}
