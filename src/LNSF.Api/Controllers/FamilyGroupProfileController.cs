using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyGroupProfileController(IFamilyGroupProfileRepository repository, IFamilyGroupProfileService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<FamilyGroupProfile>>> Query([FromQuery] FamilyGroupProfileFilter filter) =>
		await repository.Query(filter);

	[HttpPost]
	public async Task<ActionResult<FamilyGroupProfile>> Post([FromBody] FamilyGroupProfile groupProfile) =>
		await service.Create(groupProfile);

	[HttpPut]
	public async Task<ActionResult<FamilyGroupProfile>> Put([FromBody] FamilyGroupProfile groupProfile) =>
		await service.Update(groupProfile);

	[HttpDelete("{id}")]
	public async Task<ActionResult<FamilyGroupProfile>> Delete(int id) =>
		await service.Delete(id);
}
