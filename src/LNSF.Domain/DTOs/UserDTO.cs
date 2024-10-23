using LNSF.Domain.Entities;

namespace LNSF.Domain.DTOs;

public class UserDTO : User
{
    public List<Role> Roles { get; set; } = [];

    public UserDTO() { }

    public UserDTO(User user)
    {
        Id = user.Id;
        UserName = user.UserName;
        Name = user.Name;
        Email = user.Email;
        PhoneNumber = user.PhoneNumber;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
    }

    public UserDTO(User user, List<Role> roles) : this(user)
    {
        Roles = roles;
    }
}