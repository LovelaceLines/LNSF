using LNSF.Application.Interfaces;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController(IPeopleRepository repository,
	IPeopleService service,
	IPeopleRoomHostingService peopleRoomHostingService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<PeopleDTO>>> Query([FromQuery] PeopleFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<People>> Post([FromBody] People people) =>
		Ok(await service.Create(people));

	[HttpPut]
	public async Task<ActionResult<People>> Put([FromBody] People people) =>
		Ok(await service.Update(people));

	[HttpPost("add-people-to-room")]
	public async Task<ActionResult<PeopleRoomHosting>> Post([FromBody] PeopleRoomHosting peopleRoomHosting) =>
		Ok(await peopleRoomHostingService.Create(peopleRoomHosting));

	[HttpDelete("remove-people-from-room")]
	public async Task<ActionResult<PeopleRoomHosting>> Delete(PeopleRoomHosting peopleRoomHosting) =>
		Ok(await peopleRoomHostingService.Delete(peopleRoomHosting));
}
