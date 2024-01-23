using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RoleController(IRoleService roleService,
        IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of roles based on the provided filter.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<RoleViewModel>>> Get([FromQuery] RoleFilter filter)
    {
        var roles = await _roleService.Query(filter);
        return _mapper.Map<List<RoleViewModel>>(roles);
    }

    /// <summary>
    /// Get the count of roles.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _roleService.GetCount();

    /// <summary>
    /// Creates a new role.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RoleViewModel>> Post([FromBody] RolePostViewModel rolePostViewModel)
    {
        var role = _mapper.Map<IdentityRole>(rolePostViewModel);
        role = await _roleService.Add(role);
        return _mapper.Map<RoleViewModel>(role);
    }

    /// <summary>
    /// Updates a role.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RoleViewModel>> Put([FromBody] RoleViewModel rolePostViewModel)
    {
        var role = _mapper.Map<IdentityRole>(rolePostViewModel);
        role = await _roleService.Update(role);
        return _mapper.Map<RoleViewModel>(role);
    }

    /// <summary>
    /// Deletes a role by name.
    /// </summary>
    [Authorize]
    [HttpDelete("{roleName}")]
    public async Task<ActionResult> Delete(string roleName) =>
        Ok(await _roleService.Delete(roleName));
}
