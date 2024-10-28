using Microsoft.AspNetCore.Mvc;

using LNSF.Application.Interfaces;
using LNSF.API.ServiceFilters;
using LNSF.Domain.DTOs;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.API.InputModels;

namespace LNSF.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class UserController(IUserRepository repository,
	IUserService service,
	IUserRoleService userRoleService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<UserDTO>>> Query([FromQuery] UserFilter filter) =>
	 	Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<User>> Post([FromBody] UserAndPassword userAndPassword)
	{
		User user = userAndPassword;
		user = await service.Create(user, userAndPassword.Password);
		return user;
	}

	[HttpPut]
	public async Task<ActionResult<User>> Put([FromBody] User user) =>
		Ok(await service.Update(user));

	[HttpPut("password")]
	public async Task<ActionResult<User>> Put([FromBody] UpdatePasswordIM model)
	{
		var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
		var user = await service.GetById(currentUserId);
		await service.UpdatePassword(user, model.OldPassword, model.NewPassword);
		return Ok(user);
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<User>> Delete(int id) =>
		Ok(await service.Delete(id));

	[HttpPost("add-user-to-role")]
	public async Task<ActionResult<UserRole>> AddToRole([FromBody] UserRoleIM model) =>
		Ok(await userRoleService.AddToRole(model.UserId, model.RoleId));

	[HttpDelete("remove-user-from-role")]
	public async Task<ActionResult<UserRole>> RemoveFromRole([FromBody] UserRoleIM model) =>
		Ok(await userRoleService.RemoveFromRole(model.UserId, model.RoleId));
}
