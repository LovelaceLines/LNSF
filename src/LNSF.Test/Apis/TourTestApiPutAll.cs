using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class TourTestApiPutAll : GlobalClientRequest
{
    [Fact]
    public async Task ValidOpenTour_Ok()
    {
        var openTour = await GetTour();
        var closeTourFake = new TourPutViewModelFake(openTour.Id, openTour.PeopleId).Generate();

        var countBefore = await GetCount(_tourClient);
        var closeTourPuted = await Put<TourViewModel>(_putAllClient, closeTourFake);
        var countAfter = await GetCount(_tourClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTourFake, closeTourPuted);
    }

    [Fact]
    public async Task ValidCloseTour_Ok()
    {
        var openTour = await GetTour();
        var closeTour = await GetTour(openTour.Id, openTour.PeopleId);
        var closeTourToPutAllFake = new TourViewModelFake(closeTour.Id, closeTour.PeopleId).Generate();

        var countBefore = await GetCount(_tourClient);
        var closeTourPuted = await Put<TourViewModel>(_putAllClient, closeTourToPutAllFake);
        var countAfter = await GetCount(_tourClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTourToPutAllFake, closeTourPuted);
    }

    [Fact]
    public async Task InvalidTourWithInvalidDates_BadRequest()
    {
        var openTour = await GetTour();
        var closeTour = await GetTour(openTour.Id, openTour.PeopleId);
        var closeTourToPutAllFake = new TourViewModelFake(id: closeTour.Id, peopleId: closeTour.PeopleId, output: new Bogus.DataSets.Date().Future(), input: new Bogus.DataSets.Date().Past()).Generate();

        var countBefore = await GetCount(_tourClient);
        var exception = await Put<AppException>(_putAllClient, closeTourToPutAllFake);
        var countAfter = await GetCount(_tourClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
}
