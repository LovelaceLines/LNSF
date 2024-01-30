using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleRoomHostingTestApiDelete : GlobalClientRequest
{
    [Fact]
    public async Task RemovePeopleFromRoom_ValidPeopleHostingWithPatient_OK()
    {
        var prhFake = await GetPeopleRoomHosting();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var peopleRoomHostingRemoved = await DeleteByBody<PeopleRoomHostingViewModel>(_removePeopleFromRoom, prhFake);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomHostingsBefore - 1, countPeopleRoomHostingsAfter);
        Assert.Equivalent(prhFake, peopleRoomHostingRemoved);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_ValidPeopleHostingWithEscort_OK()
    {
        var escort = await GetEscort();
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id);
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);
        var room = await GetRoom(available: true, beds: 2);
        var prhFakeWithPatient = new PeopleRoomHostingViewModelFake(peopleId: patient.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomHostingPostedWithPatient = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFakeWithPatient);
        var prhFakeWithEscort = new PeopleRoomHostingViewModelFake(peopleId: escort.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomHostingPostedWithEscort = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFakeWithEscort);

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var peopleRoomHostingRemovedWithEscort = await DeleteByBody<PeopleRoomHostingViewModel>(_removePeopleFromRoom, peopleRoomHostingPostedWithEscort);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomHostingsBefore - 1, countPeopleRoomHostingsAfter);
        Assert.Equivalent(peopleRoomHostingPostedWithEscort, peopleRoomHostingRemovedWithEscort);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_InvalidPeopleHostingWithNotExistEntity_BadRequest()
    {
        var peopleRoomHosting = await GetPeopleRoomHosting();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var exceptionRoom = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomHostingViewModel() { RoomId = 0, PeopleId = peopleRoomHosting.PeopleId, HostingId = peopleRoomHosting.HostingId });
        var exceptionPeople = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomHostingViewModel() { RoomId = peopleRoomHosting.RoomId, PeopleId = 0, HostingId = peopleRoomHosting.HostingId });
        var exceptionHosting = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomHostingViewModel() { RoomId = peopleRoomHosting.RoomId, PeopleId = peopleRoomHosting.PeopleId, HostingId = 0 });
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomHostingsBefore, countPeopleRoomHostingsAfter);
        Assert.NotEqual(HttpStatusCode.OK, exceptionRoom.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionPeople.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionHosting.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionRoom.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionPeople.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionHosting.StatusCode);
    }
}
