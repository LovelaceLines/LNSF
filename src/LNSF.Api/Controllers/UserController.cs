using LNSF.Application.Interfaces;
using LNSF.API.ServiceFilters;
using LNSF.Domain.DTOs;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.API.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
public class UserController(IUserRepository repository,
	IUserService service,
	IUserRoleService userRoleService) : ControllerBase
{
	[Authorize(Policy = "Base")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<UserDTO>>> Query([FromQuery] UserFilter filter) =>
	 	Ok(await repository.Query(filter));

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<User>> Post([FromBody] UserAndPassword userAndPassword)
	{
		User user = userAndPassword;
		user = await service.Create(user, userAndPassword.Password);
		return user;
	}

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<User>> Put([FromBody] User user) =>
		Ok(await service.Update(user));

	[Authorize(Policy = "Base")]
	[HttpPut("password")]
	public async Task<ActionResult<User>> Put([FromBody] UpdatePasswordIM model)
	{
		var currentUserId = (int)HttpContext.Items["CurrentUserId"]!;
		var user = await service.GetById(currentUserId);
		await service.UpdatePassword(user, model.OldPassword, model.NewPassword);
		return Ok(user);
	}

	[Authorize(Policy = "Base")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<User>> Delete(int id) =>
		Ok(await service.Delete(id));

	[Authorize(Policy = "Base")]
	[HttpPost("add-user-to-role")]
	public async Task<ActionResult<UserRole>> AddToRole([FromBody] UserRole userRole) =>
		Ok(await userRoleService.AddToRole(userRole));

	[Authorize(Policy = "Base")]
	[HttpDelete("remove-user-from-role")]
	public async Task<ActionResult<UserRole>> RemoveFromRole([FromBody] UserRole userRole) =>
		Ok(await userRoleService.RemoveFromRole(userRole));
}
