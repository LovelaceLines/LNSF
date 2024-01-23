namespace LNSF.Domain.DTOs;

public class AuthToken
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";

    public AuthToken() { }

    public AuthToken(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}
