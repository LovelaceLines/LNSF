using AutoMapper;
using LNSF.Application.Services;
using LNSF.Domain.Entities;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationTokenService _authTokenService;
    private readonly IMapper _mapper;

    public AuthController(AuthenticationTokenService authTokenService,
        IMapper mapper)
    {
        _authTokenService = authTokenService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationTokenViewModel>> Login(AccountLoginViewModel account)
    {
        var accountMapped = _mapper.Map<Account>(account);
        var token = await _authTokenService.Login(accountMapped);

        return Ok(token);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticationTokenViewModel>> RefreshToken(AuthenticationTokenViewModel tokenViewModel)
    {
        var tokenMapped = _mapper.Map<AuthenticationToken>(tokenViewModel);
        var token = await _authTokenService.RefreshToken(tokenMapped);

        return Ok(token);
    }
}
