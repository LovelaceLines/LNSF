using LNSF.API.InputModels;
using LNSF.Application.Interfaces;
using LNSF.API.Utils;
using LNSF.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService service,
	IUserService userService,
	IRoleService roleService) : ControllerBase
{
	[HttpPost("login")]
	public async Task<ActionResult<UserToken>> Login([FromBody] LoginIM login)
	{
		var token = await service.Login(login.UserName, login.Password);
		var user = await userService.GetByUserName(login.UserName);
		var roles = await roleService.GetByUser(user.Id);

		return Ok(new UserToken(token, new UserDTO(user, roles)));
	}

	[HttpGet("refresh-token")]
	public async Task<ActionResult<AuthToken>> RefreshToken([FromHeader(Name = "Authorization")] string auth)
	{
		var token = AuthUtil.ExtractTokenFromHeader(auth);
		return Ok(await service.RefreshToken(token));
	}

	[Authorize(Policy = "User")]
	[HttpGet("user")]
	public async Task<ActionResult<UserDTO>> Get([FromHeader(Name = "Authorization")] string auth)
	{
		var token = AuthUtil.ExtractTokenFromHeader(auth);
		return Ok(await service.GetUser(token));
	}
}
