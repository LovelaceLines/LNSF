using System.Net;
using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class UserTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidUser_Ok()
    {
        // Arrange - User
        var userFake = new UserPostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_userClient);

        // Act - User
        var userPosted = await Post<UserPostViewModel>(_userClient, userFake);

        // Act - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equal(userFake.UserName, userPosted.UserName);
        Assert.Equal(userFake.Email, userPosted.Email);
        Assert.Equal(userFake.PhoneNumber, userPosted.PhoneNumber);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("invalid")]

    public async Task Post_InvalidUserWithInvalidEmail_BadRequest(string email)
    {
        // Arrange - User
        var userFake = new UserPostViewModelFake(email: email).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_userClient);

        // Act - User
        var exception = await Post<AppException>(_userClient, userFake);

        // Arrange - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("invalid")]
    public async Task Post_InvalidUserWithInvalidPassword_BadRequest(string password)
    {
        // Arrange - User
        var userFake = new UserPostViewModelFake(password: password).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_userClient);

        // Act - User
        var exception = await Post<AppException>(_userClient, userFake);

        // Arrange - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Theory]
    [InlineData("in")]
    [InlineData("invalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalid")]
    public async Task Post_InvalidUserWithInvalidUserName_BadRequest(string userName)
    {
        // Arrange - User
        var userFake = new UserPostViewModelFake(userName: userName).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_userClient);

        // Act - User
        var exception = await Post<AppException>(_userClient, userFake);

        // Arrange - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidUserWithRepeatedInfos_BadRequest()
    {
        // Arrange - User
        var userFake = new UserPostViewModelFake().Generate();
        _ = await Post<UserPostViewModel>(_userClient, userFake);

        // Arrange - Count
        var countBefore = await GetCount(_userClient);

        // Act - User
        var exceptionUserName = await Post<AppException>(_userClient, new UserPostViewModelFake(userName: userFake.UserName).Generate());
        var exceptionEmail = await Post<AppException>(_userClient, new UserPostViewModelFake(email: userFake.Email).Generate());
        var exceptionPhoneNumber = await Post<AppException>(_userClient, new UserPostViewModelFake(phoneNumber: userFake.PhoneNumber).Generate());

        // Arrange - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionUserName.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionUserName.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionEmail.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionEmail.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionPhoneNumber.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionPhoneNumber.StatusCode);
    }

    [Fact]
    public async Task Put_ValidUser_Ok()
    {
        // Arrange - User
        var user = await GetUser();

        // Arrange - Count
        var countBefore = await GetCount(_userClient);

        // Act - User
        var userFake = new UserViewModelFake(id: user.Id).Generate();
        var userUpdated = await Put<UserViewModel>(_userClient, userFake);

        // Arrange - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(userFake, userUpdated);
    }

    [Fact]
    public async void Put_InvalidUserWithRepeatInfo_BadRequest()
    {
        // Arrange - User
        var user = await GetUser();
        var repeatInfo = await GetUser();

        // Arrange - Count
        var countBefore = await GetCount(_userClient);
        
        // Act - User
        var userFakeUserName = new UserViewModelFake(id: user.Id, userName: repeatInfo.UserName).Generate();
        var exceptionUserName = await Put<AppException>(_userClient, userFakeUserName);
        var userFakeEmail = new UserViewModelFake(id: user.Id, email: repeatInfo.Email).Generate();
        var exceptionEmail = await Put<AppException>(_userClient, userFakeEmail);
        var userFakePhoneNumber = new UserViewModelFake(id: user.Id, phoneNumber: repeatInfo.PhoneNumber).Generate();
        var exceptionPhoneNumber = await Put<AppException>(_userClient, userFakePhoneNumber);

        // Arrange - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionUserName.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionUserName.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionEmail.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionEmail.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionPhoneNumber.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionPhoneNumber.StatusCode);
    }

    [Fact]
    public async void Delete_ValidUser_Ok()
    {
        // Arrange - User
        var user = await GetUser();

        // Arrange - Count
        var countBefore = await GetCount(_userClient);

        // Act - User
        var userDeleted = await Delete<UserViewModel>(_userClient, user.Id);

        // Arrange - Count
        var countAfter = await GetCount(_userClient);

        // Assert
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(user, userDeleted);
    }

    [Fact]
    public async void Post_ValidAddUserToRole_Ok()
    {
        // Arrange - User
        var user = await GetUser();

        // Arrange - Count
        var countBefore = await GetCount(_userRoleClient);

        // Act - User
        var userRole = new UserRoleViewModel { UserId = user.Id, RoleName = "Desenvolvedor" };
        var userRolePosted = await Post<UserViewModel>(_addUserToRoleClient, userRole);

        // Arrange - Count
        var countAfter = await GetCount(_userRoleClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
    }

    [Fact]
    public async void Post_InvalidAddUserToRole_BadRequest()
    {
        // Arrange - User
        var user = await GetUser();

        // Arrange - Count
        var countBefore = await GetCount(_userRoleClient);

        // Act - User
        var userRole = new UserRoleViewModel { UserId = user.Id, RoleName = "invalid" };
        var exception = await Post<AppException>(_addUserToRoleClient, userRole);

        // Arrange - Count
        var countAfter = await GetCount(_userRoleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }
}
