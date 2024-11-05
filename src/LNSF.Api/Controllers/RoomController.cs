using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController(IRoomRepository repository, IRoomService service) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<Room>>> Query([FromQuery] RoomFilter filter) =>
		await repository.Query(filter);

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<Room>> Post([FromBody] Room room) =>
		await service.Create(room);

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<Room>> Put([FromBody] Room room) =>
		await service.Update(room);
}
