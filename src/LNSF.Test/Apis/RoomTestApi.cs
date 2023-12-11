using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

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
        var fakeRoom = new RoomPostViewModelFake(number: "").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithZeroBeds_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake(beds: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidRoomWithNegativeStorey_BadResquest()
    {
        // Arrange - Room
        var fakeRoom = new RoomPostViewModelFake(storey: new Random().Next(-int.MaxValue, -1)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exception = await Post<AppException>(_roomClient, fakeRoom);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
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
        var newFakeRoom = new RoomPutViewModelFake(id: room.Id, number: "").Generate();
        var exception = await Put<AppException>(_roomClient, newFakeRoom);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithZeroBeds_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPutViewModelFake(id: room.Id, beds: 0).Generate();
        var exception = await Put<AppException>(_roomClient, newFakeRoom);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task Put_RoomInvalidWithNegativeStorey_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var newFakeRoom = new RoomPutViewModelFake(id: room.Id, storey: new Random().Next(-int.MaxValue, -1)).Generate();
        var exception = await Put<AppException>(_roomClient, newFakeRoom);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.NotEmpty(exception.Message);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
}
