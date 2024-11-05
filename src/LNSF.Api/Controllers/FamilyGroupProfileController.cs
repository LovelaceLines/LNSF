using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyGroupProfileController(IFamilyGroupProfileRepository repository, IFamilyGroupProfileService service) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<FamilyGroupProfile>>> Query([FromQuery] FamilyGroupProfileFilter filter) =>
		await repository.Query(filter);

	[Authorize(Policy = "User")]
	[HttpPost]
	public async Task<ActionResult<FamilyGroupProfile>> Post([FromBody] FamilyGroupProfile groupProfile) =>
		await service.Create(groupProfile);

	[Authorize(Policy = "User")]
	[HttpPut]
	public async Task<ActionResult<FamilyGroupProfile>> Put([FromBody] FamilyGroupProfile groupProfile) =>
		await service.Update(groupProfile);

	[Authorize(Policy = "User")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<FamilyGroupProfile>> Delete(int id) =>
		await service.Delete(id);
}
