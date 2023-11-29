using LNSF.Test.Fakers;
using LNSF.Api.ViewModels;
using Xunit;
using LNSF.Domain.Exceptions;
using System.Net;

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

        // Act
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(peopleFake, peoplePosted);
        Assert.NotEqual(0, peoplePosted.Id);
        Assert.NotEqual(-1, peoplePosted.Id);
    }
    
    [Fact]
    public async Task Post_InvalidPeopleWithEmptyName_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        peopleFake.Name = "";

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<PeopleViewModel>(_peopleClient, peopleFake));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidPeopleWithLessThan15YearsOld_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        peopleFake.BirthDate = DateTime.Now.AddYears(-14);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<PeopleViewModel>(_peopleClient, peopleFake));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidPeopleWithMoreThan128YearsOld_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        peopleFake.BirthDate = DateTime.Now.AddYears(-129);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<PeopleViewModel>(_peopleClient, peopleFake));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidPeopleWithInvalidRG_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        peopleFake.RG = "123456789";

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<PeopleViewModel>(_peopleClient, peopleFake));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidPeopleWithInvalidCPF_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        peopleFake.CPF = "123456789";

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<PeopleViewModel>(_peopleClient, peopleFake));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidPeopleWithInvalidPhone_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        peopleFake.Phone = "123456789";

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Post<PeopleViewModel>(_peopleClient, peopleFake));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_ValidPeople_Ok()
    {
        // Arrange - People
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, fakePeople);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var newFakePeople = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(newFakePeople);
        peopleMapped.Id = peoplePosted.Id;
        var peoplePuted = await Put<PeopleViewModel>(_peopleClient, peopleMapped);
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(peopleMapped, peoplePuted);
    }

    [Fact]
    public async Task Put_InvalidPeopleWithEmptyName_BadRequest()
    {
        // Arrange - People
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, fakePeople);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var newFakePeople = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(newFakePeople);
        peopleMapped.Name = "";
        peopleMapped.Id = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_peopleClient, peopleMapped));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidPeopleWithLessThan15YearsOld_BadRequest()
    {
        // Arrange - People
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, fakePeople);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var newFakePeople = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(newFakePeople);
        peopleMapped.BirthDate = DateTime.Now.AddYears(-14);
        peopleMapped.Id = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_peopleClient, peopleMapped));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidPeopleWithMoreThan128YearsOld_BadRequest()
    {
        // Arrange - People
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, fakePeople);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var newFakePeople = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(newFakePeople);
        peopleMapped.BirthDate = DateTime.Now.AddYears(-129);
        peopleMapped.Id = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_peopleClient, peopleMapped));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidPeopleWithInvalidRG_BadRequest()
    {
        // Arrange - People
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, fakePeople);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var newFakePeople = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(newFakePeople);
        peopleMapped.RG = "123456789";
        peopleMapped.Id = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_peopleClient, peopleMapped));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidPeopleWithInvalidCPF_BadRequest()
    {
        // Arrange - People
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, fakePeople);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var newFakePeople = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(newFakePeople);
        peopleMapped.CPF = "123456789";
        peopleMapped.Id = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_peopleClient, peopleMapped));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidPeopleWithInvalidPhone_BadRequest()
    {
        // Arrange - People
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, fakePeople);

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var newFakePeople = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(newFakePeople);
        peopleMapped.Phone = "123456789";
        peopleMapped.Id = peoplePosted.Id;
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_peopleClient, peopleMapped));
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
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
        var hostingFake = await GetHosting(escortIds: new List<int> { escortFake.Id });

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
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
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
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
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
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
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
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
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
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
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
        Assert.NotEqual((int)HttpStatusCode.OK, exception.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exception.StatusCode);
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
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionRoom.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionPeople.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.OK, exceptionHosting.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionRoom.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionPeople.StatusCode);
        Assert.NotEqual((int)HttpStatusCode.InternalServerError, exceptionHosting.StatusCode);
    }
}
    