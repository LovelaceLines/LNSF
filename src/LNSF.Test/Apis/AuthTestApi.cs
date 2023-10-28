using LNSF.Api.ViewModels;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class AuthTestApi : GlobalClientRequest
{
    private readonly HttpClient _loginClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/login") };
    private readonly HttpClient _refreshTokenClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/refresh-token") };

    [Fact]
    public async Task Post_ValidLogin_Ok()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Act
        var login = new AccountLoginViewModel { UserName = accountFake.UserName, Password = accountFake.Password };
        var token = await Post<AuthenticationTokenViewModel>(_loginClient, login);

        // Assert
        Assert.NotNull(token);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("invalid", "")]
    [InlineData("", "invalid")]
    [InlineData("invalid", "invalid")]
    public async Task Post_InvalidLogin_BadRequest(string userName, string password)
    {
        // Arrange
        var login = new AccountLoginViewModel { UserName = userName, Password = password };

        // Act - Assert
        await Assert.ThrowsAsync<Exception>(() => Post<AuthenticationTokenViewModel>(_loginClient, login));
    }

    [Fact]
    public async Task Post_ValidRefreshToken_Ok()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Login
        var login = new AccountLoginViewModel { UserName = accountFake.UserName, Password = accountFake.Password };
        var token = await Post<AuthenticationTokenViewModel>(_loginClient, login);

        // Act
        var refreshToken = new AuthenticationTokenViewModel { AccessToken = token.AccessToken , RefreshToken = token.RefreshToken };
        var newToken = await Post<AuthenticationTokenViewModel>(_refreshTokenClient, refreshToken);

        // Assert
        Assert.NotNull(newToken);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("invalid", "")]
    [InlineData("", "invalid")]
    [InlineData("invalid", "invalid")]
    public async Task Post_InvalidRefreshToken_BadRequest(string accessToken, string refreshToken)
    {
        // Arrange
        var refreshTokenViewModel = new AuthenticationTokenViewModel { AccessToken = accessToken, RefreshToken = refreshToken };

        // Act - Assert
        await Assert.ThrowsAsync<Exception>(() => Post<AuthenticationTokenViewModel>(_refreshTokenClient, refreshTokenViewModel));
    }
}
