namespace LNSF.Domain.Entities;

public class AuthenticationToken
{
    public int Id { get; set; }
    public string Token { get; set; } = "";
    public string RefreshToken { get; set; } = "";

    public int AccountId { get; set; }
    public Account? Account { get; set; }
}
