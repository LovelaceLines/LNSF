using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService) => 
        _userRoleService = userRoleService;

    [HttpGet]
    public async Task<ActionResult<List<IdentityUserRole<string>>>> Query([FromQuery] UserRoleFilter filter) => 
        await _userRoleService.Query(filter);
    
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _userRoleService.GetCount());
}
