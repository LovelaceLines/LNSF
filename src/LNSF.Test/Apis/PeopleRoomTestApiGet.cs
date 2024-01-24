using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleRoomTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_PeopleRoomId_Ok()
    {
        var peopleRoom = await GetPeopleRoom();

        var peopleRoomIdQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId));

        Assert.Equivalent(peopleRoom, peopleRoomIdQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomVacancy_Ok()
    {
        var room = await GetRoom(available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        var peopleRoomVacancyQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: peopleRoom.HostingId, vacancy: room.Beds));

        Assert.Equivalent(peopleRoom, peopleRoomVacancyQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHasVacancy_Ok()
    {
        var room = await GetRoom(available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        var peopleRoomHasVacancyQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: peopleRoom.HostingId, hasVacancy: true));

        Assert.Equivalent(peopleRoom, peopleRoomHasVacancyQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomAvailable_Ok()
    {
        var room = await GetRoom(available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        var peopleRoomAvailableQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: peopleRoom.HostingId, available: true));

        Assert.Equivalent(peopleRoom, peopleRoomAvailableQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomCheckInAndCheckOut_Ok()
    {
        var room = await GetRoom(beds: 2, available: true);
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, roomId: room.Id, hostingId: hosting.Id);

        var peopleRoomCheckInAndCheckOutQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: peopleRoom.HostingId, checkIn: hosting.CheckIn, checkOut: hosting.CheckOut));

        Assert.Equivalent(peopleRoom, peopleRoomCheckInAndCheckOutQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomGetPeople_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, hostingId: hosting.Id);

        var peopleRoomGetPeopleQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, getPeople: true));

        Assert.Equivalent(people, peopleRoomGetPeopleQueried.People);
    }

    [Fact]
    public async Task Get_PeopleRoomGetRoom_Ok()
    {
        var room = await GetRoom(available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        var peopleRoomRoomQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, getRoom: true));

        Assert.Equivalent(room, peopleRoomRoomQueried.Room);
    }

    [Fact]
    public async Task Get_PeopleRoomGetHosting_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, hostingId: hosting.Id);

        var peopleRoomHostingQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, getHosting: true));

        Assert.Equivalent(hosting, peopleRoomHostingQueried.Hosting);
    }

    [Fact]
    public async Task Get_PeopleRoomGlobalFilter_Ok()
    {
        var room = await GetRoom(beds: 2, available: true);
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, roomId: room.Id, hostingId: hosting.Id);

        var peopleRoomGlobalFilterPeopleNameQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, globalFilter: people.Name));
        var peopleRoomGlobalFilterRoomNumberQueried = await QueryFirst<PeopleRoomViewModel>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, globalFilter: room.Number));

        Assert.Equivalent(peopleRoom, peopleRoomGlobalFilterPeopleNameQueried);
        Assert.Equivalent(peopleRoom, peopleRoomGlobalFilterRoomNumberQueried);
    }
}
