using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using AutoMapper;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Newtonsoft.Json;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApi
{
    private readonly Global _global = new();
    private readonly IMapper _mapper;
    private readonly RoomTestApi _room = new();

    public PeopleTestApi()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PeoplePutViewModel, PeoplePostViewModel>().ReverseMap();
            cfg.CreateMap<PeopleViewModel, PeoplePostViewModel>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Post_PeopleValid_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var peoplePosted = await Post(peopleFake);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore + 1, quantityAfter);
        Assert.Equivalent(peopleFake, peoplePosted);
    }

    [Fact]
    public async Task Post_PeopleInvalid_BadRequest()
    {
        // Arrange - People
        var fakePeople1 = new PeoplePostViewModelFake().Generate();
        fakePeople1.Name = "";
        var fakePeople2 = new PeoplePostViewModelFake().Generate();
        fakePeople2.BirthDate = DateTime.Now.AddYears(-14);
        var fakePeople3 = new PeoplePostViewModelFake().Generate();
        fakePeople3.BirthDate = DateTime.Now.AddYears(-129);
        var fakePeople4 = new PeoplePostViewModelFake().Generate();
        fakePeople4.RG = "123456789";
        var fakePeople5 = new PeoplePostViewModelFake().Generate();
        fakePeople5.CPF = "123456789";
        var fakePeople6 = new PeoplePostViewModelFake().Generate();
        fakePeople6.Phone = "123456789";

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // People without name
        await Assert.ThrowsAsync<Exception>(() => Post(fakePeople1));
        // People with less than 14 years old
        await Assert.ThrowsAsync<Exception>(() => Post(fakePeople2));
        // People with more than 129 years old
        await Assert.ThrowsAsync<Exception>(() => Post(fakePeople3));
        // People with invalid RG
        await Assert.ThrowsAsync<Exception>(() => Post(fakePeople4));
        // People with invalid CPF
        await Assert.ThrowsAsync<Exception>(() => Post(fakePeople5));
        // People with invalid phone
        await Assert.ThrowsAsync<Exception>(() => Post(fakePeople6));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_PeopleValid_Ok()
    {
        // Arrange - People
        var fakePeople1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post(fakePeople1);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var fakePeople2 = new PeoplePostViewModelFake().Generate();
        var peopleMapped = _mapper.Map<PeoplePutViewModel>(fakePeople2);
        peopleMapped.Id = peoplePosted1.Id;
        var peoplePuted = await Put(peopleMapped);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
        Assert.Equivalent(peopleMapped, peoplePuted);
    }

    [Fact]
    public async Task Put_PeopleInvalid_BadRequest()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post(peopleFake);

        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peopleMapped1 = _mapper.Map<PeoplePutViewModel>(peopleFake1);
        peopleMapped1.Name = "";
        peopleMapped1.Id = peoplePosted.Id;
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peopleMapped2 = _mapper.Map<PeoplePutViewModel>(peopleFake2);
        peopleMapped2.BirthDate = DateTime.Now.AddYears(-14);
        peopleMapped2.Id = peoplePosted.Id;
        var peopleFake3 = new PeoplePostViewModelFake().Generate();
        var peopleMapped3 = _mapper.Map<PeoplePutViewModel>(peopleFake3);
        peopleMapped3.BirthDate = DateTime.Now.AddYears(-129);
        peopleMapped3.Id = peoplePosted.Id;
        var peopleFake4 = new PeoplePostViewModelFake().Generate();
        var peopleMapped4 = _mapper.Map<PeoplePutViewModel>(peopleFake4);
        peopleMapped4.RG = "123456789";
        peopleMapped4.Id = peoplePosted.Id;
        var peopleFake5 = new PeoplePostViewModelFake().Generate();
        var peopleMapped5 = _mapper.Map<PeoplePutViewModel>(peopleFake5);
        peopleMapped5.CPF = "123456789";
        peopleMapped5.Id = peoplePosted.Id;
        var peopleFake6 = new PeoplePostViewModelFake().Generate();
        var peopleMapped6 = _mapper.Map<PeoplePutViewModel>(peopleFake6);
        peopleMapped6.Phone = "123456789";
        peopleMapped6.Id = peoplePosted.Id;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // People without name
        await Assert.ThrowsAsync<Exception>(() => Put(peopleMapped1));
        // People with less than 14 years old
        await Assert.ThrowsAsync<Exception>(() => Put(peopleMapped2));
        // People with more than 129 years old
        await Assert.ThrowsAsync<Exception>(() => Put(peopleMapped3));
        // People with invalid RG
        await Assert.ThrowsAsync<Exception>(() => Put(peopleMapped4));
        // People with invalid CPF
        await Assert.ThrowsAsync<Exception>(() => Put(peopleMapped5));
        // People with invalid phone
        await Assert.ThrowsAsync<Exception>(() => Put(peopleMapped6));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_PeopleValidAndRoomValid_Ok()
    {
        // Arrange - Room
        var roomFake1 = new RoomPostViewModelFake().Generate();
        if (roomFake1.Occupation == roomFake1.Beds) roomFake1.Occupation--;
        roomFake1.Available = true;
        var roomPosted1 = await _room.Post(roomFake1);

        var roomFake2 = new RoomPostViewModelFake().Generate();
        roomFake2.Occupation = roomFake2.Beds - 1;
        roomFake2.Available = true;
        var roomPosted2 = await _room.Post(roomFake2);

        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post(peopleFake1);

        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post(peopleFake2);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var peoplePuted1 = await Put(peoplePosted1.Id, roomPosted1.Id);
        var roomPuted1 = await _room.Get(roomPosted1.Id);

        var peoplePuted2 = await Put(peoplePosted2.Id, roomPosted2.Id);
        var roomPuted2 = await _room.Get(roomPosted2.Id);

        var quantityAfter = await GetQuantity();

        Assert.Equal(quantityBefore, quantityAfter);
        Assert.Equal(peoplePuted1.RoomId, roomPosted1.Id);
        Assert.Equal(roomPosted1.Occupation + 1, roomPuted1.Occupation);
        Assert.False(roomPuted2.Available);
        Assert.Equal(roomPosted1.Beds, roomPuted1.Beds);
        Assert.Equal(peoplePosted1.Id, peoplePuted1.Id);
        Assert.Equal(roomPosted1.Id, roomPuted1.Id);
    }

    [Fact]
    public async Task AddPeopleToRoom_PeopleInvalid_BadRequest()
    {
        // Arrange - Room
        var roomFake1 = new RoomPostViewModelFake().Generate();
        if (roomFake1.Occupation == roomFake1.Beds) roomFake1.Occupation--;
        roomFake1.Available = true;
        var roomPosted1 = await _room.Post(roomFake1);

        var roomFake2 = new RoomPostViewModelFake().Generate();
        if (roomFake2.Occupation == roomFake2.Beds) roomFake2.Occupation--;
        roomFake2.Available = true;
        var roomPosted2 = await _room.Post(roomFake2);

        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post(peopleFake1);
        var peoplePuted1 = await Put(peoplePosted1.Id, roomPosted1.Id);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post(peopleFake2);
        var peopleFake3 = new PeoplePostViewModelFake().Generate();
        var peoplePosted3 = await Post(peopleFake3);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();
        
        // Act
        // People already has a room
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePuted1.Id, roomPosted1.Id));
        // People with invalid room
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePosted2.Id, -1));
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePosted3.Id, 0));
        // People with invalid id
        await Assert.ThrowsAsync<Exception>(() => Put(-1, roomPosted2.Id));
        await Assert.ThrowsAsync<Exception>(() => Put(0, roomPosted2.Id));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task AddPeopleToRoom_RoomInvalid_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post(peopleFake1);

        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post(peopleFake2);

        // Arrange - Room
        var roomFake1 = new RoomPostViewModelFake().Generate();
        roomFake1.Available = false;
        var roomPosted1 = await _room.Post(roomFake1);

        var roomFake2 = new RoomPostViewModelFake().Generate();
        roomFake2.Occupation = roomFake2.Beds;
        roomFake2.Available = false;
        var roomPosted2 = await _room.Post(roomFake2);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // Room is not available
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePosted1.Id, roomPosted2.Id));
        // Room is full
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePosted1.Id, roomPosted2.Id));
        // Room is invalid
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePosted1.Id, -1));
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePosted1.Id, 0));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_PeopleValid_Ok()
    {
        // Arrange - Room
        var roomFake = new RoomPostViewModelFake().Generate();
        if (roomFake.Occupation == roomFake.Beds) roomFake.Occupation--;
        roomFake.Available = true;
        var roomPosted = await _room.Post(roomFake);

        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post(peopleFake);

        // Arrange - AddPeopleToRoom
        var peoplePuted = await Put(peoplePosted.Id, roomPosted.Id);
        var roomPuted = await _room.Get(roomPosted.Id);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var peopleRemoved = await Put(peoplePosted.Id);
        var roomRemoved = await _room.Get(roomPosted.Id);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
        Assert.Null(peopleRemoved.RoomId);
        Assert.Equal(roomPuted.Occupation - 1, roomRemoved.Occupation);
        Assert.True(roomRemoved.Available);
        Assert.Equal(roomPuted.Beds, roomRemoved.Beds);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_PeopleInvalid_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post(peopleFake1);

        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post(peopleFake2);

        // Arrange - Room
        var roomFake1 = new RoomPostViewModelFake().Generate();
        if (roomFake1.Occupation == roomFake1.Beds) roomFake1.Occupation--;
        roomFake1.Available = true;
        var roomPosted1 = await _room.Post(roomFake1);

        var roomFake2 = new RoomPostViewModelFake().Generate();
        if (roomFake2.Occupation == roomFake2.Beds) roomFake2.Occupation--;
        roomFake2.Available = true;
        var roomPosted2 = await _room.Post(roomFake2);

        // Arrange - AddPeopleToRoom
        var peoplePuted1 = await Put(peoplePosted1.Id, roomPosted1.Id);
        var roomPuted1 = await _room.Get(roomPosted1.Id);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // People is not in a room
        await Assert.ThrowsAsync<Exception>(() => Put(peoplePosted2.Id));
        // People with invalid id
        await Assert.ThrowsAsync<Exception>(() => Put(-1));
        await Assert.ThrowsAsync<Exception>(() => Put(0));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    public async Task<PeopleViewModel> Get(int id)
    {
        var response = await _global._peopleClient.GetAsync($"{id}");
        var content = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<PeopleViewModel>>(content);
        var value = values?.FirstOrDefault();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<int> GetQuantity()
    {
        var response = await _global._peopleClient.GetAsync("quantity");
        var content = await response.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<int>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value;
    }

    public async Task<PeopleViewModel> Post(PeoplePostViewModel people)
    {
        var peopleJson = JsonContent.Create(people);
        var response = await _global._peopleClient.PostAsync("", peopleJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<PeopleViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<PeopleViewModel> Put(PeoplePutViewModel people)
    {
        var peopleJson = JsonContent.Create(people);
        var response = await _global._peopleClient.PutAsync("", peopleJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<PeopleViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<PeopleViewModel> Put(int peopleId, int roomId)
    {
        var ids = new PeopleAddPeopleToRoomViewModel { PeopleId = peopleId, RoomId = roomId };
        var peopleJson = JsonContent.Create(ids);
        var response = await _global._peopleClient.PutAsync("add-people-to-room", peopleJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<PeopleViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<PeopleViewModel> Put(int peopleId)
    {
        var ids = new PeopleRemovePeopleFromRoom { PeopleId = peopleId };
        var peopleJson = JsonContent.Create(ids);
        var response = await _global._peopleClient.PutAsync("remove-people-from-room", peopleJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<PeopleViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }
}
