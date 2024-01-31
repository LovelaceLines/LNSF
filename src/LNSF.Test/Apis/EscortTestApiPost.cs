using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class EscortTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task ValidEscort_Ok()
    {
        var people = await GetPeople();
        var escortFake = new EscortPostViewModel { PeopleId = people.Id };

        var countBefore = await GetCount(_escortClient);
        var escortPosted = await Post<EscortViewModel>(_escortClient, escortFake);
        var countAfter = await GetCount(_escortClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equal(escortFake.PeopleId, escortPosted.PeopleId);
    }

    [Fact]
    public async Task EscortWithNotExistsPeopleId_NotFound()
    {
        var escortFake = new EscortPostViewModelFake(peopleId: 0).Generate();

        var countBefore = await GetCount(_escortClient);
        var exception = await Post<AppException>(_escortClient, escortFake);
        var countAfter = await GetCount(_escortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task EscortWithUsedPeopleId_Conflict()
    {
        var escort = await GetEscort();
        var escortFake = new EscortPostViewModelFake(peopleId: escort.PeopleId).Generate();

        var countBefore = await GetCount(_escortClient);
        var exception = await Post<AppException>(_escortClient, escortFake);
        var countAfter = await GetCount(_escortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
