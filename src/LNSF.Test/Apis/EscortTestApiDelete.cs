using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class EscortTestApiDelete : GlobalClientRequest
{
    // [Fact]
    public async Task Delete_ValidEscort_Ok()
    {
        var escort = await GetEscort();

        var countBefore = await GetCount(_escortClient);
        var escortDeleted = await Delete<EscortViewModel>(_escortClient, escort.Id);
        var countAfter = await GetCount(_escortClient);

        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equal(escort.Id, escortDeleted.Id);
    }

    // [Fact]
    public async Task Delete_EscortWithNotExistsId_NotFound()
    {
        var countBefore = await GetCount(_escortClient);
        var exception = await Delete<AppException>(_escortClient, 0);
        var countAfter = await GetCount(_escortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
