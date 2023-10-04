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
        
        var token = new TokenViewModel()
        {
            Role = account.Role,
            Token = _tokenService.GenerateToken(accountMapped)
        };

        return Ok(token);
    }
}
