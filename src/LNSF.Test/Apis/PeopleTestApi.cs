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
            cfg.CreateMap<PeopleReturnViewModel, PeoplePostViewModel>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Get_UsingFilters_ReturnOk()
    {
        var fakePeoples = new PeoplePostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 10));
        await Post(fakePeoples);
        var quantity = await GetQuantity();

        var filters = new PeopleFiltersFake().Generate();
        var getResponse = await _global._peopleClient.GetAsync(_global.ConvertObjectToQueryString(filters));
        var getPeoples = await getResponse.Content.ReadFromJsonAsync<List<People>>();
        var newQuantity = await GetQuantity();

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(quantity, newQuantity);
        Assert.NotNull(getPeoples);
        Assert.True(getPeoples?.Count >= 0);
        Assert.True(getPeoples?.Count <= filters.Page.PageSize);
        Assert.True(getPeoples?.Count <= quantity);
    }

    [Fact]
    public async Task Get_UsingFiltersEmpty_ReturnAll() // Filters empty equals to return all
    {
        int randomCount = RandomNumberGenerator.GetInt32(1, 50);
        var quantity = await GetQuantity();
        var fakePeoples = new PeoplePostViewModelFake().Generate(randomCount);
        await Post(fakePeoples);

        var filters = new PeopleFilters(); // Empty
        var getResponse = await _global._peopleClient.GetAsync(_global.ConvertObjectToQueryString(filters));
        var getPeoples = await getResponse.Content.ReadFromJsonAsync<List<People>>();
        var newQuantity = await GetQuantity();

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(quantity + randomCount, newQuantity);
        Assert.Equal(newQuantity, getPeoples?.Count);
        Assert.True(getPeoples?.Count >= 0);
    }

    [Fact]
    public async Task GetQuantity__ReturnOk()
    {
        var fakePeoples = new PeoplePostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 50));
        await Post(fakePeoples);

        var getResponse = await _global._peopleClient.GetAsync("quantity");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetQuantity_PostPeople_ReturnQuantityPlusOne()
    {
        var quantity = await GetQuantity();

        var people = new PeoplePostViewModelFake().Generate();
        var postResponse = await _global._peopleClient.PostAsync("", JsonContent.Create(people));
        var newQuantity = await GetQuantity();

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        Assert.Equal(quantity + 1, newQuantity);
    }

    [Fact]
    public async Task Post_PeopleValid_ReturnOk()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var quantity = await GetQuantity();

        var postResponse = await _global._peopleClient.PostAsync("", JsonContent.Create(fakePeople));
        var postPeople = await postResponse.Content.ReadFromJsonAsync<PeopleReturnViewModel>();
        var newQuantity = await GetQuantity();

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        Assert.Equal(quantity + 1, newQuantity);
        Assert.Equivalent(fakePeople, postPeople);
        Assert.True(postPeople?.Id != null);
        Assert.True(postPeople?.RoomId == null);
    }

    [Fact]
    public async Task Post_PeopleInvalid_ReturnBadRequest()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        fakePeople.Name = "";

        var postResponse = await _global._peopleClient.PostAsync("", JsonContent.Create(fakePeople));

        Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
    }

    [Fact]
    public async Task Put_PeopleValid_ReturnOk()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var postPeople = await Post(fakePeople);
        var quantity = await _global.GetQuantity(_global._peopleClient);

        fakePeople = new PeoplePostViewModelFake().Generate();
        var putPeople = _mapper.Map<PeopleReturnViewModel>(fakePeople);
        putPeople.Id = postPeople.Id;
        var putResponse = await _global._peopleClient.PutAsync("", JsonContent.Create(putPeople));
        var getPutPeople = await putResponse.Content.ReadFromJsonAsync<PeopleReturnViewModel>();
        var newQuantity = await GetQuantity();

        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
        Assert.Equal(quantity, newQuantity);
        Assert.Equivalent(fakePeople, getPutPeople);
        Assert.Equal(postPeople.Id, putPeople.Id);
        Assert.Equal(putPeople.Id, getPutPeople?.Id);
        Assert.Equal(postPeople.RoomId, putPeople.RoomId);
        Assert.Equal(putPeople.RoomId, getPutPeople?.RoomId);
    }

    [Fact]
    public async Task AddPeopleToRoom_PeopleValidAndRoomValid_ReturnOk()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var postPeople = await Post(fakePeople);
        var fakeRoom = new RoomPostViewModelFake().Generate();
        if (fakeRoom.Occupation == fakeRoom.Beds) fakeRoom.Occupation--;
        fakeRoom.Available = true;
        var postRoom = await _room.Post(fakeRoom);

        var ids = new PeopleAddPeopleToRoomViewModel
        {
            PeopleId = postPeople?.Id ?? 0,
            RoomId = postRoom?.Id ?? 0
        };
        var peoplePutResponse = await _global._peopleClient.PutAsync("add-people-to-room", JsonContent.Create(ids));
        var putPeople = await peoplePutResponse.Content.ReadFromJsonAsync<PeopleReturnViewModel>();
        var filters = new RoomFilters(){ Id = postRoom?.Id };
        var roomGetResponse = await _global._roomClient.GetAsync(_global.ConvertObjectToQueryString(filters));
        var putRooms = await roomGetResponse.Content.ReadFromJsonAsync<List<Room>>();
        var putRoom = putRooms?.FirstOrDefault();

        Assert.Equal(HttpStatusCode.OK, peoplePutResponse.StatusCode);
        Assert.Equivalent(fakePeople, putPeople);
        Assert.Equal(ids.RoomId, putRoom?.Id);
        Assert.Equal(fakeRoom?.Occupation + 1, putRoom?.Occupation ?? 0);
    }

    [Fact]
    public async Task AddPeopleToRoom_PeopleInvalid_ReturnBadRequest()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var postPeople = await Post(fakePeople);
        var fakeRoom = new RoomPostViewModelFake().Generate();
        if (fakeRoom.Occupation == fakeRoom.Beds) fakeRoom.Occupation--;
        fakeRoom.Available = true;
        var postRoom = await _room.Post(fakeRoom);
        var ids = new PeopleAddPeopleToRoomViewModel
        {
            PeopleId = postPeople?.Id ?? 0,
            RoomId = postRoom?.Id ?? 0
        };
        var peoplePutResponse = await _global._peopleClient.PutAsync("add-people-to-room", JsonContent.Create(ids));
        
        var fakeRoom2 = new RoomPostViewModelFake().Generate();
        if (fakeRoom2.Occupation == fakeRoom2.Beds) fakeRoom2.Occupation--;
        fakeRoom2.Available = true;
        var postRoom2 = await _room.Post(fakeRoom2);
        var ids2 = new PeopleAddPeopleToRoomViewModel
        {
            PeopleId = postPeople?.Id ?? 0,
            RoomId = postRoom2?.Id ?? 0
        };

        var peoplePutResponse2 = await _global._peopleClient.PutAsync("add-people-to-room", JsonContent.Create(ids2));
        var getRoom2 = await _room.Get(postRoom2?.Id ?? 0);

        Assert.Equal(HttpStatusCode.OK, peoplePutResponse.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, peoplePutResponse2.StatusCode);
        Assert.Equivalent(fakeRoom2, getRoom2);
        Assert.Equivalent(postRoom2, getRoom2);
    }

    [Fact]
    public async Task AddPeopleToRoom_RoomInvalid_ReturnBadRequest()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var postPeople = await Post(fakePeople);
        var fakeRoom = new RoomPostViewModelFake().Generate();
        if (fakeRoom.Occupation == fakeRoom.Beds) fakeRoom.Occupation = fakeRoom.Beds - 1;
        fakeRoom.Available = true;
        var postRoom = await _room.Post(fakeRoom);
        var ids = new PeopleAddPeopleToRoomViewModel
        {
            PeopleId = postPeople?.Id ?? 0,
            RoomId = postRoom?.Id ?? 0
        };
        var peoplePutResponse = await _global._peopleClient.PutAsync("add-people-to-room", JsonContent.Create(ids));
        var getPostRoom = await _room.Get(postRoom?.Id ?? 0);

        var fakePeople2 = new PeoplePostViewModelFake().Generate();
        var postPeople2 = await Post(fakePeople2);
        var ids2 = new PeopleAddPeopleToRoomViewModel
        {
            PeopleId = postPeople2?.Id ?? 0,
            RoomId = postRoom?.Id ?? 0
        };

        var peoplePutResponse2 = await _global._peopleClient.PutAsync("add-people-to-room", JsonContent.Create(ids2));
        var getRoom2 = await _room.Get(postRoom?.Id ?? 0);

        Assert.Equal(HttpStatusCode.OK, peoplePutResponse.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, peoplePutResponse2.StatusCode);
        Assert.Equivalent(getPostRoom, getRoom2);
        Assert.NotEqual(postRoom, getRoom2);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_PeopleValid_ReturnOk()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var postPeople = await Post(fakePeople);
        var fakeRoom = new RoomPostViewModelFake().Generate();
        if (fakeRoom.Occupation == fakeRoom.Beds) fakeRoom.Occupation = fakeRoom.Beds - 1;
        fakeRoom.Available = true;
        var postRoom = await _room.Post(fakeRoom);
        var ids = new PeopleAddPeopleToRoomViewModel
        {
            PeopleId = postPeople?.Id ?? 0,
            RoomId = postRoom?.Id ?? 0
        };
        var peoplePutResponse = await _global._peopleClient.PutAsync("add-people-to-room", JsonContent.Create(ids));
        var getPostRoom = await _room.Get(postRoom?.Id ?? 0);
        var getPutPeople = await Get(postPeople?.Id ?? 0);

        var peoplePutResponse2 = await _global._peopleClient.PutAsync("remove-people-from-room", JsonContent.Create(new PeopleRemovePeopleFromRoom(){ PeopleId = postPeople?.Id ?? 0 }));
        var message = await peoplePutResponse2.Content.ReadAsStringAsync();
        var getPeople = await Get(postPeople?.Id ?? 0); 
        var getRoom = await _room.Get(postRoom?.Id ?? 0);

        Assert.Equal(HttpStatusCode.OK, peoplePutResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, peoplePutResponse2.StatusCode);
        Assert.Equal(getPostRoom.Occupation, getRoom.Occupation + 1);
        Assert.NotEqual(getPutPeople.RoomId, getPeople.RoomId);
    }

    [Fact]
    public async Task RemovePeopleFromRoom_PeopleInvalid_ReturnBadRequest()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var peoplePostResponse = await _global._peopleClient.PostAsync("", JsonContent.Create(fakePeople));
        var postPeople = await peoplePostResponse.Content.ReadFromJsonAsync<PeopleReturnViewModel>();
        
        var peoplePutResponse = await _global._peopleClient.PutAsync("remove-people-from-room", JsonContent.Create(new PeopleRemovePeopleFromRoom(){ PeopleId = postPeople?.Id ?? 0 }));
        var getPeople = await Get(postPeople?.Id ?? 0);
        
        Assert.Equal(HttpStatusCode.BadRequest, peoplePutResponse.StatusCode);
        Assert.Equivalent(postPeople, getPeople);
    }

    public async Task<PeopleReturnViewModel> Get(int id)
    {
        var getResponse = await _global._peopleClient.GetAsync($"?id={id}");
        var getPeople = await getResponse.Content.ReadFromJsonAsync<List<PeopleReturnViewModel>>();

        return getPeople?.First() ?? throw new Exception("Value is null");
    }

    public async Task<int> GetQuantity()
    {
        var response = await _global._peopleClient.GetAsync("quantity");
        var quantity = int.Parse(await response.Content.ReadAsStringAsync());

        return quantity;
    }

    public async Task Post(List<PeoplePostViewModel> peoples)
    {
        var postTasks = peoples.Select(people => _global._peopleClient.PostAsync("", JsonContent.Create(people))).ToList();
        await Task.WhenAll(postTasks);
    }

    public async Task<PeopleReturnViewModel> Post(PeoplePostViewModel people)
    {
        var postResponse = await _global._peopleClient.PostAsync("", JsonContent.Create(people));
        var content = await postResponse.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<PeopleReturnViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<PeopleReturnViewModel> Put(PeoplePutViewModel people)
    {
        var putResponse = await _global._peopleClient.PutAsync("", JsonContent.Create(people));
        var content = await putResponse.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<PeopleReturnViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }
}
