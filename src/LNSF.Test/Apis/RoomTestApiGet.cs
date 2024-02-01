using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class RoomTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryValidRoomId_Ok()
    {
        var room = await GetRoom();

        var queryId = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id));
        var RoomIdQueried = queryId.First();

        Assert.Equivalent(room, RoomIdQueried);
    }

    [Fact]
    public async Task QueryValidRoomNumber_Ok()
    {
        var room = await GetRoom();

        var queryNumber = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, number: room.Number));
        var RoomNumberQueried = queryNumber.First();

        Assert.Equivalent(room, RoomNumberQueried);
    }

    [Fact]
    public async Task QueryValidRoomBeds_Ok()
    {
        var room = await GetRoom();

        var queryBeds = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, beds: room.Beds));
        var RoomBedsQueried = queryBeds.First();

        Assert.Equivalent(room, RoomBedsQueried);
    }

    [Fact]
    public async Task QueryValidRoomStorey_Ok()
    {
        var room = await GetRoom();

        var queryStorey = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, storey: room.Storey));
        var RoomStoreyQueried = queryStorey.First();

        Assert.Equivalent(room, RoomStoreyQueried);
    }

    [Fact]
    public async Task QueryValidRoomAvailable_Ok()
    {
        var room = await GetRoom();

        var queryAvailable = await Query<List<RoomViewModel>>(_roomClient, new RoomFilter(id: room.Id, available: room.Available));
        var RoomAvailableQueried = queryAvailable.First();

        Assert.Equivalent(room, RoomAvailableQueried);
    }
}
