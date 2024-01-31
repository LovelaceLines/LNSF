using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class TourTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Tour_Ok()
    {
        var openTour = await GetTour();
        var closeTourFake = new TourPutViewModelFake(openTour.Id, openTour.PeopleId).Generate();

        var countBefore = await GetCount(_tourClient);
        var tourPuted = await Put<TourViewModel>(_putAllClient, closeTourFake);
        var countAfter = await GetCount(_tourClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTourFake, tourPuted);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task TourWithNonExistentPeopleId_NotFound(int peopleId)
    {
        var openTour = await GetTour();
        var closeTourFake = new TourPutViewModelFake(openTour.Id, peopleId).Generate();

        var countBefore = await GetCount(_tourClient);
        var exception = await Put<AppException>(_putAllClient, closeTourFake);
        var countAfter = await GetCount(_tourClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
