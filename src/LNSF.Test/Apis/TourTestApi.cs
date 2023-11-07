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
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Arrange - Tour
        var tourFake = new TourPostViewModelFake().Generate();
        tourFake.PeopleId = peoplePosted.Id;

        // Act - Tour
        var tourPosted = await Post<TourViewModel>(_tourClient, tourFake);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(tourFake.PeopleId, tourPosted.PeopleId);
        Assert.Equal(tourPosted.Output.Date, DateTime.Now.Date);
        Assert.Null(tourPosted.Input);
    }

    [Fact]
    public async Task Post_InvalidTourWithPeopleOwningAnOpenTour_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var openTourFake1 = new TourPostViewModelFake().Generate();
        openTourFake1.PeopleId = peoplePosted.Id;
        var openTourPosted1 = await Post<TourViewModel>(_tourClient, openTourFake1);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Arrange - Tour
        var openTourFake2 = new TourPostViewModelFake().Generate();
        openTourFake2.PeopleId = peoplePosted.Id;

        // Act - Tour
        var exception = await Post<AppException>(_tourClient, openTourFake2);
        
        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Put_ValidTourWithCloseTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var openTourFake = new TourPostViewModelFake().Generate();
        openTourFake.PeopleId = peoplePosted.Id;
        var openTourPosted = await Post<TourViewModel>(_tourClient, openTourFake);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Arrange - Tour
        var closeTourFake = new TourPutViewModelFake().Generate();
        closeTourFake.Id = openTourPosted.Id;
        closeTourFake.PeopleId = peoplePosted.Id;

        // Act - Tour
        var tourPuted = await Put<TourViewModel>(_putAllClient, closeTourFake);

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(closeTourFake, tourPuted);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_InvalidTourWithNonExistentPeopleId_BadRequest(int peopleId)
    {
        // Arrange - Tour
        var tourFake = new TourPostViewModelFake().Generate();
        tourFake.PeopleId = peopleId;

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);

        // Act - Tour
        var exception = await Put<AppException>(_putAllClient, tourFake);
        
        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task PutAll_ValidTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var openTourFake = new TourPostViewModelFake().Generate();
        openTourFake.PeopleId = peoplePosted.Id;
        var openTourPosted = await Post<TourViewModel>(_tourClient, openTourFake);

        var closeTourFake = new TourPutViewModelFake().Generate();
        closeTourFake.Id = openTourPosted.Id;
        closeTourFake.PeopleId = peoplePosted.Id;
        var closeTourPosted = await Put<TourViewModel>(_putAllClient, closeTourFake);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var putTourFake = new TourViewModelFake().Generate();
        putTourFake.Id = closeTourPosted.Id;
        putTourFake.PeopleId = peoplePosted.Id;
        var tourPuted = await Put<TourViewModel>(_putAllClient, putTourFake); 

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(putTourFake, tourPuted);
    }

    [Fact]
    public async Task PutAll_ValidTourWithoutCloseTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var openTourFake = new TourPostViewModelFake().Generate();
        openTourFake.PeopleId = peoplePosted.Id;
        var openTourPosted = await Post<TourViewModel>(_tourClient, openTourFake);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var putTourFake = new TourViewModelFake().Generate();
        putTourFake.Id = openTourPosted.Id;
        putTourFake.PeopleId = peoplePosted.Id;
        var tourPuted = await Put<TourViewModel>(_putAllClient, putTourFake); 

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(putTourFake, tourPuted);
    }

    [Fact]
    public async Task PutAll_InvalidTourWithInvalidDates_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var openTourFake = new TourPostViewModelFake().Generate();
        openTourFake.PeopleId = peoplePosted.Id;
        var openTourPosted = await Post<TourViewModel>(_tourClient, openTourFake);

        var closeTourFake = new TourPutViewModelFake().Generate();
        closeTourFake.Id = openTourPosted.Id;
        closeTourFake.PeopleId = peoplePosted.Id;
        var closeTourPosted = await Put<TourViewModel>(_putAllClient, closeTourFake);

        // Arrange - Count
        var countBefore = await GetCount(_tourClient);
 
        // Act - Tour
        var putTourFake = new TourViewModelFake().Generate();
        putTourFake.Id = openTourPosted.Id;
        putTourFake.PeopleId = peoplePosted.Id;
        putTourFake.Output = new Bogus.DataSets.Date().Future();
        putTourFake.Input = new Bogus.DataSets.Date().Past();
        var exception = await Put<AppException>(_putAllClient, putTourFake); 

        // Act - Count
        var countAfter = await GetCount(_tourClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
    }
}