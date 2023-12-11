using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
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
        var room = new RoomPostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var roomPosted = await Post<RoomViewModel>(_roomClient, room);

        // Act - Count
        var countAfter = await GetCount(_roomClient);

        // Act - Query
        var query = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: roomPosted.Id));
        var roomQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(room, roomPosted);
        Assert.Equivalent(roomPosted, roomQueried);
    }

    [Fact]
    public async Task Post_InvalidRoom_BadResquest()
    {
        // Arrange - Room
        var roomWithouNumber = new RoomPostViewModelFake(number: "").Generate();
        var roomWithouBads = new RoomPostViewModelFake(beds: 0).Generate();
        var roomWithNegativeStorey = new RoomPostViewModelFake(storey: new Random().Next(-int.MaxValue, -1)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exceptionWithoutNumber = await Post<AppException>(_roomClient, roomWithouNumber);
        var exceptionWithoutBads = await Post<AppException>(_roomClient, roomWithouBads);
        var exceptionWithNegativeStorey = await Post<AppException>(_roomClient, roomWithNegativeStorey);

        // Assert - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutNumber.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutBads.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithNegativeStorey.StatusCode);
    }

    [Fact]
    public async Task Put_RoomValid_Ok()
    {
        // Arrange - Room
        var room = await GetRoom();
        var roomToPut = new RoomViewModelFake(id: room.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var roomPuted = await Put<RoomViewModel>(_roomClient, roomToPut);

        // Act - Count
        var countAfter = await GetCount(_roomClient);

        // Act - Query
        var query = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: roomPuted.Id));
        var roomQueried = query.FirstOrDefault();

        // Assert
        Assert.Equivalent(roomToPut, roomPuted);
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(roomPuted, roomQueried);
    }

    [Fact]
    public async Task Put_RoomInvalidWithEmptyNumber_BadRequest()
    {
        // Arrange - Room
        var room = await GetRoom();

        var roomToPutWithoutNumber = new RoomViewModelFake(id: room.Id, number: "").Generate();
        var roomToPutWithoutBads = new RoomViewModelFake(id: room.Id, beds: 0).Generate();
        var roomToPutWithNegativeStorey = new RoomViewModelFake(id: room.Id, storey: new Random().Next(-int.MaxValue, -1)).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_roomClient);

        // Act - Room
        var exceptionWithoutNumber = await Put<AppException>(_roomClient, roomToPutWithoutNumber);
        var exceptionWithoutBads = await Put<AppException>(_roomClient, roomToPutWithoutBads);
        var exceptionWithNegativeStorey = await Put<AppException>(_roomClient, roomToPutWithNegativeStorey);

        // Arrange - Count
        var countAfter = await GetCount(_roomClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutNumber.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutBads.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithNegativeStorey.StatusCode);
    }
}
