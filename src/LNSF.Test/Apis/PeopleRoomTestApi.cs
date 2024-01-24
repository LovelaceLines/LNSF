using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleRoomTestApi : GlobalClientRequest
{
    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientHotingRoom_OK()
    {
        var room = await GetRoom(beds: 1, available: true);

        var patient = await GetPatient();

        var hosting = await GetHosting(patientId: patient.Id);

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
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

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
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

        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore + 2, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFakeWithPatient, peopleRoomPostedWithPatient);
        Assert.Equivalent(peopleRoomFakeWithEscort, peopleRoomPostedWithEscort);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientActiveHostingRoom_OK()
    {
        var room = await GetRoom(beds: 1, available: true);

        var patient = await GetPatient();

        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientActiveHostingRoomWithEscort_OK()
    {
        var room = await GetRoom(beds: 2, available: true);

        var patient = await GetPatient();
        var escort = await GetEscort();

        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));

        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore + 2, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFakeWithPatient, peopleRoomPostedWithPatient);
        Assert.Equivalent(peopleRoomFakeWithEscort, peopleRoomPostedWithEscort);
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

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: 0).Generate();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidRoom_NotFound()
    {
        var patient = await GetPatient();

        var hosting = await GetHosting(patientId: patient.Id);

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: 0, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
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

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: 0, hostingId: hostingFake.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
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

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
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

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
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

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();

        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
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

        var peopleRoomWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomWithPatient);

        var peopleRoomWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomWithEscort);

        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomWithPatient, peopleRoomPostedWithPatient);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_ValidPeopleHostingRoomWithPatient_OK()
    {
        var peopleRoomFake = await GetPeopleRoom();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var peopleRoomRemoved = await DeleteByBody<PeopleRoomViewModel>(_removePeopleFromRoom, peopleRoomFake);

        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore - 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomRemoved);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_ValidPeopleHostingRoomWithEscort_OK()
    {
        var escort = await GetEscort();
        var patient = await GetPatient();

        var hosting = await GetHosting(patientId: patient.Id);

        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);

        var room = await GetRoom(available: true, beds: 2);

        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(peopleId: patient.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);

        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(peopleId: escort.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var peopleRoomRemovedWithEscort = await DeleteByBody<PeopleRoomViewModel>(_removePeopleFromRoom, peopleRoomFakeWithEscort);

        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore - 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFakeWithEscort, peopleRoomRemovedWithEscort);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_InvalidPeopleHostingRoomWithNotExistEntity_BadRequest()
    {
        var peopleRoom = await GetPeopleRoom();

        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var exceptionRoom = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = 0, PeopleId = peopleRoom.PeopleId, HostingId = peopleRoom.HostingId });
        var exceptionPeople = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = peopleRoom.RoomId, PeopleId = 0, HostingId = peopleRoom.HostingId });
        var exceptionHosting = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = peopleRoom.RoomId, PeopleId = peopleRoom.PeopleId, HostingId = 0 });

        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countPatientsBefore, countPatientsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.NotEqual(HttpStatusCode.OK, exceptionRoom.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionPeople.StatusCode);
        Assert.NotEqual(HttpStatusCode.OK, exceptionHosting.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionRoom.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionPeople.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionHosting.StatusCode);
    }
}
