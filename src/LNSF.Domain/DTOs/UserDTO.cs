using Microsoft.AspNetCore.Identity;

namespace LNSF.Domain.DTOs;

public class UserDTO : IdentityUser
{
    public List<string> Roles { get; set; } = new();
}
