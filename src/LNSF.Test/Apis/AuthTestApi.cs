using System.Net;
using LNSF.Api.ViewModels;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
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
        // Arrange - User
        var password = new Bogus.Person().FirstName + "#" + new Bogus.Randomizer().Replace("###");
        var user = await GetUser(password: password, role: "Desenvolvedor");

        // Act
        var login = new UserLoginViewModel { UserName = user.UserName, Password = password };
        var token = await Post<AuthenticationToken>(_loginClient, login);

        // Assert
        Assert.NotNull(token);
        Assert.NotNull(token.Token);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("invalid", "")]
    [InlineData("", "invalid")]
    [InlineData("invalid", "invalid")]
    public async Task Post_InvalidLogin_BadRequest(string userName, string password)
    {
        // Arrange
        var login = new UserLoginViewModel { UserName = userName, Password = password };

        // Act - Assert
        var exception = await Post<AppException>(_loginClient, login);

        // Assert
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Post_ValidRefreshToken_Ok()
    {
        // Arrange - Account
        var password = new Bogus.Person().FirstName + "#" + new Bogus.Randomizer().Replace("###");
        var user = await GetUser(password: password, role: "Desenvolvedor");

        // Arrange - Login
        var login = new UserLoginViewModel { UserName = user.UserName, Password = password };
        var token = await Post<AuthenticationToken>(_loginClient, login);

        // Act
        var refreshToken = new RefreshTokenTokenViewModel { RefreshToken = token.Token };
        var newToken = await Post<AuthenticationToken>(_refreshTokenClient, refreshToken);

        // Assert
        Assert.NotNull(newToken);
    }

    [Theory]
    [InlineData("")]
    [InlineData("invalid")]
    public async Task Post_InvalidRefreshToken_BadRequest(string token)
    {
        // Arrange
        var refreshTokenViewModel = new AuthenticationToken { Token = token };

        // Act - Assert
        await Post<AppException>(_refreshTokenClient, refreshTokenViewModel);

        // Assert
        Assert.NotEqual((int)HttpStatusCode.OK, (int)HttpStatusCode.InternalServerError);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, (int)HttpStatusCode.OK);
    }
}
