using LNSF.Test.Fakers;
using LNSF.Api.ViewModels;
using Xunit;
using LNSF.Domain.Exceptions;
using System.Net;

namespace LNSF.Test.Apis;

public class RoomTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidRoom_Ok()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var roomPosted = await Post<RoomViewModel>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(fakeRoom, roomPosted);
        var roomGeted = (await GetById<List<RoomViewModel>>(_roomClient, roomPosted.Id)).First();
        Assert.Equivalent(roomPosted, roomGeted);
    }

    [Fact]
    public async Task Post_InvalidRoomWithEmptyNumber_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Number = "";

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithZeroBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Beds = 0;

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithNegativeStorey_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Storey = new Random().Next(-int.MaxValue, -1);

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithNegativeOccupation_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Occupation = new Random().Next(-int.MaxValue, -1);

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithOccupationGreaterThanBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Occupation = fakeRoom.Beds + 1;

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithAvailableButNoVacantBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Available = true;
        fakeRoom.Occupation = fakeRoom.Beds;

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithAvailableButMoreOccupantsThanBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Available = true;
        fakeRoom.Occupation = fakeRoom.Beds + 1;

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);    

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomValid_Ok()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        var roomPuted = await Put<RoomViewModel>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equivalent(newFakeRoom, roomPuted);
        Assert.Equal(countBefore, countAfter);
        var roomGeted = (await GetById<List<RoomViewModel>>(_roomClient, roomPuted.Id)).First();
        Assert.Equivalent(roomPuted, roomGeted);
    }

    [Fact]
    public async Task Put_RoomInvalidWithEmptyNumber_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        roomMapped.Number = "";
        var exception = await Put<AppException>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithZeroBeds_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        roomMapped.Beds = 0;
        var exception = await Put<AppException>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithNegativeStorey_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        roomMapped.Storey = new Random().Next(-int.MaxValue, -1);
        var exception = await Put<AppException>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithNegativeOccupation_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        roomMapped.Occupation = new Random().Next(-int.MaxValue, -1);
        var exception = await Put<AppException>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithOccupationGreaterThanBeds_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        roomMapped.Occupation = roomMapped.Beds + 1;
        var exception = await Put<AppException>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithAvailableButNoVacantBeds_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        roomMapped.Available = true;
        roomMapped.Occupation = roomMapped.Beds;
        var exception = await Put<AppException>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithAvailableButMoreOccupantsThanBeds_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(newFakeRoom);
        roomMapped.Id = room.Id;
        roomMapped.Available = true;
        roomMapped.Occupation = roomMapped.Beds + 1;
        var exception = await Put<AppException>(_roomClient, roomMapped);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
    }
}
