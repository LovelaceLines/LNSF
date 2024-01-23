using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class UserTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidUserId_Ok()
    {
        // Arrange - User
        var user = await GetUser();

        // Act - User
        var queryId = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id));
        var UserIdQueried = queryId.First();

        Assert.Equivalent(user, UserIdQueried);
    }

    [Fact]
    public async Task Get_ValidUserName_Ok()
    {
        // Arrange - User
        var user = await GetUser();

        // Act - User
        var queryUserName = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, userName: user.UserName));
        var UserNameQueried = queryUserName.First();

        Assert.Equivalent(user, UserNameQueried);
    }

    [Fact]
    public async Task Get_ValidEmail_Ok()
    {
        // Arrange - User
        var user = await GetUser();

        // Act - User
        var queryEmail = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, email: user.Email));
        var EmailQueried = queryEmail.First();

        Assert.Equivalent(user, EmailQueried);
    }

    [Fact]
    public async Task Get_ValidPhoneNumber_Ok()
    {
        // Arrange - User
        var user = await GetUser();

        // Act - User
        var queryPhoneNumber = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, phoneNumber: user.PhoneNumber));
        var PhoneNumberQueried = queryPhoneNumber.First();

        Assert.Equivalent(user, PhoneNumberQueried);
    }

    [Fact]
    public async Task Get_ValidRole_Ok()
    {
        // Arrange - User
        var user = await GetUser(role: "Desenvolvedor");

        // Act - User
        var queryRole = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, role: "Desenvolvedor"));
        var RoleQueried = queryRole.First();

        Assert.Equivalent(user, RoleQueried);
    }

    [Fact]
    public async Task Get_ValidGlobalFilter_Ok()
    {
        // Arrange - User
        var user = await GetUser(role: "Desenvolvedor");

        // Act - User
        var queryGlobalFilterUserName = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, globalFilter: user.UserName));
        var queryGlobalFilterEmail = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, globalFilter: user.Email));
        var queryGlobalFilterPhoneNumber = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, globalFilter: user.PhoneNumber));
        var queryGlobalFilterRole = await Query<List<UserViewModel>>(_userClient, new UserFilter(id: user.Id, globalFilter: "Desenvolvedor"));

        var GlobalFilterUserNameQueried = queryGlobalFilterUserName.First();
        var GlobalFilterEmailQueried = queryGlobalFilterEmail.First();
        var GlobalFilterPhoneNumberQueried = queryGlobalFilterPhoneNumber.First();
        var GlobalFilterRoleQueried = queryGlobalFilterRole.First();

        Assert.Equivalent(user, GlobalFilterUserNameQueried);
        Assert.Equivalent(user, GlobalFilterEmailQueried);
        Assert.Equivalent(user, GlobalFilterPhoneNumberQueried);
        Assert.Equivalent(user, GlobalFilterRoleQueried);
    }
}
