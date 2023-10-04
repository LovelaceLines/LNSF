using AutoMapper;
using LNSF.Application.Services;
using LNSF.Domain.Entities;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly AccountService _accountService;
    private readonly IMapper _mapper;

    public AuthController(TokenService tokenService,
        AccountService accountService,
        IMapper mapper)
    {
        _tokenService = tokenService;
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenViewModel>> Login(AccountViewModel account)
    {
        var accountMapped = _mapper.Map<Account>(account);

        if (!await _accountService.Exist(accountMapped))
            return BadRequest("Usuário ou senha inválidos");

        var token = _tokenService.GenerateToken(accountMapped);
        var refreshToken = _tokenService.GenerateRefreshToken();
        _tokenService.SetRefreshToken(account.Role, refreshToken);
        
        var tokenViewModel = new TokenViewModel()
        {
            Role = account.Role,
            Token = token,
            RefreshToken = refreshToken
        };

        return Ok(tokenViewModel);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenViewModel>> RefreshToken(TokenViewModel tokenViewModel)
    {
        var refreshToken = _tokenService.GetRefreshToken(tokenViewModel.Role);

        if (refreshToken != tokenViewModel.RefreshToken)
            return BadRequest("Token inválido");

        var token = _tokenService.GenerateToken(new List<Claim>()
        {
            new(ClaimTypes.Role, tokenViewModel.Role)
        });
        refreshToken = _tokenService.GenerateRefreshToken();
        _tokenService.SetRefreshToken(tokenViewModel.Role, refreshToken);

        tokenViewModel.Token = token;
        tokenViewModel.RefreshToken = refreshToken;

        return Ok(tokenViewModel);
    }

    // [HttpPost("register")]
    [NonAction]
    public async Task<ActionResult> Register(AccountViewModel account)
    {
        var accountMapped = _mapper.Map<Account>(account);

        if (await _accountService.Exist(accountMapped))
            return BadRequest("Usuário já cadastrado");

        await _accountService.Create(accountMapped);

        return Ok();
    }
}
