using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmergencyContactController(IEmergencyContactRepository repository, IEmergencyContactService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<EmergencyContact>>> Query([FromQuery] EmergencyContactFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<EmergencyContact>> Post([FromBody] EmergencyContact emergencyContact) =>
		await service.Create(emergencyContact);

	[HttpPut]
	public async Task<ActionResult<EmergencyContact>> Put([FromBody] EmergencyContact emergencyContact) =>
		await service.Update(emergencyContact);

	[HttpDelete("{id}")]
	public async Task<ActionResult<EmergencyContact>> Delete(int id) =>
		await service.Delete(id);
}
