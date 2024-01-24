using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class UserTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_QueryUser_Ok()
    {
        var user = await GetUser();

        var userQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id));

        Assert.Equivalent(user.Id, userQueried.Id);
        Assert.Equivalent(user.UserName, userQueried.UserName);
        Assert.Equivalent(user.Email, userQueried.Email);
        Assert.Equivalent(user.PhoneNumber, userQueried.PhoneNumber);
    }

    [Fact]
    public async Task Get_QueryUserName_Ok()
    {
        var user = await GetUser();

        var userNameQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, userName: user.UserName));

        Assert.Equivalent(user.UserName, userNameQueried.UserName);
    }

    [Fact]
    public async Task Get_QueryUserEmail_Ok()
    {
        var user = await GetUser();

        var userEmailQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, email: user.Email));

        Assert.Equivalent(user.Email, userEmailQueried.Email);
    }

    [Fact]
    public async Task Get_QueryUserPhoneNumber_Ok()
    {
        var user = await GetUser();

        var userPhoneNumberQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, phoneNumber: user.PhoneNumber));

        Assert.Equivalent(user.PhoneNumber, userPhoneNumberQueried.PhoneNumber);
    }

    [Theory]
    [InlineData("Desenvolvedor")]
    [InlineData("Administrador")]
    [InlineData("Assistente Social")]
    [InlineData("Secretário")]
    [InlineData("Voluntário")]
    public async Task Get_QueryRole_Ok(string roleName)
    {
        var user = await GetUser(role: roleName);

        var userRoleQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, role: roleName));

        Assert.Equivalent(user.Id, userRoleQueried.Id);
    }

    [Theory]
    [InlineData("Desenvolvedor")]
    [InlineData("Administrador")]
    [InlineData("Assistente Social")]
    [InlineData("Secretario")]
    [InlineData("Voluntario")]
    public async Task Get_QueryGlobalFilter_Ok(string roleName)
    {
        var user = await GetUser(role: roleName);
        var userQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id));

        var userNameQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, globalFilter: user.UserName));
        var userEmailQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, globalFilter: user.Email));
        var userPhoneNumberQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, globalFilter: user.PhoneNumber));
        var userRoleQueried = await QueryFirst<UserViewModel>(_userClient, new UserFilter(id: user.Id, globalFilter: roleName));

        Assert.Equivalent(userQueried, userNameQueried);
        Assert.Equivalent(userQueried, userEmailQueried);
        Assert.Equivalent(userQueried, userPhoneNumberQueried);
        Assert.Equivalent(userQueried, userRoleQueried);
    }
}
