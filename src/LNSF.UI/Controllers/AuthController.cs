using AutoMapper;
using LNSF.Application.Services;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationTokenService _authTokenService;
    private readonly AccountService _accountService;
    private readonly IMapper _mapper;

    public AuthController(AuthenticationTokenService authTokenService,
        AccountService accountService,
        IMapper mapper)
    {
        _authTokenService = authTokenService;
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationTokenViewModel>> Login(AccountLoginViewModel account)
    {
        try
        {
            var accountMapped = _mapper.Map<Account>(account);
            var token = await _authTokenService.Login(accountMapped);

            return Ok(token);
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticationTokenViewModel>> RefreshToken(AuthenticationTokenViewModel tokenViewModel)
    {
        try
        {
            var tokenMapped = _mapper.Map<AuthenticationToken>(tokenViewModel);
            var token = await _authTokenService.RefreshToken(tokenMapped);

            return Ok(token);
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout(AuthenticationTokenViewModel tokenViewModel)
    {
        try
        {
            var tokenMapped = _mapper.Map<AuthenticationToken>(tokenViewModel);
            await _authTokenService.Logout(tokenMapped);

            return Ok();
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<AccountViewModel>> Get()
    {
        try
        {
            return Ok(await _authTokenService.Get());
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
