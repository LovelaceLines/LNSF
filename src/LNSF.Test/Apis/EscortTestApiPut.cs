using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class EscortTestApiPut : GlobalClientRequest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task EscortWithOutherNonExistsPeopleId_NotFound(dynamic peopleId)
    {
        var escort = await GetEscort();
        var escortToPut = new EscortViewModelFake(id: escort.Id, peopleId: peopleId).Generate();

        var countBefore = await GetCount(_escortClient);
        var exception = await Put<AppException>(_escortClient, escortToPut);
        var countAfter = await GetCount(_escortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task EscortWithUsePeopleId_NotFound()
    {
        var escort1 = await GetEscort();
        var escort2 = await GetEscort();
        var escortFake = new EscortViewModelFake(id: escort1.Id, peopleId: escort2.PeopleId).Generate();

        var countBefore = await GetCount(_escortClient);
        var exception = await Put<AppException>(_escortClient, escortFake);
        var countAfter = await GetCount(_escortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
