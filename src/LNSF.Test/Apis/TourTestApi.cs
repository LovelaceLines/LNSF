using LNSF.Test.Fakers;
using LNSF.Api.ViewModels;
using Xunit;
using LNSF.Domain.Exceptions;
using System.Net;

namespace LNSF.Test.Apis;

public class TourTestApi : GlobalClientRequest
{
    private readonly HttpClient _putAllClient = new() { BaseAddress = new Uri($"{BaseUrl}Tour/put-all") };

    [Fact]
    public async Task Post_ValidTour_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Arrange - Tour
        var tourFake = new TourPostViewModelFake(people.Id).Generate();

        // Act - Tour
        var tourPosted = await Post<TourViewModel>(_tourClient, tourFake);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(tourFake.PeopleId, tourPosted.PeopleId);
        Assert.Equal(tourPosted.Output.Date, DateTime.Now.Date);
        Assert.Equal(tourPosted.Output.Hour, DateTime.Now.Hour);
        Assert.Equal(tourPosted.Output.Minute, DateTime.Now.Minute);

        var tourGeted = (await GetById<List<TourViewModel>>(_tourClient, tourPosted.Id)).First();
        Assert.Equivalent(tourPosted, tourGeted);
    }

    [Fact]
    public async Task Post_ValidTourWithoutNote_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Arrange - Tour
        var tourFake = new TourPostViewModelFake(people.Id).Generate();
        tourFake.Note = "";

        // Act - Tour
        var tourPosted = await Post<TourViewModel>(_tourClient, tourFake);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(tourFake.PeopleId, tourPosted.PeopleId);
        Assert.Equal(tourPosted.Output.Date, DateTime.Now.Date);
        Assert.Equal(tourPosted.Output.Hour, DateTime.Now.Hour);
        Assert.Equal(tourPosted.Output.Minute, DateTime.Now.Minute);
        
        var tourGeted = (await GetById<List<TourViewModel>>(_tourClient, tourPosted.Id)).First();
        Assert.Equivalent(tourPosted, tourGeted);
    }

    [Fact]
    public async Task Post_InvalidTourWithPeopleOwningAnOpenTour_BadRequest()
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
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Put_ValidTourWithCloseTour_Ok()
    {
        // Arrange - Tour
        var openTour = await GetTour();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Arrange - Tour
        var closeTour = new TourPutViewModelFake(openTour.Id, openTour.PeopleId).Generate();

        // Act - Tour
        var tourPuted = await Put<TourViewModel>(_putAllClient, closeTour);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTour, tourPuted);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_InvalidTourWithNonExistentPeopleId_BadRequest(int peopleId)
    {
        // Arrange - Tour
        var tour = await GetTour();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Act - Tour
        var tourFake = new TourPutViewModelFake(tour.Id, peopleId).Generate();
        var exception = await Put<AppException>(_putAllClient, tourFake);
        
        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task PutAll_ValidTourWithOpenTour_Ok()
    {
        // Arrange - Tour
        var openTour = await GetTour();

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var closeTour = new TourPutViewModelFake(openTour.Id, openTour.PeopleId).Generate();
        var closeTourPuted = await Put<TourViewModel>(_putAllClient, closeTour);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTour, closeTourPuted);
    }

    [Fact]
    public async Task PutAll_ValidTourWithoutCloseTour_Ok()
    {
        // Arrange - Tour
        var openTour = await GetTour();
        var closeTour = await GetTour(openTour.Id, openTour.PeopleId);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var closeTourFake = new TourViewModelFake(closeTour.Id, closeTour.PeopleId).Generate();
        var closeTourPuted = await Put<TourViewModel>(_putAllClient, closeTourFake);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTourFake, closeTourPuted);

        var closeTourGeted = (await GetById<List<TourViewModel>>(_tourClient, closeTourPuted.Id)).First();
        Assert.Equivalent(closeTourPuted, closeTourGeted);
    }

    [Fact]
    public async Task PutAll_InvalidTourWithInvalidDates_BadRequest()
    {
        // Arrange - Tour
        var openTour = await GetTour();
        var closeTour = await GetTour(openTour.Id, openTour.PeopleId);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var putTourFake = new TourViewModelFake(closeTour.Id, closeTour.PeopleId).Generate();
        putTourFake.Output = new Bogus.DataSets.Date().Future();
        putTourFake.Input = new Bogus.DataSets.Date().Past();
        var exception = await Put<AppException>(_putAllClient, putTourFake); 

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
}