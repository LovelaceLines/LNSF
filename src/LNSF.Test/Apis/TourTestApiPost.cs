using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class TourTestApiPost : GlobalClientRequest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Post_Tour_Ok(string? note)
    {
        var people = await GetPeople();
        var tourFake = new TourPostViewModelFake(people.Id, note: note).Generate();

        var countBefore = await GetCount(_tourClient);
        var tourPosted = await Post<TourViewModel>(_tourClient, tourFake);
        var countAfter = await GetCount(_tourClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(tourFake, tourPosted);
    }

    [Fact]
    public async Task Post_TourWithPeopleHaveAnOpenTour_Conflict()
    {
        var openTour = await GetTour();
        var openTourFake = new TourPostViewModelFake(openTour.PeopleId).Generate();

        var countBefore = await GetCount(_tourClient);
        var exception = await Post<AppException>(_tourClient, openTourFake);
        var countAfter = await GetCount(_tourClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}