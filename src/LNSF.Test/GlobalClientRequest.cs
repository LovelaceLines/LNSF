using System.Net;
using System.Net.Http.Json;
using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Domain.Entities;
using Newtonsoft.Json;
using Xunit;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace LNSF.Test;

public class GlobalClientRequest
{
    public const string BaseUrl = "http://localhost:5206/api/";
    public readonly HttpClient _authClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/") };
    public readonly HttpClient _accountClient = new() { BaseAddress = new Uri($"{BaseUrl}Account/") };
    public readonly HttpClient _peopleClient = new() { BaseAddress = new Uri($"{BaseUrl}People/") };
    public readonly HttpClient _roomClient = new() { BaseAddress = new Uri($"{BaseUrl}Room/") };
    public readonly HttpClient _emergencyContactClient = new() { BaseAddress = new Uri($"{BaseUrl}EmergencyContact/") };
    public readonly HttpClient _tourClient = new() { BaseAddress = new Uri($"{BaseUrl}Tour/") };
    public readonly HttpClient _escortClient = new() { BaseAddress = new Uri($"{BaseUrl}Escort/") };
    public readonly HttpClient _hospitalClient = new() { BaseAddress = new Uri($"{BaseUrl}Hospital/") };
    public readonly IMapper _mapper;

    public GlobalClientRequest()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AccountPostViewModel, AccountLoginViewModel>().ReverseMap();
            cfg.CreateMap<AccountPostViewModel, AccountPutViewModel>().ReverseMap();
            cfg.CreateMap<AuthenticationTokenViewModel, AuthenticationToken>().ReverseMap();
            
            cfg.CreateMap<RoomViewModel, RoomPostViewModel>().ReverseMap();

            cfg.CreateMap<PeoplePostViewModel, PeoplePutViewModel>().ReverseMap();
            cfg.CreateMap<PeopleViewModel, PeoplePostViewModel>().ReverseMap();

            cfg.CreateMap<EmergencyContactViewModel, EmergencyContactPostViewModel>().ReverseMap();

            cfg.CreateMap<TourViewModel, TourPostViewModel>().ReverseMap();
            cfg.CreateMap<TourViewModel, TourPutViewModel>().ReverseMap();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    public virtual async Task<AuthenticationTokenViewModel> Auth(HttpClient client, dynamic obj)
    {
        var objJson = JsonContent.Create(obj);
        var response = await client.PostAsync("", objJson);
        return await DeserializeResponse<AuthenticationTokenViewModel>(response);
    }

    public virtual async Task<T> Get<T>(HttpClient client, dynamic obj) where T : class
    {
        var response = await client.GetAsync($"{obj}");
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<T> GetById<T>(HttpClient client, dynamic obj) where T : class
    {
        var response = await client.GetAsync($"?Id={obj}");
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<int> GetCount(HttpClient client)
    {
        var response = await client.GetAsync("count");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return await DeserializeResponse<int>(response);
    }

    public virtual async Task<T> Post<T>(HttpClient client, dynamic obj) where T : class
    {
        var objJson = JsonContent.Create(obj);
        var response = await client.PostAsync("", objJson);
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<T> Put<T>(HttpClient client, dynamic obj) where T : class
    {
        var objJson = JsonContent.Create(obj);
        var response = await client.PutAsync("", objJson);
        return await DeserializeResponse<T>(response);
    }

    public async Task<T> Delete<T>(HttpClient client, dynamic id) where T : class
    {
        var response = await client.DeleteAsync($"{id}");
        return await DeserializeResponse<T>(response);
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            return JsonConvert.DeserializeObject<T>(content) ?? 
                throw new Exception("Deserialized object is null");
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new Exception(content);
        }

        throw new Exception($"Unexpected response status code: {response.StatusCode}, {response}");
    }
}
