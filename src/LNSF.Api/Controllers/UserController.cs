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
    public async Task<List<IdentityUser>> Get([FromServices] UserFilter filter) =>
        await _userService.Query(filter);
    
    [HttpPost]
    public async Task<UserViewModel> Post(UserPostViewModel userPostViewModel)
    {
        var user = _mapper.Map<IdentityUser>(userPostViewModel);
        user = await _userService.Create(user);
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
    public async Task<UserViewModel> PutPassword(UserPutPasswordViewModel userPutPasswordViewModel)
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
}
