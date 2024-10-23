using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IRoleRepository repository, IRoleService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<Role>>> Query([FromQuery] RoleFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<Role>> Post([FromBody] Role role) =>
		Ok(await service.Create(role));

	[HttpPut]
	public async Task<ActionResult<Role>> Put([FromBody] Role role) =>
		Ok(await service.Update(role));

	[HttpDelete("{id}")]
	public async Task<ActionResult<Role>> Delete(int id) =>
		Ok(await service.Delete(id));
}
