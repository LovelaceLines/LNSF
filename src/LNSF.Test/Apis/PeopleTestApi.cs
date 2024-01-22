using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidPeople_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Act - Query
        var query = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: peoplePosted.Id));
        var peopleQueried = query.First();

        peopleQueried.Experience = null;
        peopleQueried.Status = null;

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(peopleFake, peoplePosted);
        Assert.Equivalent(peoplePosted, peopleQueried);
    }

    [Fact]
    public async Task Post_validPeopleWithPhoneNumber_OK()
    {
        // Arrange - People
        var acceptPeople1 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("(##) #####-####"));
        var acceptPeople2 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("####-####"));

        var peopleNotAccept1 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("(##) # ####-####"));
        var peopleNotAccept2 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("(##) ####-####"));
        var peopleNotAccept6 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("## (##) ####-####"));
        var peopleNotAccept7 = new PeoplePostViewModelFake(phone: new Bogus.DataSets.PhoneNumbers().PhoneNumber("## (##) # ####-####"));

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, acceptPeople1.Generate());
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, acceptPeople2.Generate());

        var exception1 = await Post<AppException>(_peopleClient, peopleNotAccept1.Generate());
        var exception2 = await Post<AppException>(_peopleClient, peopleNotAccept2.Generate());
        var exception6 = await Post<AppException>(_peopleClient, peopleNotAccept6.Generate());
        var exception7 = await Post<AppException>(_peopleClient, peopleNotAccept7.Generate());

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception1.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exception2.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exception6.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exception7.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidPeople_BadRequest()
    {
        // Arrange - People
        var peopleWithoutName = new PeoplePostViewModelFake(name: "").Generate();
        var peopleAged14 = new PeoplePostViewModelFake(birthDate: DateTime.Now.AddYears(-14)).Generate();
        var peopleAged129 = new PeoplePostViewModelFake(birthDate: DateTime.Now.AddYears(-129)).Generate();
        var peopleWithInvalidRG = new PeoplePostViewModelFake(rg: "123456789").Generate();
        var peopleWithInvalidCPF = new PeoplePostViewModelFake(cpf: "123456789").Generate();
        var peopleWithInvalidPhone = new PeoplePostViewModelFake(phone: "123456789").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var exceptionWithoutName = await Post<AppException>(_peopleClient, peopleWithoutName);
        var exceptionAged14 = await Post<AppException>(_peopleClient, peopleAged14);
        var exceptionAged129 = await Post<AppException>(_peopleClient, peopleAged129);
        var exceptionWithInvalidRG = await Post<AppException>(_peopleClient, peopleWithInvalidRG);
        var exceptionWithInvalidCPF = await Post<AppException>(_peopleClient, peopleWithInvalidCPF);
        var exceptionWithInvalidPhone = await Post<AppException>(_peopleClient, peopleWithInvalidPhone);

        // Arrange - Count
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged14.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged129.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidRG.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidCPF.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidPhone.StatusCode);
    }

    [Fact]
    public async Task Put_ValidPeople_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        var peopleToPut = new PeopleViewModelFake(people.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var peoplePuted = await Put<PeopleViewModel>(_peopleClient, peopleToPut);

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Act - Query
        var query = await Query<List<PeopleViewModel>>(_peopleClient, new PeopleFilter(id: people.Id));
        var peopleQueried = query.First();

        peopleQueried.Experience = null;
        peopleQueried.Status = null;

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(peopleToPut, peoplePuted);
        Assert.Equivalent(peoplePuted, peopleQueried);
    }

    [Fact]
    public async Task Put_InvalidPeople_BadRequest()
    {
        // Arrange - People
        var people = await GetPeople();

        var peopleToPutWithoutName = new PeopleViewModelFake(people.Id, name: "").Generate();
        var peopleToPutAged14 = new PeopleViewModelFake(people.Id, birthDate: DateTime.Now.AddYears(-14)).Generate();
        var peopleToPutAged129 = new PeopleViewModelFake(people.Id, birthDate: DateTime.Now.AddYears(-129)).Generate();
        var peopleToPutWithInvalidRG = new PeopleViewModelFake(people.Id, rg: "123456789").Generate();
        var peopleToPutWithInvalidCPF = new PeopleViewModelFake(people.Id, cpf: "123456789").Generate();
        var peopleToPutWithInvalidPhone = new PeopleViewModelFake(people.Id, phone: "123456789").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act - People
        var exceptionWithoutName = await Put<AppException>(_peopleClient, peopleToPutWithoutName);
        var exceptionAged14 = await Put<AppException>(_peopleClient, peopleToPutAged14);
        var exceptionAged129 = await Put<AppException>(_peopleClient, peopleToPutAged129);
        var exceptionWithInvalidRG = await Put<AppException>(_peopleClient, peopleToPutWithInvalidRG);
        var exceptionWithInvalidCPF = await Put<AppException>(_peopleClient, peopleToPutWithInvalidCPF);
        var exceptionWithInvalidPhone = await Put<AppException>(_peopleClient, peopleToPutWithInvalidPhone);

        // Act - Count
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged14.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionAged129.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidRG.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidCPF.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithInvalidPhone.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeoplePatientHotingRoom_OK()
    {
        // Arrange - Room
        var room = await GetRoom(beds: 1, available: true);

        // Arrange - People
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(beds: 1, available: true);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(beds: 2, available: true);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(beds: 1, available: true);

        // Arrange - People
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(beds: 2, available: true);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id,
            checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
            checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(available: true);

        // Arrange - People
        var patient = await GetPatient();

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: 0).Generate();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - People
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: 0, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var roomFake = await GetRoom(available: true);

        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: 0, hostingId: hostingFake.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(available: true);

        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(available: true);

        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: 0, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
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
        // Arrange - Room
        var roomFake = await GetRoom(available: false);

        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();

        // Arrange - Count
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
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
        // Arrange - Room
        var room = await GetRoom(available: true, beds: 1);

        // Arrange - People
        var patient = await GetPatient();
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(escortId: escort.Id, hostingId: hosting.Id);

        // Arrange - PeopleRoom
        var peopleRoomWithPatient = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: patient.PeopleId, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomWithPatient);

        var peopleRoomWithEscort = new PeopleRoomViewModelFake(roomId: room.Id, peopleId: escort.PeopleId, hostingId: hosting.Id).Generate();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomWithEscort);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
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
        // Arrange - PeopleRoom
        var peopleRoomFake = await GetPeopleRoom();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var peopleRoomRemoved = await DeleteByBody<PeopleRoomViewModel>(_removePeopleFromRoom, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
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
        // Arrange - People
        var escort = await GetEscort();
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);

        // Arrange - Room
        var room = await GetRoom(available: true, beds: 2);

        // Arrange - PeopleRoom
        var peopleRoomFakeWithPatient = new PeopleRoomViewModelFake(peopleId: patient.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithPatient = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithPatient);

        var peopleRoomFakeWithEscort = new PeopleRoomViewModelFake(peopleId: escort.PeopleId, roomId: room.Id, hostingId: hosting.Id).Generate();
        var peopleRoomPostedWithEscort = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFakeWithEscort);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var peopleRoomRemovedWithEscort = await DeleteByBody<PeopleRoomViewModel>(_removePeopleFromRoom, peopleRoomFakeWithEscort);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
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
        // Arrange - PeopleRoom
        var peopleRoom = await GetPeopleRoom();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - RemovePeopleFromRoom
        var exceptionRoom = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = 0, PeopleId = peopleRoom.PeopleId, HostingId = peopleRoom.HostingId });
        var exceptionPeople = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = peopleRoom.RoomId, PeopleId = 0, HostingId = peopleRoom.HostingId });
        var exceptionHosting = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel() { RoomId = peopleRoom.RoomId, PeopleId = peopleRoom.PeopleId, HostingId = 0 });

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countPatientsAfter = await GetCount(_patientClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
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
