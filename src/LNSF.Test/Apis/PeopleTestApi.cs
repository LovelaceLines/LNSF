using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApi : GlobalClientRequest
{
    public readonly HttpClient _addPeopleToRoomClient = new() { BaseAddress = new Uri($"{BaseUrl}People/add-people-to-room/") };
    public readonly HttpClient _removePeopleFromRoom = new() { BaseAddress = new Uri($"{BaseUrl}People/remove-people-from-room/") };

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

    [Fact]
    public async Task AddPeopleToRoom_ValidPeopleAndValidRoomWithManyVacancies_OK()
    {
        // Arrange - Room
        var roomFake = new RoomPostViewModelFake().Generate();
        if (roomFake.Beds < 2) roomFake.Beds = new Random().Next(3, 4);
        roomFake.Occupation = new Random().Next(0, 1);
        roomFake.Available = true;
        var roomPosted = await Post<RoomViewModel>(_roomClient, roomFake);

        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);

        // Act
        var ids = new PeopleAddPeopleToRoomViewModel { PeopleId = peoplePosted.Id, RoomId = roomPosted.Id };
        var peoplePuted = await Put<PeopleViewModel>(_addPeopleToRoomClient, ids);
        var roomsPuted = await Get<List<RoomViewModel>>(_roomClient, roomPosted.Id);
        var roomPuted = roomsPuted.First();
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.True(roomPuted.Available);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(peoplePuted.RoomId, roomPosted.Id);
        Assert.Equal(roomPosted.Occupation + 1, roomPuted.Occupation);
        Assert.Equal(roomPosted.Beds, roomPuted.Beds);
        Assert.Equal(peoplePosted.Id, peoplePuted.Id);
        Assert.Equal(roomPosted.Id, roomPuted.Id);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeopleAndValidRoomWithOneVacancy_OK()
    {
        // Arrange - Room
        var roomFake = new RoomPostViewModelFake().Generate();
        roomFake.Occupation = roomFake.Beds - 1;
        roomFake.Available = true;
        var roomPosted = await Post<RoomViewModel>(_roomClient, roomFake);

        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);

        // Act
        var ids = new PeopleAddPeopleToRoomViewModel { PeopleId = peoplePosted.Id, RoomId = roomPosted.Id };
        var peoplePuted = await Put<PeopleViewModel>(_addPeopleToRoomClient, ids);
        var roomsPuted = await Get<List<RoomViewModel>>(_roomClient, roomPosted.Id);
        var roomPuted = roomsPuted.First();
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.False(roomPuted.Available);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
        Assert.Equal(peoplePuted.RoomId, roomPosted.Id);
        Assert.Equal(roomPosted.Occupation + 1, roomPuted.Occupation);
        Assert.Equal(roomPosted.Beds, roomPuted.Beds);
        Assert.Equal(peoplePosted.Id, peoplePuted.Id);
        Assert.Equal(roomPosted.Id, roomPuted.Id);
    }

    [Fact]
    public async Task AddPeopleToRoom_InvalidPeopleWithExistingRoomIdAndValidRoom_BadRequest()
    {
        // Arrange - Room
        var roomFake = new RoomPostViewModelFake().Generate();
        if (roomFake.Occupation == roomFake.Beds) roomFake.Occupation--;
        roomFake.Available = true;
        var roomPosted = await Post<RoomViewModel>(_roomClient, roomFake);

        var otherRoomFake = new RoomPostViewModelFake().Generate();
        if (otherRoomFake.Occupation == otherRoomFake.Beds) otherRoomFake.Occupation--;
        otherRoomFake.Available = true;
        var otherRoomPosted = await Post<RoomViewModel>(_roomClient, otherRoomFake);

        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - AddPeopleToRoom
        var ids = new PeopleAddPeopleToRoomViewModel { PeopleId = peoplePosted.Id, RoomId = otherRoomPosted.Id };
        var peoplePuted = await Put<PeopleViewModel>(_addPeopleToRoomClient, ids);
        var otherRoomPuted = await Get<List<RoomViewModel>>(_roomClient, otherRoomPosted.Id);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);

        // Act
        ids = new PeopleAddPeopleToRoomViewModel { PeopleId = peoplePosted.Id, RoomId = roomPosted.Id };
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_addPeopleToRoomClient, ids));
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_ValidPeopleAndInvalidRoomWithAvailableFalse_BadRequest()
    {
        // Arrange - Room
        var roomFake = new RoomPostViewModelFake().Generate();
        roomFake.Available = false;
        var roomPosted = await Post<RoomViewModel>(_roomClient, roomFake);

        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);

        // Act
        var ids = new PeopleAddPeopleToRoomViewModel { PeopleId = peoplePosted.Id, RoomId = roomPosted.Id };
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_addPeopleToRoomClient, ids));
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countRoomsBefore, countRoomsAfter);
        Assert.Equal(countPeoplesBefore, countPeoplesAfter);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_PeopleValid_OK()
    {
        // Arrange - Room
        var roomFake = new RoomPostViewModelFake().Generate();
        if (roomFake.Occupation == roomFake.Beds) roomFake.Occupation--;
        roomFake.Available = true;
        var roomPosted = await Post<RoomViewModel>(_roomClient, roomFake);

        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - AddPeopleToRoom
        var ids = new PeopleAddPeopleToRoomViewModel { PeopleId = peoplePosted.Id, RoomId = roomPosted.Id };
        var peoplePuted = await Put<PeopleViewModel>(_addPeopleToRoomClient, ids);
        var roomsPuted = await Get<List<RoomViewModel>>(_roomClient, roomPosted.Id);
        var roomPuted = roomsPuted.First();

        // Arrange - Count
        var countBefore = await GetCount(_peopleClient);

        // Act
        var id = new PeopleRemovePeopleFromRoom { PeopleId = peoplePosted.Id };
        var peopleRemoved = await Put<PeopleViewModel>(_removePeopleFromRoom, id);
        var roomsRemoved = await Get<List<RoomViewModel>>(_roomClient, roomPosted.Id);
        var roomRemoved = roomsRemoved.First();
        var countAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Null(peopleRemoved.RoomId);
        Assert.Equal(roomPuted.Occupation - 1, roomRemoved.Occupation);
        Assert.Equal(roomPuted.Beds, roomRemoved.Beds);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_PeopleInvalidWithoutRoomId_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Count
        var countRoomsBefore = await GetCount(_roomClient);
        var countPeoplesBefore = await GetCount(_peopleClient);

        // Act
        await Assert.ThrowsAsync<Exception>(() => Put<PeopleViewModel>(_peopleClient, peoplePosted.Id));
        var countRoomsAfter = await GetCount(_roomClient);
        var countPeoplesAfter = await GetCount(_peopleClient);

        // Assert
        Assert.Equal(countPeoplesAfter, countPeoplesBefore);
        Assert.Equal(countRoomsAfter, countRoomsBefore);
    }
}
    