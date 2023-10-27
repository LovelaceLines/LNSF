using AutoMapper;
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
}
