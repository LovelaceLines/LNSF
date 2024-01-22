using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class RoleTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidRole_Ok()
    {
        // Arrange - Role
        var role = new RolePostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roleClient);

        // Act - Role
        var rolePosted = await Post<RoleViewModel>(_roleClient, role);

        // Act - Count
        var countAfter = await GetCount(_roleClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equal(role.Name, rolePosted.Name);
    }

    [Fact]
    public async Task Delete_StaticRole_BadRequest()
    {
        // Arrange - Count
        var countBefore = await GetCount(_roleClient);

        // Act
        var exceptionDev = await Delete<AppException>(_roleClient, "Desenvolvedor");
        var exceptionAdmin = await Delete<AppException>(_roleClient, "Administrador");
        var exceptionAssistente = await Delete<AppException>(_roleClient, "Assistente Social");
        var exceptionSecr = await Delete<AppException>(_roleClient, "Secretário");
        var exceptionVol = await Delete<AppException>(_roleClient, "Voluntário");

        // Arrange - Count
        var countAfter = await GetCount(_roleClient);

        Assert.Equal(countBefore, countAfter);
        Assert.NotEqual(HttpStatusCode.OK, exceptionDev.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionDev.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionAdmin.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionAdmin.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionAssistente.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionAssistente.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionSecr.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionSecr.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionVol.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionVol.StatusCode);
    }
}
