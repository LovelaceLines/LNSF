using System.Net;
using System.Net.Http.Json;
using AutoMapper;
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
            cfg.CreateMap<RoomPostViewModel, RoomViewModel>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Post_RoomValid_Ok()
    {
        // Arrange
        var fakeRoom = new RoomPostViewModelFake().Generate();
        var quantityBefore = await GetQuantity();

        // Act
        var roomPosted = await Post(fakeRoom);
        var quantityAfter = await GetQuantity();

        Assert.Equal(quantityBefore + 1, quantityAfter);
        Assert.Equivalent(fakeRoom, roomPosted);
    }

    [Fact]
    public async Task Post_RoomInvalid_BadRequest()
    {
        // Arrange
        var fakeRoom1 = new RoomPostViewModelFake().Generate();
        fakeRoom1.Number = "";
        var fakeRoom2 = new RoomPostViewModelFake().Generate();
        fakeRoom2.Beds = 0;
        var fakeRoom3 = new RoomPostViewModelFake().Generate();
        fakeRoom3.Storey = -1;
        var fakeRoom4 = new RoomPostViewModelFake().Generate();
        fakeRoom4.Occupation = -1;
        var fakeRoom5 = new RoomPostViewModelFake().Generate();
        fakeRoom5.Occupation = fakeRoom5.Beds + 1;
        var fakeRoom6 = new RoomPostViewModelFake().Generate();
        fakeRoom6.Available = true;
        fakeRoom6.Occupation = fakeRoom6.Beds;
        var fakeRoom7 = new RoomPostViewModelFake().Generate();
        fakeRoom7.Available = true;
        fakeRoom7.Occupation = fakeRoom7.Beds + 1;

        var quantityBefore = await GetQuantity();

        // Act
        // Room withou number
        await Assert.ThrowsAsync<Exception>(() => Post(fakeRoom1));
        // Room withou beds
        await Assert.ThrowsAsync<Exception>(() => Post(fakeRoom2));
        // Room with invalid storey
        await Assert.ThrowsAsync<Exception>(() => Post(fakeRoom3));
        // Room with invalid occupation
        await Assert.ThrowsAsync<Exception>(() => Post(fakeRoom4));
        // Room with more occupants than beds
        await Assert.ThrowsAsync<Exception>(() => Post(fakeRoom5));
        // Room with available but no vacant beds
        await Assert.ThrowsAsync<Exception>(() => Post(fakeRoom6));
        // Room with available but more occupants than beds
        await Assert.ThrowsAsync<Exception>(() => Post(fakeRoom7));

        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomValid_Ok()
    {
        // Arrange - Room
        var fakeRoom1 = new RoomPostViewModelFake().Generate();
        var roomPosted1 = await Post(fakeRoom1);

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var fakeRoom2 = new RoomPostViewModelFake().Generate();
        var roomMapped = _mapper.Map<RoomViewModel>(fakeRoom2);
        roomMapped.Id = roomPosted1.Id;
        var roomPuted = await Put(roomMapped);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equivalent(fakeRoom2, roomPuted);
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_RoomInvalid_BadRequest()
    {
        // Arrange - Room
        var roomFake = new RoomPostViewModelFake().Generate();
        var roomPosted = await Post(roomFake);

        var roomFake1 = new RoomPostViewModelFake().Generate();
        var roomMapped1 = _mapper.Map<RoomViewModel>(roomFake1);
        roomMapped1.Number = "";
        var roomFake2 = new RoomPostViewModelFake().Generate();
        var roomMapped2 = _mapper.Map<RoomViewModel>(roomFake2);
        roomMapped2.Beds = 0;
        var roomFake3 = new RoomPostViewModelFake().Generate();
        var roomMapped3 = _mapper.Map<RoomViewModel>(roomFake3);
        roomMapped3.Storey = -1;
        var roomFake4 = new RoomPostViewModelFake().Generate();
        var roomMapped4 = _mapper.Map<RoomViewModel>(roomFake4); 
        roomMapped4.Occupation = -1;
        var roomFake5 = new RoomPostViewModelFake().Generate();
        var roomMapped5 = _mapper.Map<RoomViewModel>(roomFake5);
        roomMapped5.Occupation = roomMapped5.Beds + 1;
        var roomFake6 = new RoomPostViewModelFake().Generate();
        var roomMapped6 = _mapper.Map<RoomViewModel>(roomFake6); 
        roomMapped6.Available = true;
        roomMapped6.Occupation = roomMapped6.Beds;
        var roomFake7 = new RoomPostViewModelFake().Generate();
        var roomMapped7 = _mapper.Map<RoomViewModel>(roomFake7); 
        roomMapped7.Available = true;
        roomMapped7.Occupation = roomMapped7.Beds + 1;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // Room withou number
        await Assert.ThrowsAsync<Exception>(() => Put(roomMapped1));
        // Room withou beds
        await Assert.ThrowsAsync<Exception>(() => Put(roomMapped2));
        // Room with invalid storey
        await Assert.ThrowsAsync<Exception>(() => Put(roomMapped3));
        // Room with invalid occupation
        await Assert.ThrowsAsync<Exception>(() => Put(roomMapped4));
        // Room with more occupants than beds
        await Assert.ThrowsAsync<Exception>(() => Put(roomMapped5));
        // Room with available but no vacant beds
        await Assert.ThrowsAsync<Exception>(() => Put(roomMapped6));
        // Room with available but more occupants than beds
        await Assert.ThrowsAsync<Exception>(() => Put(roomMapped7));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    public async Task<RoomViewModel> Get(int id)
    {
        var response = await _global._roomClient.GetAsync($"?Id={id}");
        var content = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<RoomViewModel>>(content);
        var value = values?.FirstOrDefault();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<int> GetQuantity()
    {
        var response = await _global._roomClient.GetAsync("quantity");
        var content = await response.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<int>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value;
    }

    public async Task<RoomViewModel> Post(RoomPostViewModel room)
    {
        var roomJson = JsonContent.Create(room);
        var response = await _global._roomClient.PostAsync("", roomJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<RoomViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<RoomViewModel> Put(RoomViewModel room)
    {
        var roomJson = JsonContent.Create(room);
        var response = await _global._roomClient.PutAsync("", roomJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<RoomViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }
}
