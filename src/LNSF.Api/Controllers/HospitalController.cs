using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HospitalController(IHospitalRepository repository, IHospitalService service) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<Hospital>>> Query([FromQuery] HospitalFilter filter) =>
		Ok(await repository.Query(filter));

	[Authorize(Policy = "User")]
	[HttpPost]
	public async Task<ActionResult<Hospital>> Post([FromBody] Hospital hospital) =>
		await service.Create(hospital);

	[Authorize(Policy = "User")]
	[HttpPut]
	public async Task<ActionResult<Hospital>> Put([FromBody] Hospital hospital) =>
		await service.Update(hospital);

	[Authorize(Policy = "User")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<Hospital>> Delete(int id) =>
		await service.Delete(id);
}
