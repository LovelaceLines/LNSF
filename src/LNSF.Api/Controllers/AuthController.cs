using AutoMapper;
using LNSF.Application.Interfaces;
using LNSF.Application.Services;
using LNSF.Domain.Entities;
using LNSF.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationTokenViewModel>> Login(AccountLoginViewModel account)
    {
        var accountMapped = _mapper.Map<Account>(account);
        var token = await _service.Login(accountMapped);

        return Ok(token);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticationTokenViewModel>> RefreshToken(AuthenticationTokenViewModel tokenViewModel)
    {
        var tokenMapped = _mapper.Map<AuthenticationToken>(tokenViewModel);
        var token = await _service.RefreshToken(tokenMapped);

        return Ok(token);
    }
}
