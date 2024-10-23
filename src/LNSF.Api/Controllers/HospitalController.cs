using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HospitalController(IHospitalRepository repository, IHospitalService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<Hospital>>> Query([FromQuery] HospitalFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<Hospital>> Post([FromBody] Hospital hospital) =>
		await service.Create(hospital);

	[HttpPut]
	public async Task<ActionResult<Hospital>> Put([FromBody] Hospital hospital) =>
		await service.Update(hospital);

	[HttpDelete("{id}")]
	public async Task<ActionResult<Hospital>> Delete(int id) =>
		await service.Delete(id);
}
