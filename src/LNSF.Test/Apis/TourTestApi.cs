/*using System.Net;
using System.Net.Http.Json;
using AutoMapper;
using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Newtonsoft.Json;
using Xunit;

namespace LNSF.Test.Apis;

public class TourTestApi
{
    private readonly GlobalClientRequest _global = new();
    private readonly IMapper _mapper;
    private readonly PeopleTestApi _people = new();

    public TourTestApi()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TourViewModel, TourPostViewModel>().ReverseMap();
            cfg.CreateMap<TourViewModel, TourPutViewModel>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Post_ValidTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await _people.Post(peopleFake);

        // Arrange - Tour
        var tourFake = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake.PeopleId = peoplePosted.Id;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        var tourPosted = await Post(tourFake);
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore + 1, quantityAfter);
        Assert.Equivalent(tourFake.PeopleId, tourPosted.PeopleId);
    }

    [Fact]
    public async Task Post_InvalidTour_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await _people.Post(peopleFake1);

        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await _people.Post(peopleFake2);

        // Arrange - Tour
        var tourFake1 = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake1.PeopleId = peoplePosted1.Id;
        var tourPosted = await Post(tourFake1);

        var tourFake2 = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake2.PeopleId = peoplePosted1.Id;

        // Arrange - Tour
        var tourFake3 = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake3.PeopleId = 0;
        var tourFake4 = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake4.PeopleId = -1;

        // Arrange - Quantity
        var quantityBefore = await GetQuantity();

        // Act
        // Not create tour with tour open
        await Assert.ThrowsAsync<Exception>(() => Post(tourFake2));
        // Tour with invalid peopleId
        await Assert.ThrowsAsync<Exception>(() => Post(tourFake3));
        // Tour with invalid peopleId
        await Assert.ThrowsAsync<Exception>(() => Post(tourFake4));
        var quantityAfter = await GetQuantity();

        // Assert
        Assert.Equal(quantityBefore, quantityAfter);
    }

    [Fact]
    public async Task Put_ValidTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await _people.Post(peopleFake);

        // Arrange - Tour
        var tourFake = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake.PeopleId = peoplePosted.Id;
        var tourPosted = await Post(tourFake);

        // Arrange - Tour
        var tourFake2 = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        tourFake2.Id = tourPosted.Id;
        tourFake2.PeopleId = peoplePosted.Id;

        // Act
        var tourPut = await Put(tourFake2);

        // Assert
        Assert.Equal(tourFake2.Id, tourPut.Id);
        Assert.Equal(tourFake2.PeopleId, tourPut.PeopleId);
    }

    [Fact]
    public async Task Put_InvalidTour_BadRequest()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await _people.Post(peopleFake1);

        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await _people.Post(peopleFake2);

        // Arrange - Tour
        var tourFake1 = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake1.PeopleId = peoplePosted1.Id;
        var tourPosted = await Post(tourFake1);

        var tourFake2 = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        tourFake2.Id = tourPosted.Id;
        tourFake2.PeopleId = peoplePosted1.Id;

        var tourFake3 = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        tourFake3.Id = tourPosted.Id;
        tourFake3.PeopleId = peoplePosted2.Id;

        var tourFake4 = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        tourFake4.Id = tourPosted.Id;
        tourFake4.PeopleId = 0;
        var tourFake5 = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        tourFake5.Id = tourPosted.Id;
        tourFake5.PeopleId = -1;

        // Act
        // Tour with invalid peopleId
        await Assert.ThrowsAsync<Exception>(() => Put(tourFake3));
        // Tour with invalid peopleId
        await Assert.ThrowsAsync<Exception>(() => Put(tourFake4));
        // Tour with invalid peopleId
        await Assert.ThrowsAsync<Exception>(() => Put(tourFake5));
    }

    [Fact]
    public async Task PutAll_ValidTour_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await _people.Post(peopleFake);

        // Arrange - Tour
        var tourFake = _mapper.Map<TourPostViewModel>(new TourViewModelFake().Generate());
        tourFake.PeopleId = peoplePosted.Id;
        var tourPosted = await Post(tourFake);
        var tourFake2 = _mapper.Map<TourPutViewModel>(new TourViewModelFake().Generate());
        tourFake2.Id = tourPosted.Id;
        tourFake2.PeopleId = peoplePosted.Id;
        var tourPuted = await Put(tourFake2);

        // Arrange - Tour
        var tourFake3 = _mapper.Map<TourViewModel>(new TourViewModelFake().Generate());
        tourFake3.Id = tourPuted.Id;
        tourFake3.PeopleId = peoplePosted.Id;

        // Act
        var tourPuted2 = await PutAll(tourFake3);

        // Assert
        Assert.Equal(tourFake3.Id, tourPuted2.Id);
        Assert.Equal(tourFake3.PeopleId, tourPuted2.PeopleId);
    }

    public async Task<TourViewModel> Get(int id)
    {
        var response = await _global._tourClient.GetAsync($"{id}");
        var content = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<TourViewModel>>(content);
        var value = values?.FirstOrDefault();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<int> GetQuantity()
    {
        var response = await _global._tourClient.GetAsync("quantity");
        var content = await response.Content.ReadAsStringAsync();
        var value = JsonConvert.DeserializeObject<int>(content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        return value;
    }

    public async Task<TourViewModel> Post(TourPostViewModel tour)
    {
        var tourJson = JsonContent.Create(tour);
        var response = await _global._tourClient.PostAsync("", tourJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<TourViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<TourViewModel> Put(TourPutViewModel tour)
    {
        var tourJson = JsonContent.Create(tour);
        var response = await _global._tourClient.PutAsync("", tourJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<TourViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }

    public async Task<TourViewModel> PutAll(TourViewModel tour)
    {
        var tourJson = JsonContent.Create(tour);
        var response = await _global._tourClient.PutAsync("put-all", tourJson);
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.BadRequest)
            throw new Exception(content);

        var value = JsonConvert.DeserializeObject<TourViewModel>(content);

        return value ?? throw new Exception("Value is null");
    }
}
*/