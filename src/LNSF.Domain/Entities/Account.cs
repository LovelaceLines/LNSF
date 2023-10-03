namespace LNSF.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string Role { get; set; } = "";
    public string Password { get; set; } = "";
}
