using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Xunit;

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

        // Arrange - Tour
        var tourFake = new TourViewModelFake().Generate();
        var tourMapped = _mapper.Map<TourPostViewModel>(tourFake);
        tourMapped.PeopleId = peoplePosted.Id;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_tourClient);

        // Act
        var tourPosted = await Post<TourViewModel>(_tourClient, tourMapped);
        var quantityAfter = await GetQuantity(_tourClient);

        // Assert
        Assert.Equal(quantityBefore + 1, quantityAfter);
        Assert.Equivalent(tourMapped.PeopleId, tourPosted.PeopleId);
    }

    [Fact]
    public async Task Post_InvalidTourWithOpenTour_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var openTourFake = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        openTourFake.PeopleId = peoplePosted.Id;
        var openTourPosted = await Post<TourViewModel>(_tourClient, openTourFake);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_tourClient);

        // Act
        var otherTourFake = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        otherTourFake.PeopleId = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(() => Post<TourViewModel>(_tourClient, otherTourFake));
        var quantityAfter = await GetQuantity(_tourClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_ValidTourWithCloseTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var openTourFake = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        openTourFake.PeopleId = peoplePosted.Id;
        var tourPosted = await Post<TourViewModel>(_tourClient, openTourFake);

        // Arrange - Tour
        var closeTourFake = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        closeTourFake.Id = tourPosted.Id;
        closeTourFake.PeopleId = peoplePosted.Id;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_tourClient);

        // Act
        var tourPut = await Put<TourViewModel>(_putAllClient, closeTourFake);
        var quantityAfter = await GetQuantity(_tourClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
        Assert.Equal(closeTourFake.Id, tourPut.Id);
        Assert.Equal(closeTourFake.PeopleId, tourPut.PeopleId);
    }

    [Fact]
    public async Task Put_InvalidTourWithNonExistentPeopleId_BadRequest()
    {
        // Arrange - Tour
        var closeTourFake = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        closeTourFake.PeopleId = new Random().Next(9999, 99999);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_tourClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Put<TourViewModel>(_putAllClient, closeTourFake));
        var quantityAfter = await GetQuantity(_tourClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task PutAll_ValidTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Tour
        var tourFake = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake.PeopleId = peoplePosted.Id;
        var tourPosted = await Post<TourViewModel>(_tourClient, tourFake);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_tourClient);
 
        // Act
        var otherTourFake = _mapper.Map<TourViewModel>(new TourViewModelFake().Generate());
        otherTourFake.Id = tourPosted.Id;
        otherTourFake.PeopleId = tourPosted.PeopleId;
        var otherTourPuted = await Put<TourViewModel>(_putAllClient, otherTourFake); 
        var quantityAfter = await GetQuantity(_tourClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }
}