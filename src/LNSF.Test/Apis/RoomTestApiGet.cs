using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class RoomTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidRoomId_Ok()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Act - Room
        var queryId = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id));
        var RoomIdQueried = queryId.First();

        Assert.Equivalent(room, RoomIdQueried);
    }

    [Fact]
    public async Task Get_ValidRoomNumber_Ok()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Act - Room
        var queryNumber = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, number: room.Number));
        var RoomNumberQueried = queryNumber.First();

        Assert.Equivalent(room, RoomNumberQueried);
    }

    [Fact]
    public async Task Get_ValidRoomBeds_Ok()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Act - Room
        var queryBeds = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, beds: room.Beds));
        var RoomBedsQueried = queryBeds.First();

        Assert.Equivalent(room, RoomBedsQueried);
    }

    [Fact]
    public async Task Get_ValidRoomStorey_Ok()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Act - Room
        var queryStorey = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, storey: room.Storey));
        var RoomStoreyQueried = queryStorey.First();

        Assert.Equivalent(room, RoomStoreyQueried);
    }

    [Fact]
    public async Task Get_ValidRoomAvailable_Ok()
    {
        // Arrange - Room
        var room = await GetRoom();

        // Act - Room
        var queryAvailable = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, available: room.Available));
        var RoomAvailableQueried = queryAvailable.First();

        Assert.Equivalent(room, RoomAvailableQueried);
    }
}
