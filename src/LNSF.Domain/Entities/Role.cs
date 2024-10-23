using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Domain.Entities;

public class Role : IdentityRole<int>
{
    override public required string Name { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Role() { }

    public Role(string roleName)
    {
        Name = roleName;
    }

    public Role(Role role)
    {
        Id = role.Id;
        Name = role.Name;
    }

    [JsonIgnore]
    override public string? NormalizedName { get; set; }
    [JsonIgnore]
    override public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}