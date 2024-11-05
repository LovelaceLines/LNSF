using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmergencyContactController(IEmergencyContactRepository repository, IEmergencyContactService service) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<EmergencyContact>>> Query([FromQuery] EmergencyContactFilter filter) =>
		Ok(await repository.Query(filter));

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<EmergencyContact>> Post([FromBody] EmergencyContact emergencyContact) =>
		await service.Create(emergencyContact);

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<EmergencyContact>> Put([FromBody] EmergencyContact emergencyContact) =>
		await service.Update(emergencyContact);

	[Authorize(Policy = "Base")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<EmergencyContact>> Delete(int id) =>
		await service.Delete(id);
}
