using LNSF.Api.ViewModels;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class AuthTestApi : GlobalClientRequest
{
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
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Get_ValidRefreshToken_Ok()
    {
        // Arrange - Account
        var password = new Bogus.Person().FirstName + "#" + new Bogus.Randomizer().Replace("###");
        var user = await GetUser(password: password, role: "Desenvolvedor");

        // Arrange - Login
        var login = new UserLoginViewModel { UserName = user.UserName, Password = password };
        var token = await Post<AuthenticationToken>(_loginClient, login);
        _acessToken = token.Token;

        // Act
        var refreshToken = await Get<AuthenticationToken>(_refreshTokenClient);

        // Assert
        Assert.NotNull(refreshToken);
        Assert.NotEmpty(refreshToken.Token);
    }

    [Theory]
    [InlineData("invalid")]
    public async Task Get_InvalidRefreshToken_BadRequest(string token)
    {
        // Arrange - Token
        _acessToken = token;

        // Act - Assert
        var exception = await Get<AppException>(_refreshTokenClient);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
    }

    [Fact]
    public async Task GetUser_ValidUser_Ok()
    {
        // Arrange - User
        var password = new Bogus.Person().FirstName + "#" + new Bogus.Randomizer().Replace("###");
        var user = await GetUser(password: password, role: "Desenvolvedor");

        // Arrange - Login
        var login = new UserLoginViewModel{ UserName = user.UserName, Password = password };
        var token = await Post<AuthenticationToken>(_loginClient, login);
        _acessToken = token.Token;

        // Act
        var userGet = await Get<UserGetViewModel>(_authUserClient);

        // Assert
        Assert.Equivalent(user, userGet);
    }

    [Theory]
    [InlineData("invalid")]
    public async Task GetUser_InvalidUserWithInvalidToken_Unauthorized(string token)
    {
        // Arrange - Token
        _acessToken = token;

        // Act - Assert
        var exception = await Get<AppException>(_authUserClient);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
    }
}
