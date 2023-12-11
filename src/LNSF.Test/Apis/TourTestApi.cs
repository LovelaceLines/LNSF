using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class TourTestApi : GlobalClientRequest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Post_ValidTour_Ok(string? note)
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Tour
        var tour = new TourPostViewModelFake(people.Id, note: note).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Act - Tour
        var tourPosted = await Post<TourViewModel>(_tourClient, tour);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Act - Query
        var query = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tourPosted.Id));
        var tourQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(tour, tourPosted);
        Assert.Equivalent(tourPosted, tourQueried);
        Assert.Equal(tourPosted.Output.Date, DateTime.Now.Date);
        Assert.Equal(tourPosted.Output.Hour, DateTime.Now.Hour);
        Assert.Equal(tourPosted.Output.Minute, DateTime.Now.Minute);
    }

    [Fact]
    public async Task Post_InvalidTourWithPeopleOwningAnOpenTour_Conflict()
    {
        // Arrange - Tour
        var openTour1 = await GetTour();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Arrange - Tour
        var openTour2 = new TourPostViewModelFake(openTour1.PeopleId).Generate();

        // Act - Tour
        var exception = await Post<AppException>(_tourClient, openTour2);
        
        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task Put_ValidTour_Ok()
    {
        // Arrange - Tour
        var openTour = await GetTour();

        // Arrange - Tour
        var closeTour = new TourPutViewModelFake(openTour.Id, openTour.PeopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Act - Tour
        var tourPuted = await Put<TourViewModel>(_putAllClient, closeTour);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Act - Query
        var query = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tourPuted.Id));
        var tourQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTour, tourPuted);
        Assert.Equivalent(tourPuted, tourQueried);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_InvalidTourWithNonExistentPeopleId_NotFound(int peopleId)
    {
        // Arrange - Tour
        var tour = await GetTour();
        var tourToPut = new TourPutViewModelFake(tour.Id, peopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Act - Tour
        var exception = await Put<AppException>(_putAllClient, tourToPut);
        
        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task PutAll_ValidOpenTour_Ok()
    {
        // Arrange - Tour
        var openTour = await GetTour();
        var closeTourToPutAll = new TourPutViewModelFake(openTour.Id, openTour.PeopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var closeTourPuted = await Put<TourViewModel>(_putAllClient, closeTourToPutAll);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Act - Query
        var query = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: closeTourPuted.Id));
        var closeTourQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTourToPutAll, closeTourPuted);
        Assert.Equivalent(closeTourPuted, closeTourQueried);
    }

    [Fact]
    public async Task PutAll_ValidCloseTour_Ok()
    {
        // Arrange - Tour
        var openTour = await GetTour();
        var closeTour = await GetTour(openTour.Id, openTour.PeopleId);
        var closeTourToPutAll = new TourViewModelFake(closeTour.Id, closeTour.PeopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var closeTourPuted = await Put<TourViewModel>(_putAllClient, closeTourToPutAll);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Act - Query
        var query = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: closeTourPuted.Id));
        var closeTourQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTourToPutAll, closeTourPuted);
        Assert.Equivalent(closeTourPuted, closeTourQueried);
    }

    [Fact]
    public async Task PutAll_InvalidTourWithInvalidDates_BadRequest()
    {
        // Arrange - Tour
        var openTour = await GetTour();
        var closeTour = await GetTour(openTour.Id, openTour.PeopleId);
        var putTourFake = new TourViewModelFake(id: closeTour.Id, peopleId: closeTour.PeopleId, output: new Bogus.DataSets.Date().Future(), input: new Bogus.DataSets.Date().Past()).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var exception = await Put<AppException>(_putAllClient, putTourFake); 

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
}