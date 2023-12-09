using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controller;

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

    [HttpGet]
    public async Task<ActionResult<List<RoleViewModel>>> Get([FromQuery]RoleFilter filter)
    {
        var roles = await _roleService.Query(filter);
        return _mapper.Map<List<RoleViewModel>>(roles);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        await _roleService.GetCount();

    [HttpPost]
    public async Task<ActionResult<RoleViewModel>> Post([FromBody]RolePostViewModel rolePostViewModel)
    {
        var role = _mapper.Map<IdentityRole>(rolePostViewModel);
        role = await _roleService.Add(role);
        return _mapper.Map<RoleViewModel>(role);
    }

    [HttpPut]
    public async Task<ActionResult<RoleViewModel>> Put([FromBody]RoleViewModel rolePostViewModel)
    {
        var role = _mapper.Map<IdentityRole>(rolePostViewModel);
        role = await _roleService.Update(role);
        return _mapper.Map<RoleViewModel>(role);
    }

    [HttpDelete("{roleName}")]
    public async Task<ActionResult> Delete(string roleName) => 
        Ok(await _roleService.Delete(roleName));
}
