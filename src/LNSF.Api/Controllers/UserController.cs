using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet]
    public async Task<List<IdentityUser>> Get([FromQuery] UserFilter filter) =>
        await _userService.Query(filter);

    [HttpGet("count")]
    public async Task<int> GetCount() =>
        await _userService.GetCount();
    
    [HttpPost]
    public async Task<UserViewModel> Post(UserPostViewModel userPostViewModel)
    {
        var user = _mapper.Map<IdentityUser>(userPostViewModel);
        user = await _userService.Create(user, userPostViewModel.Password);
        return _mapper.Map<UserViewModel>(user);
    }

    [HttpPost("add-user-to-role")]
    public async Task<UserViewModel> Post(UserRoleViewModel userRoleViewModel)
    {
        var user = await _userService.AddToRole(userRoleViewModel.UserId, userRoleViewModel.RoleName);
        return _mapper.Map<UserViewModel>(user);
    }

    [HttpPut]   
    public async Task<UserViewModel> Put(UserViewModel userViewModel)
    {
        var user = _mapper.Map<IdentityUser>(userViewModel);
        user = await _userService.Update(user);
        return _mapper.Map<UserViewModel>(user);
    }

    [HttpPut("password")]
    public async Task<UserViewModel> Put(UserPutPasswordViewModel userPutPasswordViewModel)
    {
        var user = await _userService.UpdatePassword(userPutPasswordViewModel.Id, userPutPasswordViewModel.OldPassword, userPutPasswordViewModel.NewPassword);
        return _mapper.Map<UserViewModel>(user);
    }

    [HttpDelete("{id}")]
    public async Task<UserViewModel> Delete(string id)
    {
        var user = await _userService.Delete(id);
        return _mapper.Map<UserViewModel>(user);
    }

    [HttpDelete("remove-user-from-role")]
    public async Task<UserViewModel> Delete(UserRoleViewModel userRoleViewModel)
    {
        var user = await _userService.RemoveFromRole(userRoleViewModel.UserId, userRoleViewModel.RoleName);
        return _mapper.Map<UserViewModel>(user);
    }
}
