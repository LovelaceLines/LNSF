using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using AutoMapper;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Test.Fakers;
using LNSF.UI.ViewModels;
using Xunit;

namespace LNSF.Test.Apis;

public class PeopleTestApi
{
    private static readonly Uri uri = new($"{Util.BaseUrl}People/");
    private readonly HttpClient _client = new() { BaseAddress = uri };
    private readonly Util _util = new();
    private readonly IMapper _mapper;

    public PeopleTestApi()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PeoplePutViewModel, PeoplePostViewModel>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task Get_UsingFilters_ReturnOk()
    {
        var fakePeoples = new PeoplePostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 50));
        await _util.Post(_client, fakePeoples);

        var filters = new PeopleFiltersFake().Generate();
        var getResponse = await _client.GetAsync(_util.ConvertObjectToQueryString(filters));

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task Get_UsingFiltersEmpty_ReturnAll()
    {
        var fakePeoples = new PeoplePostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 50));
        await _util.Post(_client, fakePeoples);

        var filters = new PeopleFilters(); // Empty
        var getResponse = await _client.GetAsync(_util.ConvertObjectToQueryString(filters));
        var getPeoples = await getResponse.Content.ReadFromJsonAsync<List<People>>();
        var quantity = await _util.GetQuantity(_client);

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.Equal(quantity, getPeoples?.Count);
    }

    [Fact]
    public async Task GetQuantity_ReturnOk()
    {
        var fakePeoples = new PeoplePostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 50));
        await _util.Post(_client, fakePeoples);

        var getResponse = await _client.GetAsync("quantity");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task GetQuantity_PostPeople_ReturnQuantityPlusOne()
    {
        var fakePeoples = new PeoplePostViewModelFake().Generate(RandomNumberGenerator.GetInt32(1, 50));
        await _util.Post(_client, fakePeoples);
        var quantityBefore = await _util.GetQuantity(_client);

        var people = new PeoplePostViewModelFake().Generate();
        var postResponse = await _client.PostAsync("", JsonContent.Create(people));
        var quantityAfter = await _util.GetQuantity(_client);

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        Assert.Equal(quantityBefore + 1, quantityAfter);
    }

    [Fact]
    public async Task Post_PeopleValid_ReturnOk()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var quantity = await _util.GetQuantity(_client);

        var postResponse = await _client.PostAsync("", JsonContent.Create(fakePeople));
        var postPeople = await postResponse.Content.ReadFromJsonAsync<PeopleReturnViewModel>();

        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        Assert.Equal(quantity + 1, postPeople?.Id);
        Assert.True(postPeople?.RoomId == null);
    }

    [Fact]
    public async Task Post_PeopleInvalid_ReturnBadRequest()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        fakePeople.Name = "";

        var postResponse = await _client.PostAsync("", JsonContent.Create(fakePeople));

        Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
    }

    [Fact]
    public async Task Put_PeopleValid_ReturnOk()
    {
        var fakePeople = new PeoplePostViewModelFake().Generate();
        var postResponse = await _client.PostAsync("", JsonContent.Create(fakePeople));
        var postPeople = await postResponse.Content.ReadFromJsonAsync<PeopleReturnViewModel>();

        var putPeople = _mapper.Map<PeoplePutViewModel>(new PeoplePostViewModelFake().Generate());
        putPeople.Id = postPeople?.Id ?? 0;
        var putResponse = await _client.PutAsync("", JsonContent.Create(putPeople));
        var message = await putResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
    }
}
