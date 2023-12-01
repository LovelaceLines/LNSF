using LNSF.Domain.Enums;

namespace LNSF.Api.ViewModels;

public class UserPostViewModel
{
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
    public Role Role { get; set; } = Role.VOLUNTEERING;
}

public class UserViewModel
{
    public string Id { get; set; } = "";
    public string UserName { get; set; } = "";
    public string Email { get; set; } = "";
    public Role Role { get; set; }
}

public class UserPutPasswordViewModel
{
    public string Id { get; set; } = "";
    public string OldPassword { get; set; } = "";
    public string NewPassword { get; set; } = "";
}
