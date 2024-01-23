using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleRoomTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_PeopleRoomId_Ok()
    {
        // Arrange
        var peopleRoom = await GetPeopleRoom();

        // Act
        var queryId = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId));
        var peopleRoomIdQueried = queryId.First();

        Assert.Equivalent(peopleRoom, peopleRoomIdQueried);
    }

    [Fact]
    public async Task Get_Vacancy_Ok()
    {
        // Arrange
        var room = await GetRoom(beds: 2, available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        // Act
        var queryVacancy = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: peopleRoom.HostingId, vacancy: 2));
        var peopleRoomVacancyQueried = queryVacancy.First();

        Assert.Equivalent(peopleRoom, peopleRoomVacancyQueried);
    }

    [Fact]
    public async Task Get_HasVanc_Ok()
    {
        // Arrange
        var room = await GetRoom(beds: 2, available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        // Act
        var queryHasVac = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: peopleRoom.HostingId, hasVacancy: true));
        var peopleRoomHasVacQueried = queryHasVac.First();

        Assert.Equivalent(peopleRoom, peopleRoomHasVacQueried);
    }

    [Fact]
    public async Task Get_Available_Ok()
    {
        // Arrange
        var room = await GetRoom(beds: 2, available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        // Act
        var queryAvailable = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: peopleRoom.HostingId, available: true));
        var peopleRoomAvailableQueried = queryAvailable.First();

        Assert.Equivalent(peopleRoom, peopleRoomAvailableQueried);
    }

    [Fact]
    public async Task Get_CheckInAndCheckOut_Ok()
    {
        // Arrange - Room
        var room = await GetRoom(beds: 2, available: true);

        // Arrange - People
        var people = await GetPeople();

        // Arrange - Patient
        var patient = await GetPatient(peopleId: people.Id);

        // Arrange - Hosting
        var checkIn = DateTime.Now.AddDays(-1);
        var checkOut = DateTime.Now.AddDays(1);
        var hosting = await GetHosting(patientId: patient.Id, checkIn: checkIn, checkOut: checkOut);

        // Arrange - PeopleRoom
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, roomId: room.Id, hostingId: hosting.Id);

        // Act - PeopleRoom
        var queryCheckInAndCheckOut = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: room.Id, hostingId: hosting.Id, checkIn: checkIn.AddSeconds(-1), checkOut: checkOut.AddSeconds(1)));
        var peopleRoomCheckInAndCheckOutQueried = queryCheckInAndCheckOut.First();

        Assert.Equivalent(peopleRoom, peopleRoomCheckInAndCheckOutQueried);
    }

    [Fact]
    public async Task Get_GetPeople_Ok()
    {
        // Arrange
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, hostingId: hosting.Id);

        // Act
        var queryPeople = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, getPeople: true));
        var peopleRoomPeopleQueried = queryPeople.First();

        Assert.Equivalent(people, peopleRoomPeopleQueried.People);
    }

    [Fact]
    public async Task Get_GetRoom_Ok()
    {
        // Arrange
        var room = await GetRoom(available: true);
        var peopleRoom = await GetPeopleRoom(roomId: room.Id);

        // Act
        var queryRoom = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, getRoom: true));
        var peopleRoomRoomQueried = queryRoom.First();

        Assert.Equivalent(room, peopleRoomRoomQueried.Room);
    }

    [Fact]
    public async Task Get_GetHosting_Ok()
    {
        // Arrange
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, hostingId: hosting.Id);

        // Act
        var queryHosting = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, getHosting: true));
        var peopleRoomHostingQueried = queryHosting.First();

        Assert.Equivalent(hosting, peopleRoomHostingQueried.Hosting);
    }

    [Fact]
    public async Task Get_GlobalFilter_Ok()
    {
        // Arrange
        var room = await GetRoom(beds: 2, available: true);
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var peopleRoom = await GetPeopleRoom(peopleId: people.Id, roomId: room.Id, hostingId: hosting.Id);

        // Act
        var queryGlobalFilterPeopleName = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, globalFilter: people.Name));
        var peopleRoomGlobalFilterPeopleNameQueried = queryGlobalFilterPeopleName.First();
        var queryGlobalFilterRoomNumber = await Query<List<PeopleRoomViewModel>>(_peopleRoomClient, new PeopleRoomFilter(peopleId: peopleRoom.PeopleId, roomId: peopleRoom.RoomId, hostingId: peopleRoom.HostingId, globalFilter: room.Number));
        var peopleRoomGlobalFilterRoomNumberQueried = queryGlobalFilterRoomNumber.First();

        Assert.Equivalent(peopleRoom, peopleRoomGlobalFilterPeopleNameQueried);
        Assert.Equivalent(peopleRoom, peopleRoomGlobalFilterRoomNumberQueried);
    }
}
