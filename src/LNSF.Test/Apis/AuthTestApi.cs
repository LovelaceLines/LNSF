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
    public async Task Post_ValidLogin_ReturnsToken()
    {
        // Arrange
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountMapped = _mapper.Map<AccountLoginViewModel>(accountFake);
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Act
        var login = await Auth(_loginClient, accountMapped);

        // Assert
        Assert.NotNull(login.AccessToken);
        Assert.NotNull(login.RefreshToken); 
    }
}
