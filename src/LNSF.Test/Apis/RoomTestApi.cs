using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Test.Fakers;
using Newtonsoft.Json;
using Xunit;

namespace LNSF.Test.Apis;

public class RoomTestApi
{
    private static readonly Uri uri = new("http://localhost:5206/api/Room/");
    private readonly HttpClient _client = new() { BaseAddress = uri };
    private readonly Util _util = new();

    [Fact]
    public async Task Get_UsingFilters_ReturnOk()
    {
        var rooms = new RoomPostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 9999));
        var postTasks = rooms.Select(room => 
            _client.PostAsync("", JsonContent.Create(room))
        ).ToList();
        await Task.WhenAll(postTasks);

        var filters = new RoomFiltersFake().Generate();
        var response = await _client.GetAsync(_util.ConvertObjectToQueryString(filters));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Get_UsingFiltersEmpty_ReturnAll()
    {
        var rooms = new RoomPostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 9999));
        var postTasks = rooms.Select(room => 
            _client.PostAsync("", JsonContent.Create(room))
        ).ToList();
        await Task.WhenAll(postTasks);

        var filters = new RoomFilters(); // Empty
        var response = await _client.GetAsync(_util.ConvertObjectToQueryString(filters));
        var roomsResponse = await response.Content.ReadFromJsonAsync<List<Room>>();
        var quantity = await _util.GetQuantity(_client);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(quantity, roomsResponse.Count);
    }

    [Fact]
    public async Task GetQuantity__NotNull()
    {
        var reponse = await _client.GetAsync("quantity");
        var quantity = await reponse.Content.ReadFromJsonAsync<int>();

        Assert.Equal(HttpStatusCode.OK, reponse.StatusCode);
        Assert.True(quantity >= 0);
    }

    [Fact]
    public async Task GetQuantity_PostRoom_ReturnQuantityPlusOne()
    {
        var quantity = await _util.GetQuantity(_client);
        
        await _client.PostAsync("", JsonContent.Create(new RoomPostViewModelFake().Generate()));
        var newQuantity = await _util.GetQuantity(_client);

        Assert.Equal(quantity + 1, newQuantity);
    }

    [Fact]
    public async Task Post_RoomValid_ReturnOk()
    {
        var room = new RoomPostViewModelFake().Generate();
        var quantity = await _util.GetQuantity(_client);

        var response = await _client.PostAsync("", JsonContent.Create(room));
        var roomResponse = await response.Content.ReadFromJsonAsync<Room>();
        var newQuantity = await _util.GetQuantity(_client);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(quantity + 1, newQuantity);
        Assert.NotEqual(0, roomResponse.Id);
        Assert.Equal(room.Number, roomResponse.Number);
    }

    [Fact]
    public async Task Post_RoomInvalid_ReturnBadRequest()
    {
        var room = new RoomPostViewModelFake().Generate();
        room.Number = "";
        var quantity = await _util.GetQuantity(_client);

        var response = await _client.PostAsync("", JsonContent.Create(room));
        var newQuantity = await _util.GetQuantity(_client);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal(quantity, newQuantity);
    }

    [Fact]
    public async Task Put_RoomValid_ReturnOk()
    {
        var room = new RoomPostViewModelFake().Generate();
        var postResponse = await _client.PostAsync("", JsonContent.Create(room));
        var newRoom = await postResponse.Content.ReadFromJsonAsync<Room>();
        var numberRoom = RandomNumberGenerator.GetInt32(999999).ToString();
        newRoom.Number = numberRoom;

        var response = await _client.PutAsync("", JsonContent.Create(newRoom));
        var updatedRoom = await response.Content.ReadFromJsonAsync<Room>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(newRoom.Id, updatedRoom.Id);
    }
}
