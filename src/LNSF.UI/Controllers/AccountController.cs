using LNSF.Application.Services;
using LNSF.Domain.DTOs;
using LNSF.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
        => _accountService = accountService;

    [HttpPost("login")]
    public async Task<ActionResult<bool>> Login(AccountFilters filters)
    {
        try
        {
            return Ok(await _accountService.Exist(filters));
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
