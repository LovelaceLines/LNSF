using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class AccountTestApi : GlobalClientRequest
{
    public readonly HttpClient _putPasswordClient = new() { BaseAddress = new Uri($"{BaseUrl}Account/password") };

    [Fact]
    public async Task Post_ValidAccount_Ok()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        
        // Arrange - Count
        var countBefore = await GetCount(_accountClient);

        // Act
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.NotNull(accountPosted.Id);
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equal(accountFake.UserName, accountPosted.UserName);
        Assert.Equal(accountFake.Role, accountPosted.Role);
    }

    [Fact]
    public async Task Post_InvalidAccountWithEmptyUserName_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        accountFake.UserName = string.Empty;

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<AccountViewModel>(_accountClient, accountFake));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidAccountWithEmptyPassword_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        accountFake.Password = string.Empty;

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<AccountViewModel>(_accountClient, accountFake));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public async Task Post_InvalidAccountWithInvalidRole_BadRequest(int role)
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        accountFake.Role = (Domain.Enums.Role)(role);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<AccountViewModel>(_accountClient, accountFake));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_ValidAccount_Ok()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutFake = new AccountPostViewModelFake().Generate();
        var accountMapped = _mapper.Map<AccountPutViewModel>(accountPutFake);
        accountMapped.Id = accountPosted.Id;
        var accountPuted = await Put<AccountViewModel>(_accountClient, accountMapped);
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(accountMapped.Id, accountPuted.Id);
    }

    [Fact]
    public async Task Put_InvalidAccountWithEmptyUserName_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutFake = new AccountPostViewModelFake().Generate();
        accountPutFake.UserName = string.Empty;
        var accountMapped = _mapper.Map<AccountPutViewModel>(accountPutFake);
        accountMapped.Id = accountPosted.Id;

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_accountClient, accountMapped));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public async Task Put_InvalidAccountWithInvalidRole_BadRequest(int role)
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutFake = new AccountPostViewModelFake().Generate();
        accountPutFake.Role = (Domain.Enums.Role)(role);
        var accountMapped = _mapper.Map<AccountPutViewModel>(accountPutFake);
        accountMapped.Id = accountPosted.Id;

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_accountClient, accountMapped));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidAccountWithEmptyId_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutFake = new AccountPostViewModelFake().Generate();
        var accountMapped = _mapper.Map<AccountPutViewModel>(accountPutFake);
        accountMapped.Id = string.Empty;

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_accountClient, accountMapped));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidAccountWithInvalidId_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutFake = new AccountPostViewModelFake().Generate();
        var accountMapped = _mapper.Map<AccountPutViewModel>(accountPutFake);
        accountMapped.Id = "invalid";

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_accountClient, accountMapped));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task PutPassword_ValidPassword_Ok()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutPasswordFake = new AccountPostViewModelFake().Generate();
        var accountPutPassword = new AccountPutPasswordViewModel
        {
            Id = accountPosted.Id,
            OldPassword = accountFake.Password,
            NewPassword = accountPutPasswordFake.Password
        };
        var accountPuted = await Put<AccountViewModel>(_putPasswordClient, accountPutPassword);
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(accountPutPassword.Id, accountPuted.Id);
        Assert.Equal(accountPosted.UserName, accountPuted.UserName);
    }

    [Fact]
    public async Task PutPassword_InvalidPasswordWithEmptyId_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutPasswordFake = new AccountPostViewModelFake().Generate();
        var accountPutPassword = new AccountPutPasswordViewModel
        {
            Id = string.Empty,
            OldPassword = accountFake.Password,
            NewPassword = accountPutPasswordFake.Password
        };

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_putPasswordClient, accountPutPassword));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task PutPassword_InvalidPasswordWithInvalidId_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutPasswordFake = new AccountPostViewModelFake().Generate();
        var accountPutPassword = new AccountPutPasswordViewModel
        {
            Id = "invalid",
            OldPassword = accountFake.Password,
            NewPassword = accountPutPasswordFake.Password
        };

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_putPasswordClient, accountPutPassword));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task PutPassword_InvalidPasswordWithEmptyOldPassword_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutPasswordFake = new AccountPostViewModelFake().Generate();
        var accountPutPassword = new AccountPutPasswordViewModel
        {
            Id = accountPosted.Id,
            OldPassword = string.Empty,
            NewPassword = accountPutPasswordFake.Password
        };

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_putPasswordClient, accountPutPassword));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task PutPassword_InvalidPasswordWithEmptyNewPassword_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountPutPasswordFake = new AccountPostViewModelFake().Generate();
        var accountPutPassword = new AccountPutPasswordViewModel
        {
            Id = accountPosted.Id,
            OldPassword = accountFake.Password,
            NewPassword = string.Empty
        };

        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_putPasswordClient, accountPutPassword));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task PutPassword_InvalidPasswordWithInvalidOldPassword_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);
        var accountPutPassword = new AccountPutPasswordViewModel
        {
            Id = accountPosted.Id,
            OldPassword = "invalid",
            NewPassword = new Bogus.Randomizer().ReplaceNumbers("######")
        };

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        await Assert.ThrowsAsync<Exception>(() => Put<AccountViewModel>(_putPasswordClient, accountPutPassword));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Delete_ValidAccount_Ok()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        var accountDeleted = await Delete<AccountViewModel>(_accountClient, accountPosted.Id);
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equal(accountPosted.Id, accountDeleted.Id);
    }

    [Fact]
    public async Task Delete_InvalidAccountWithEmptyId_BadRequest()
    {
        // Arrange - Account
        var accountFake = new AccountPostViewModelFake().Generate();
        var accountPosted = await Post<AccountViewModel>(_accountClient, accountFake);

        // Arrange - Count
        var countBefore = await GetCount(_accountClient);
        
        // Act
        await Assert.ThrowsAsync<Exception>(() => Delete<AccountViewModel>(_accountClient, string.Empty));
        var countAfter = await GetCount(_accountClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }
}
