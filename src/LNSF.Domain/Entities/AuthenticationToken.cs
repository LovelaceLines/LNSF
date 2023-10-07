namespace LNSF.Domain.Entities;

public class AuthenticationToken
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public DateTime Expires { get; set; } = DateTime.Now;
}
