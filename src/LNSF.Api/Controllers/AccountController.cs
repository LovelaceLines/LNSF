using AutoMapper;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAccountService _service;

    public AccountController(IAccountService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of accounts based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<AccountViewModel>>> Get([FromQuery]AccountFilter filter)
    {
        var accounts = await _service.Query(filter);
        var accountsViewModel = _mapper.Map<List<AccountViewModel>>(accounts);

        return Ok(accountsViewModel);
    }

    /// <summary>
    /// Gets the count of accounts.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _service.GetCount());

    /// <summary>
    /// Gets an account by UserName.
    /// </summary>
    [HttpGet("{userName}")]
    public async Task<ActionResult<AccountViewModel>> Get(string userName) => 
        Ok(await _service.Get(userName));

    /// <summary>
    /// Creates a new account.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<AccountViewModel>> Post([FromBody]AccountPostViewModel account)
    {
        var accountMapped = _mapper.Map<Account>(account);
        var accountCreated = await _service.Create(accountMapped);
        var accountViewModel = _mapper.Map<AccountViewModel>(accountCreated);

        return Ok(accountViewModel);
    }

    /// <summary>
    /// Updates an account (UserName or Role). Note: do not update your password. 
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<AccountViewModel>> Put([FromBody]AccountPutViewModel account)
    {
        var accountMapped = _mapper.Map<Account>(account);
        var accountUpdated = await _service.Update(accountMapped);
        var accountViewModel = _mapper.Map<AccountViewModel>(accountUpdated);

        return Ok(accountViewModel);
    }

    /// <summary>
    /// Updates the password of an account.
    /// </summary>
    [HttpPut("password")]
    public async Task<ActionResult<AccountViewModel>> Put([FromBody]AccountPutPasswordViewModel account)
    {
        var accountUpdated = await _service.Update(account.Id, account.NewPassword, account.OldPassword);
        var accountViewModel = _mapper.Map<AccountViewModel>(accountUpdated);
        
        return Ok(accountViewModel);
    }

    /// <summary>
    /// Deletes an account by ID.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<AccountViewModel>> Delete(string id)
    {
        var accountDeleted = await _service.Delete(id);
        var accountViewModel = _mapper.Map<AccountViewModel>(accountDeleted);

        return Ok(accountViewModel);
    }
}
