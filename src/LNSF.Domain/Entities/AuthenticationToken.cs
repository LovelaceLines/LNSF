namespace LNSF.Domain.Entities;

public class AuthenticationToken
{
    public string Token { get; set; } = "";
    public DateTime Expires { get; set; }

    public AuthenticationToken() { }

    public AuthenticationToken(string token, DateTime expires)
    {
        Token = token;
        Expires = expires;
    }
}
