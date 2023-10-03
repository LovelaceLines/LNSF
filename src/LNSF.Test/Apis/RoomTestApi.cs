using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Xunit;

namespace LNSF.Test.Apis;

public class RoomTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidRoom_Ok()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore + 1, quantityAfter);
        Assert.Equivalent(fakeRoom, roomPosted);
        Assert.NotEqual(0, roomPosted.Id);
    }

    [Fact]
    public async Task Post_InvalidRoomWithEmptyNumber_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Number = "";

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<RoomViewModel>(_roomClient, fakeRoom));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Post_InvalidRoomWithZeroBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Beds = 0;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<RoomViewModel>(_roomClient, fakeRoom));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Post_InvalidRoomWithNegativeStorey_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Storey = new Random().Next(-int.MaxValue, -1);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<RoomViewModel>(_roomClient, fakeRoom));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Post_InvalidRoomWithNegativeOccupation_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Occupation = new Random().Next(-int.MaxValue, -1);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<RoomViewModel>(_roomClient, fakeRoom));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Post_InvalidRoomWithOccupationGreaterThanBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Occupation = fakeRoom.Beds + 1;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        await Assert.ThrowsAsync<Exception>(() => Post<RoomViewModel>(_roomClient, fakeRoom));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Post_InvalidRoomWithAvailableButNoVacantBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Available = true;
        fakeRoom.Occupation = fakeRoom.Beds;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<RoomViewModel>(_roomClient, fakeRoom));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Post_InvalidRoomWithAvailableButMoreOccupantsThanBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Available = true;
        fakeRoom.Occupation = fakeRoom.Beds + 1;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<RoomViewModel>(_roomClient, fakeRoom));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomValid_Ok()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        var roomPuted = await Put<RoomViewModel>(_roomClient, roomMapped);
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equivalent(newFakeRoom, roomPuted);
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalidWithEmptyNumber_BadRequest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        roomMapped.Number = "";
        await Assert.ThrowsAsync<Exception>(() => Put<RoomViewModel>(_roomClient, roomMapped));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalidWithZeroBeds_BadRequest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        roomMapped.Beds = 0;
        await Assert.ThrowsAsync<Exception>(() => Put<RoomViewModel>(_roomClient, roomMapped));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalidWithNegativeStorey_BadRequest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        roomMapped.Storey = new Random().Next(-int.MaxValue, -1);
        await Assert.ThrowsAsync<Exception>(() => Put<RoomViewModel>(_roomClient, roomMapped));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalidWithNegativeOccupation_BadRequest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        roomMapped.Occupation = new Random().Next(-int.MaxValue, -1);
        await Assert.ThrowsAsync<Exception>(() => Put<RoomViewModel>(_roomClient, roomMapped));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalidWithOccupationGreaterThanBeds_BadRequest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        roomMapped.Occupation = roomMapped.Beds + 1;
        await Assert.ThrowsAsync<Exception>(() => Put<RoomViewModel>(_roomClient, roomMapped));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalidWithAvailableButNoVacantBeds_BadRequest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        roomMapped.Available = true;
        roomMapped.Occupation = roomMapped.Beds;
        await Assert.ThrowsAsync<Exception>(() => Put<RoomViewModel>(_roomClient, roomMapped));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalidWithAvailableButMoreOccupantsThanBeds_BadRequest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = roomPosted.Id;
        roomMapped.Available = true;
        roomMapped.Occupation = roomMapped.Beds + 1;
        await Assert.ThrowsAsync<Exception>(() => Put<RoomViewModel>(_roomClient, roomMapped));
        var quantityAfter = await GetQuantity(_roomClient);

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }
}
