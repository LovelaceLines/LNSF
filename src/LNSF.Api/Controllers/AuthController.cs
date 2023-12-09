using LNSF.Application.Interfaces;
using LNSF.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LNSF.Domain.Entities;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationTokenService _service;

    public AuthController(IAuthenticationTokenService service) => 
        _service = service;

    /// <summary>
    /// Authenticates a user and returns an authentication token.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationToken>> Login(UserLoginViewModel user) => 
        Ok(await _service.Login(user.UserName, user.Password));

    /// <summary>
    /// Refreshes an authentication token.
    /// </summary>
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticationToken>> RefreshToken(RefreshTokenTokenViewModel token) => 
        Ok(await _service.RefreshToken(token.RefreshToken));
}
