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
        var peopleQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(peopleFake, peoplePosted);
        Assert.Equivalent(peoplePosted, peopleQueried);
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
        var peopleQueried = query.FirstOrDefault();

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

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task AddPeopleToRoom_ValidPeoplePatientAndHotingAndRoom_OK(int beds)
    {
        // Arrange - Room
        var roomFake = await GetRoom(beds: beds, available: true);

        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id);

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

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
        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task AddPeopleToRoom_ValidPeopleEscortAndHotingAndRoom_OK(int beds)
    {
        // Arrange - Room
        var roomFake = await GetRoom(beds: beds, available: true);

        // Arrange - People
        var escortFake = await GetEscort();

        // Arrange - Hosting
        var hostingFake = await GetHosting();

        // Arrange - Add Escort to Hosting
        var hostingEscort = await Post<HostingEscortViewModel>(_addEscortToHostingClient, new HostingEscortViewModel() { HostingId = hostingFake.Id, EscortId = escortFake.Id });

        // Arrange - PeopleRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: escortFake.PeopleId, hostingId: hostingFake.Id).Generate();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var peopleRoomPosted = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);

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
        Assert.Equal(countPeopleRoomsBefore + 1, countPeopleRoomsAfter);
        Assert.Equivalent(peopleRoomFake, peopleRoomPosted);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidHostingWithValidPeoplePatientAndRoom_BadRequest()
    {
        // Arrange - Room
        var roomFake = await GetRoom(available: true);

        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: 0).Generate();
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
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidHostingWithValidPeopleEscortAndRoom_BadRequest()
    {
        // Arrange - Room
        var roomFake = await GetRoom(available: true);

        // Arrange - People
        var escortFake = await GetEscort();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countEscortsBefore = await GetCount(_escortClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);
        var countHostingsBefore = await GetCount(_hostingClient);

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: escortFake.PeopleId, hostingId: 0).Generate();
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countEscortsAfter = await GetCount(_escortClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);
        var countHostingsAfter = await GetCount(_hostingClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countEscortsBefore, countEscortsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeopleWithValidHostingAndRoom_BadRequest()
    {
        // Arrange - Room
        var roomFake = await GetRoom(available: true);

        // Arrange - Hosting
        var hostingFake = await GetHosting();

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: 0, hostingId: hostingFake.Id).Generate();
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake);

        // Act - Count
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);
        var countHostingsAfter = await GetCount(_hostingClient);
        var countPeopleRoomsAfter = await GetCount(_peopleRoomClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(countHostingsBefore, countHostingsAfter);
        Assert.Equal(countPeopleRoomsBefore, countPeopleRoomsAfter);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
    
    [Fact]
    public async Task AddPeopleToRoom_InvalidRoomWithValidPeoplePatientAndHosting_BadRequest()
    {
        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: 0, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();
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
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
    
    [Fact]
    public async Task AddPeopleToRoom_InvalidRoomNotAvailableWithValidPeopleAndHosting_BadRequest()
    {
        // Arrange - Room
        var roomFake = await GetRoom(available: false);

        // Arrange - People
        var patientFake = await GetPatient();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();
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
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }
    
    [Fact]
    public async Task AddPeopleToRoom_InvalidRoomWithoutVacancyAvaWithValidPeopleAndHosting_BadRequest()
    {
        // Arrange - Room
        var roomFake = await GetRoom(available: true, beds: 1);

        // Arrange - People
        var patientFake = await GetPatient();
        var escortFake = await GetEscort();

        // Arrange - Hosting
        var hostingFake = await GetHosting(patientId: patientFake.Id, escortIds: new List<int> { escortFake.Id });

        // Arrange - PeopleRoom
        var peopleRoomFake1 = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: patientFake.PeopleId, hostingId: hostingFake.Id).Generate();
        var peopleRoomPosted1 = await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake1);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);
        var countPatientsBefore = await GetCount(_patientClient);
        var countHostingsBefore = await GetCount(_hostingClient);
        var countPeopleRoomsBefore = await GetCount(_peopleRoomClient);

        // Act - AddPeopleToRoom
        var peopleRoomFake2 = new PeopleRoomViewModelFake(roomId: roomFake.Id, peopleId: escortFake.PeopleId, hostingId: hostingFake.Id).Generate();
        var exception = await Post<AppException>(_addPeopleToRoomClient, peopleRoomFake2);

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
        Assert.Equivalent(peopleRoomFake1, peopleRoomPosted1);
        Assert.NotEqual(HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_ValidPeopleRoom_OK()
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
    public async Task RemovePeopleFromRoom_InvalidPeopleRoom_BadRequest()
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
        var exceptionRoom = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel(){ RoomId = 0, PeopleId = peopleRoomFake.PeopleId, HostingId = peopleRoomFake.HostingId });
        var exceptionPeople = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel(){ RoomId = peopleRoomFake.RoomId, PeopleId = 0, HostingId = peopleRoomFake.HostingId });
        var exceptionHosting = await DeleteByBody<AppException>(_removePeopleFromRoom, new PeopleRoomViewModel(){ RoomId = peopleRoomFake.RoomId, PeopleId = peopleRoomFake.PeopleId, HostingId = 0 });

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
    