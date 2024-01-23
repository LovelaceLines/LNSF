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
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    /// <summary>
    /// Retrieves a list of users based on provided filter.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<List<UserGetViewModel>> Get([FromQuery] UserFilter filter)
    {
        var users = await _userService.Query(filter);
        return _mapper.Map<List<UserGetViewModel>>(users);
    }

    /// <summary>
    /// Gets the count of User.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<int> GetCount() =>
        await _userService.GetCount();

    /// <summary>
    /// Creates a new user.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<UserViewModel> Post(UserPostViewModel userPostViewModel)
    {
        var user = _mapper.Map<IdentityUser>(userPostViewModel);
        user = await _userService.Create(user, userPostViewModel.Password);
        return _mapper.Map<UserViewModel>(user);
    }

    /// <summary>
    /// Adds a user to a role.
    /// </summary>
    [Authorize]
    [HttpPost("add-user-to-role")]
    public async Task<UserViewModel> Post(UserRoleViewModel userRoleViewModel)
    {
        var user = await _userService.AddToRole(userRoleViewModel.UserId, userRoleViewModel.RoleName);
        return _mapper.Map<UserViewModel>(user);
    }

    /// <summary>
    /// Updates a user. Note: do not update the user's password.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<UserViewModel> Put(UserViewModel userViewModel)
    {
        var user = _mapper.Map<IdentityUser>(userViewModel);
        user = await _userService.Update(user);
        return _mapper.Map<UserViewModel>(user);
    }

    /// <summary>
    /// Updates the password of a user.
    /// </summary>
    [Authorize]
    [HttpPut("password")]
    public async Task<UserViewModel> Put(UserPutPasswordViewModel userPutPasswordViewModel)
    {
        var user = await _userService.UpdatePassword(userPutPasswordViewModel.Id, userPutPasswordViewModel.OldPassword, userPutPasswordViewModel.NewPassword);
        return _mapper.Map<UserViewModel>(user);
    }

    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<UserViewModel> Delete(string id)
    {
        var user = await _userService.Delete(id);
        return _mapper.Map<UserViewModel>(user);
    }

    /// <summary>
    /// Removes a user from a role.
    /// </summary>
    [Authorize]
    [HttpDelete("remove-user-from-role")]
    public async Task<ActionResult<UserViewModel>> Delete(UserRoleViewModel userRoleViewModel)
    {
        var user = await _userService.RemoveFromRole(userRoleViewModel.UserId, userRoleViewModel.RoleName);
        return Ok(_mapper.Map<UserViewModel>(user));
    }
}
