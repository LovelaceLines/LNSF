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

public class RoomTestApi
{
    private readonly Global _global = new();
    private readonly IMapper _mapper;

    public RoomTestApi()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<RoomPostViewModel, Room>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Get_UsingFilters_ReturnOk()
    {
        var fakeRooms = new RoomPostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 10));
        await _global.Post(_global._roomClient, fakeRooms);

        var filters = new RoomFiltersFake().Generate();
        var getResponse = await _global._roomClient.GetAsync(_global.ConvertObjectToQueryString(filters));

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task Get_UsingFiltersEmpty_ReturnAll()
    {
        var fakeRooms = new RoomPostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 10));
        await _global.Post(_global._roomClient, fakeRooms);

        var filters = new RoomFilters(); // Empty
        var getResponse = await _global._roomClient.GetAsync(_global.ConvertObjectToQueryString(filters));
        var getRooms = await getResponse.Content.ReadFromJsonAsync<List<Room>>();
        var quantity = await _global.GetQuantity(_global._roomClient);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(quantity, getRooms?.Count);
    }

    [Fact]
    public async Task Get_UsingFiltersId_ReturnOne()
    {
        var fakeRooms = new RoomPostViewModelFake().Generate();
        var postResponse = await _global._roomClient.PostAsync("", JsonContent.Create(fakeRooms));
        var postRoom = await postResponse.Content.ReadFromJsonAsync<Room>();

        var filters = new RoomFilters() { Id = postRoom?.Id ?? 0 };
        var getResponse = await _global._roomClient.GetAsync(_global.ConvertObjectToQueryString(filters));
        var getRooms = await _global.Get<List<Room>>(_global._roomClient, _global.ConvertObjectToQueryString(filters));

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Single(getRooms);
    }

    [Fact]
    public async Task GetQuantity__NotNull()
    {
        var getReponse = await _global._roomClient.GetAsync("quantity");
        var quantity = await getReponse.Content.ReadFromJsonAsync<int>();

        Assert.Equal(HttpStatusCode.OK, getReponse.StatusCode);
        Assert.True(quantity >= 0);
    }

    [Fact]
    public async Task GetQuantity_PostRoom_ReturnQuantityPlusOne()
    {
        var quantity = await _global.GetQuantity(_global._roomClient);
        
        await _global._roomClient.PostAsync("", JsonContent.Create(new RoomPostViewModelFake().Generate()));
        var newQuantity = await _global.GetQuantity(_global._roomClient);

        Assert.Equal(quantity + 1, newQuantity);
    }

    [Fact]
    public async Task Post_RoomValid_ReturnOk()
    {
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var quantity = await _global.GetQuantity(_global._roomClient);

        var postResponse = await _global._roomClient.PostAsync("", JsonContent.Create(fakeRoom));
        var postRoom = await postResponse.Content.ReadFromJsonAsync<Room>();
        var newQuantity = await _global.GetQuantity(_global._roomClient);

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        Assert.Equal(quantity + 1, newQuantity);
        Assert.NotEqual(0, postRoom?.Id ?? 0);
        Assert.Equivalent(fakeRoom, postRoom);
    }

    [Fact]
    public async Task Post_RoomInvalid_ReturnBadRequest()
    {
        var fakeRoom = new RoomPostViewModelFake().Generate();
        fakeRoom.Number = "";
        var quantity = await _global.GetQuantity(_global._roomClient);

        var postResponse = await _global._roomClient.PostAsync("", JsonContent.Create(fakeRoom));
        var newQuantity = await _global.GetQuantity(_global._roomClient);

        Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        Assert.Equal(quantity, newQuantity);
    }

    [Fact]
    public async Task Put_RoomValid_ReturnOk()
    {
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var postResponse = await _global._roomClient.PostAsync("", JsonContent.Create(fakeRoom));
        var postRoom = await postResponse.Content.ReadFromJsonAsync<Room>();
        var postRoomId = postRoom?.Id ?? 0;
        postRoom = _mapper.Map<Room>(new RoomPostViewModelFake().Generate());
        postRoom.Id = postRoomId;

        var putResponse = await _global._roomClient.PutAsync("", JsonContent.Create(postRoom));
        var putRoom = await putResponse.Content.ReadFromJsonAsync<Room>();

        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
        Assert.Equal(postRoomId, putRoom?.Id);
    }

    public async Task<Room> Get(int id)
    {
        var filters = new RoomFilters() { Id = id };
        var getResponse = await _global._roomClient.GetAsync(_global.ConvertObjectToQueryString(filters));
        var getRooms = await _global.Get<List<Room>>(_global._roomClient, _global.ConvertObjectToQueryString(filters));

        return getRooms?.FirstOrDefault();
    }

    public async Task<Room> Post(RoomPostViewModel room)
    {
        var postResponse = await _global._roomClient.PostAsync("", JsonContent.Create(room));
        var content = await postResponse.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<Room>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task Post(List<RoomPostViewModel> rooms)
    {
        var postTasks = rooms.Select(room => _global._roomClient.PostAsync("", JsonContent.Create(room))).ToList();
        await Task.WhenAll(postTasks);
    }
}
