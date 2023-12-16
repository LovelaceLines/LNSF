using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationTokenService _service;
    private readonly IMapper _mapper;

    public AuthController(IAuthenticationTokenService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Authenticates a user and returns an authentication token.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationToken>> Login(UserLoginViewModel user) => 
        await _service.Login(user.UserName, user.Password);

    /// <summary>
    /// Refreshes an authentication token.
    /// </summary>
    [HttpGet("refresh-token")]
    public async Task<ActionResult<AuthenticationToken>> RefreshToken([FromHeader(Name = "Authorization")] string auth)
    {
        var token = ExtractTokenFromHeader(auth);
        return await _service.RefreshToken(token);
    }

    /// <summary>
    /// Retrieves the user information based on the provided authorization token. Note: Authorization token in the format "Bearer {token}".
    /// </summary>
    [HttpGet("user")]
    public async Task<ActionResult<UserGetViewModel>> Get([FromHeader(Name = "Authorization")] string auth)
    {
        var token = ExtractTokenFromHeader(auth);
        var userDTO = await _service.GetUser(token);
        return _mapper.Map<UserGetViewModel>(userDTO);
    }

    private static string ExtractTokenFromHeader(string auth)
    {
        string[] headerParts = auth.Split(' ');

        if (headerParts.Length != 2 || !headerParts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            throw new AppException("Cabeçalho de autorização inválido!", HttpStatusCode.BadRequest);

        return headerParts[1];
    }
}
