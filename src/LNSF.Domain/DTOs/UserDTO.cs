using Microsoft.AspNetCore.Identity;

namespace LNSF.Domain.DTOs;

public class UserDTO : IdentityUser
{
    public List<string> Roles { get; set; } = new();

    public UserDTO() { }

    public UserDTO(IdentityUser user, List<string> roles)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        EmailConfirmed = user.EmailConfirmed;
        PhoneNumber = user.PhoneNumber;
        PhoneNumberConfirmed = user.PhoneNumberConfirmed;
        TwoFactorEnabled = user.TwoFactorEnabled;
        LockoutEnd = user.LockoutEnd;
        LockoutEnabled = user.LockoutEnabled;
        AccessFailedCount = user.AccessFailedCount;
        Roles = roles;
    }
}
