using LNSF.Domain.Enums;

namespace LNSF.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public Role Role { get; set; }
    public string UserName { get; set; } = "";
    public string Password { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
