using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleRoomHostingController(IPeopleRoomHostingRepository repository) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<PeopleRoomHosting>>> Get([FromQuery] PeopleRoomHostingFilter filter) =>
		Ok(await repository.Query(filter));
}
