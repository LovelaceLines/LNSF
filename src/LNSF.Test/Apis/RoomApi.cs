using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LNSF.Domain.Entities;
using LNSF.Test.Fakers;
using Newtonsoft.Json;
using Xunit;

namespace LNSF.Test.Apis;

public class RoomApi
{
    private static readonly Uri uri = new("http://localhost:5206/api/Room/");
    private readonly HttpClient _client = new() { BaseAddress = uri };

    [Fact]
    public async Task Get_UsingFilters_ReturnOk()
    {
        var rooms = new RoomPostViewModelFake().Generate(200);
        var postTasks = rooms.Select(room => 
            _client.PostAsync("", JsonContent.Create(room))
        ).ToList();
        await Task.WhenAll(postTasks);

        var filters = new RoomFiltersFake().Generate();
        var response = await _client.GetAsync($"?{ConvertObjectToQueryString(filters)}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetQuantity_Empty_IsZero()
    {
        var quantity = await GetQuantity();
        if (quantity != 0) return;

        quantity.Should().Be(0);
    }

    [Fact]
    public async Task GetQuantity_PostRoom_ReturnQuantityPlusOne()
    {
        var quantity = await GetQuantity();
        
        await _client.PostAsync("", JsonContent.Create(new RoomPostViewModelFake().Generate()));
        
        var response = await GetQuantity();
        response.Should().Be(quantity + 1);
    }

    [Fact]
    public async Task Post_RoomValid_ReturnOk()
    {
        var quantity = await GetQuantity();
        var room = new RoomPostViewModelFake().Generate();
        var response = await _client.PostAsync("", JsonContent.Create(room));
        var newQuantity = await GetQuantity();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        newQuantity.Should().Be(quantity + 1);
        var newRoom = await response.Content.ReadFromJsonAsync<Room>();
        newRoom.Id.Should().NotBe(0);
        room.Should().BeEquivalentTo(newRoom, o => o.Excluding(r => r.Id));
    }

    [Fact]
    public async Task Post_RoomInvalid_ReturnBadRequest()
    {
        var quantity = await GetQuantity();
        var room = new RoomPostViewModelFake().Generate();
        room.Number = "";
        var response = await _client.PostAsync("", JsonContent.Create(room));
        var newQuantity = await GetQuantity();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        newQuantity.Should().Be(quantity);
    }

    public async Task<int> GetQuantity()
    {
        var response = await _client.GetAsync("quantity");
        var quantity = int.Parse(await response.Content.ReadAsStringAsync());

        return quantity;
    }

    public string ConvertObjectToQueryString(object obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var encodedJson = Uri.EscapeDataString(json);

        return encodedJson;
    }
}
