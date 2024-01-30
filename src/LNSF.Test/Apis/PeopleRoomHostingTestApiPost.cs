using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleRoomHostingTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientHotingRoom_OK()
    {
        var room = await GetRoom(beds: 1, available: true);
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id);
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var prhPosted = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFake);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore + 1, countPeopleRoomHostingsAfter);
        Assert.Equivalent(prhFake, prhPosted);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeopleEscortHotingRoomWithEscort_OK()
    {
        var room = await GetRoom(beds: 1, available: true);
        var patient = await GetPatient();
        var escort = await GetEscort();
        var hosting = await GetHosting(patientId: patient.Id);
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var prhPosted = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFake);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore + 1, countPeopleRoomHostingsAfter);
        Assert.Equivalent(prhFake, prhPosted);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeopleEscortHotingRoomWithPatientAndEscort_Ok()
    {
        var room = await GetRoom(beds: 2, available: true);
        var patient = await GetPatient();
        var escort = await GetEscort();
        var hosting = await GetHosting(patientId: patient.Id);
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);
        var prhFakeWithPatient = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var prhFakeWithEscort = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var peopleRoomHostingPostedWithPatient = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFakeWithPatient);
        var peopleRoomHostingPostedWithEscort = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFakeWithEscort);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore + 2, countPeopleRoomHostingsAfter);
        Assert.Equivalent(prhFakeWithPatient, peopleRoomHostingPostedWithPatient);
        Assert.Equivalent(prhFakeWithEscort, peopleRoomHostingPostedWithEscort);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientActiveHosting_OK()
    {
        var room = await GetRoom(beds: 1, available: true);
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var prhPosted = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFake);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore + 1, countPeopleRoomHostingsAfter);
        Assert.Equivalent(prhFake, prhPosted);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientActiveHostingWithEscort_OK()
    {
        var room = await GetRoom(beds: 2, available: true);
        var patient = await GetPatient();
        var escort = await GetEscort();
        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);
        var prhFakeWithPatient = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var prhFakeWithEscort = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        var peopleRoomHostingPostedWithPatient = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFakeWithPatient);
        var peopleRoomHostingPostedWithEscort = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, prhFakeWithEscort);

        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore + 2, countPeopleRoomHostingsAfter);
        Assert.Equivalent(prhFakeWithPatient, peopleRoomHostingPostedWithPatient);
        Assert.Equivalent(prhFakeWithEscort, peopleRoomHostingPostedWithEscort);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidHosting_NotFound()
    {
        var room = await GetRoom(available: true);
        var patient = await GetPatient();
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: 0).Generate();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_addPeopleToRoomClient, prhFake);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countPeopleRoomHostingsBefore, countPeopleRoomHostingsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidRoom_NotFound()
    {
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id);
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: 0, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_addPeopleToRoomClient, prhFake);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore, countPeopleRoomHostingsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidPeople_BadRequest()
    {
        var roomFake = await GetRoom(available: true);
        var patientFake = await GetPatient();
        var hostingFake = await GetHosting(patientId: patientFake.Id);
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: roomFake.Id, peopleId: 0, hostingId: hostingFake.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_addPeopleToRoomClient, prhFake);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore, countPeopleRoomHostingsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithValidHostingPatientButInvalidPeople_BadRequest()
    {
        var room = await GetRoom(available: true);
        var hosting = await GetHosting();
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_addPeopleToRoomClient, prhFake);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore, countPeopleRoomHostingsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithValidHostingPatientAndEscortButInvalidPeople_BadRequest()
    {
        var room = await GetRoom(available: true);
        var hosting = await GetHosting();
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id);
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_addPeopleToRoomClient, prhFake);
        var countPeopleRoomHostingsAfter = await GetCount(_peopleRoomHostingClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomHostingsBefore, countPeopleRoomHostingsAfter);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithRoomNotAvailable_Conflict()
    {
        var roomFake = await GetRoom(available: false);
        var patientFake = await GetPatient();
        var hostingFake = await GetHosting(patientId: patientFake.Id);
        var prhFake = new PeopleRoomHostingViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();

        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_addPeopleToRoomClient, prhFake);
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
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithoutVacancyToPatientAndEscort_Conflict()
    {
        var room = await GetRoom(available: true, beds: 1);
        var patient = await GetPatient();
        var escort = await GetEscort();
        var hosting = await GetHosting(patientId: patient.Id);
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);
        var peopleRoomHostingWithPatient = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomHostingPostedWithPatient = await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, peopleRoomHostingWithPatient);
        var peopleRoomHostingWithEscort = new PeopleRoomHostingViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomHostingsBefore = await GetCount(_peopleRoomHostingClient);
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomHostingWithEscort);
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
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
