using LNSF.Application.Interfaces;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController(IPeopleRepository repository,
	IPeopleService service,
	IPeopleRoomHostingService peopleRoomHostingService) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<PeopleDTO>>> Query([FromQuery] PeopleFilter filter) =>
		Ok(await repository.Query(filter));

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<People>> Post([FromBody] People people) =>
		Ok(await service.Create(people));

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<People>> Put([FromBody] People people) =>
		Ok(await service.Update(people));

	[Authorize(Policy = "Base")]
	[HttpPost("add-people-to-room")]
	public async Task<ActionResult<PeopleRoomHosting>> Post([FromBody] PeopleRoomHosting peopleRoomHosting) =>
		Ok(await peopleRoomHostingService.Create(peopleRoomHosting));

	[Authorize(Policy = "Base")]
	[HttpDelete("remove-people-from-room")]
	public async Task<ActionResult<PeopleRoomHosting>> Delete(PeopleRoomHosting peopleRoomHosting) =>
		Ok(await peopleRoomHostingService.Delete(peopleRoomHosting));
}
