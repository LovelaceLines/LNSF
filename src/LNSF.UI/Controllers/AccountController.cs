using AutoMapper;
using LNSF.Application.Services;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService, 
        IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AccountViewModel>>> Get([FromQuery]AccountFilter filter)
    {
        try
        {
            var accounts = await _accountService.Query(filter);
            var accountsViewModel = _mapper.Map<List<AccountViewModel>>(accounts);

            return Ok(accountsViewModel);
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

    [HttpPost]
    public async Task<ActionResult<AccountViewModel>> Post([FromBody]AccountPostViewModel account)
    {
        try
        {
            var accountMapped = _mapper.Map<Account>(account);
            var accountCreated = await _accountService.Create(accountMapped);
            var accountViewModel = _mapper.Map<AccountViewModel>(accountCreated);

            return Ok(accountViewModel);
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

    [HttpPut]
    public async Task<ActionResult<AccountViewModel>> Put([FromBody]AccountPutViewModel account)
    {
        try
        {
            var accountMapped = _mapper.Map<Account>(account);
            var accountUpdated = await _accountService.Update(accountMapped);
            var accountViewModel = _mapper.Map<AccountViewModel>(accountUpdated);

            return Ok(accountViewModel);
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

    [HttpDelete]
    public async Task<ActionResult<AccountViewModel>> Delete([FromBody]AccountDeleteViewModel id)
    {
        try
        {
            var accountDeleted = await _accountService.Delete(id.AccountId);
            var accountViewModel = _mapper.Map<AccountViewModel>(accountDeleted);

            return Ok(accountViewModel);
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
