using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class UserTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidUser_Ok()
    {
        var userFake = new UserPostViewModelFake().Generate();

        var countBefore = await GetCount(_userClient);
        var userPosted = await Post<UserPostViewModel>(_userClient, userFake);
        var countAfter = await GetCount(_userClient);

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
        var userFake = new UserPostViewModelFake(email: email).Generate();

        var countBefore = await GetCount(_userClient);
        var exception = await Post<AppException>(_userClient, userFake);
        var countAfter = await GetCount(_userClient);

        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("invalid")]
    public async Task Post_InvalidUserWithInvalidPassword_BadRequest(string password)
    {
        var userFake = new UserPostViewModelFake(password: password).Generate();

        var countBefore = await GetCount(_userClient);
        var exception = await Post<AppException>(_userClient, userFake);
        var countAfter = await GetCount(_userClient);

        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Theory]
    [InlineData("in")]
    [InlineData("invalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalidinvalid")]
    public async Task Post_InvalidUserWithInvalidUserName_BadRequest(string userName)
    {
        var userFake = new UserPostViewModelFake(userName: userName).Generate();

        var countBefore = await GetCount(_userClient);
        var exception = await Post<AppException>(_userClient, userFake);
        var countAfter = await GetCount(_userClient);

        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidUserWithRepeatedInfos_BadRequest()
    {
        var userFake = new UserPostViewModelFake().Generate();
        _ = await Post<UserPostViewModel>(_userClient, userFake);

        var countBefore = await GetCount(_userClient);
        var exceptionUserName = await Post<AppException>(_userClient, new UserPostViewModelFake(userName: userFake.UserName).Generate());
        var exceptionEmail = await Post<AppException>(_userClient, new UserPostViewModelFake(email: userFake.Email).Generate());
        var exceptionPhoneNumber = await Post<AppException>(_userClient, new UserPostViewModelFake(phoneNumber: userFake.PhoneNumber).Generate());
        var countAfter = await GetCount(_userClient);

        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.OK, exceptionUserName.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionUserName.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionEmail.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionEmail.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionPhoneNumber.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionPhoneNumber.StatusCode);
    }

    [Fact]
    public async Task Put_ValidUser_Ok()
    {
        var user = await GetUser();

        var countBefore = await GetCount(_userClient);
        var userFake = new UserViewModelFake(id: user.Id).Generate();
        var userUpdated = await Put<UserViewModel>(_userClient, userFake);
        var countAfter = await GetCount(_userClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(userFake, userUpdated);
    }

    [Fact]
    public async void Put_InvalidUserWithRepeatInfo_BadRequest()
    {
        var user = await GetUser();
        var repeatInfo = await GetUser();

        var countBefore = await GetCount(_userClient);
        var userFakeUserName = new UserViewModelFake(id: user.Id, userName: repeatInfo.UserName).Generate();
        var exceptionUserName = await Put<AppException>(_userClient, userFakeUserName);
        var userFakeEmail = new UserViewModelFake(id: user.Id, email: repeatInfo.Email).Generate();
        var exceptionEmail = await Put<AppException>(_userClient, userFakeEmail);
        var userFakePhoneNumber = new UserViewModelFake(id: user.Id, phoneNumber: repeatInfo.PhoneNumber).Generate();
        var exceptionPhoneNumber = await Put<AppException>(_userClient, userFakePhoneNumber);
        var countAfter = await GetCount(_userClient);

        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.OK, exceptionUserName.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionUserName.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionEmail.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionEmail.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionPhoneNumber.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionPhoneNumber.StatusCode);
    }

    [Fact]
    public async void Delete_ValidUser_Ok()
    {
        var user = await GetUser();

        var countBefore = await GetCount(_userClient);
        var userDeleted = await Delete<UserViewModel>(_userClient, user.Id);
        var countAfter = await GetCount(_userClient);

        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(user, userDeleted);
    }

    [Fact]
    public async void Post_ValidAddUserToRole_Ok()
    {
        var user = await GetUser();

        var countBefore = await GetCount(_userRoleClient);
        var userRole = new UserRoleViewModel { UserId = user.Id, RoleName = "Desenvolvedor" };
        var userRolePosted = await Post<UserViewModel>(_addUserToRoleClient, userRole);
        var countAfter = await GetCount(_userRoleClient);

        Assert.Equal(countBefore + 1, countAfter);
    }

    [Fact]
    public async void Post_InvalidAddUserToRole_BadRequest()
    {
        var user = await GetUser();

        var countBefore = await GetCount(_userRoleClient);
        var userRole = new UserRoleViewModel { UserId = user.Id, RoleName = "invalid" };
        var exception = await Post<AppException>(_addUserToRoleClient, userRole);
        var countAfter = await GetCount(_userRoleClient);

        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
}
