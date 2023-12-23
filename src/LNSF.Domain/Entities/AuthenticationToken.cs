namespace LNSF.Domain.Entities;

public class AuthenticationToken
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";

    public AuthenticationToken() { }

    public AuthenticationToken(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
