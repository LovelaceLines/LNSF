using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleRoomHostingTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_PeopleRoomHostingId_Ok()
    {
        var prh = await GetPeopleRoomHosting();

        var prhQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId));

        Assert.Equivalent(prh, prhQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingVacancy_Ok()
    {
        var room = await GetRoom(available: true);
        var prh = await GetPeopleRoomHosting(roomId: room.Id);

        var prhVacancyQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, vacancy: room.Beds));

        Assert.Equivalent(prh, prhVacancyQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingHasVacancy_Ok()
    {
        var room = await GetRoom(available: true);
        var prh = await GetPeopleRoomHosting(roomId: room.Id);

        var prhHasVacancyQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, hasVacancy: true));

        Assert.Equivalent(prh, prhHasVacancyQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingAvailable_Ok()
    {
        var room = await GetRoom(available: true);
        var prh = await GetPeopleRoomHosting(roomId: room.Id);

        var prhAvailableQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, available: true));

        Assert.Equivalent(prh, prhAvailableQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingCheckInAndCheckOut_Ok()
    {
        var room = await GetRoom(beds: 2, available: true);
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var prh = await GetPeopleRoomHosting(peopleId: people.Id, roomId: room.Id, hostingId: hosting.Id);

        var prhCheckInAndCheckOutQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, checkIn: hosting.CheckIn, checkOut: hosting.CheckOut));

        Assert.Equivalent(prh, prhCheckInAndCheckOutQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingActive_Ok()
    {
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));
        var prh = await GetPeopleRoomHosting(peopleId: patient.PeopleId, hostingId: hosting.Id);

        var prhActiveQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: patient.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, active: true));

        Assert.Equivalent(prh, prhActiveQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingNoActive_Ok()
    {
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(1), checkOut: DateTime.Now.AddDays(2));
        var prh = await GetPeopleRoomHosting(peopleId: patient.PeopleId, hostingId: hosting.Id);

        var prhActiveQueried = await Query<List<PeopleRoomHostingViewModel>>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: patient.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, active: true));

        Assert.Empty(prhActiveQueried);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingGetPeople_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var prh = await GetPeopleRoomHosting(peopleId: people.Id, hostingId: hosting.Id);

        var prhGetPeopleQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, getPeople: true));

        Assert.Equivalent(people, prhGetPeopleQueried.People);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingGetRoom_Ok()
    {
        var room = await GetRoom(available: true);
        var prh = await GetPeopleRoomHosting(roomId: room.Id);

        var prhRoomQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, getRoom: true));

        Assert.Equivalent(room, prhRoomQueried.Room);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingGetHosting_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var prh = await GetPeopleRoomHosting(peopleId: people.Id, hostingId: hosting.Id);

        var prhHostingHostingQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, getHosting: true));

        Assert.Equivalent(hosting, prhHostingHostingQueried.Hosting);
    }

    [Fact]
    public async Task Get_PeopleRoomHostingGlobalFilter_Ok()
    {
        var room = await GetRoom(beds: 2, available: true);
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var hosting = await GetHosting(patientId: patient.Id);
        var prh = await GetPeopleRoomHosting(peopleId: people.Id, roomId: room.Id, hostingId: hosting.Id);

        var prhGlobalFilterPeopleNameQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, globalFilter: people.Name));
        var prhGlobalFilterRoomNumberQueried = await QueryFirst<PeopleRoomHostingViewModel>(_peopleRoomHostingClient, new PeopleRoomHostingFilter(peopleId: prh.PeopleId, roomId: prh.RoomId, hostingId: prh.HostingId, globalFilter: room.Number));

        Assert.Equivalent(prh, prhGlobalFilterPeopleNameQueried);
        Assert.Equivalent(prh, prhGlobalFilterRoomNumberQueried);
    }
}
