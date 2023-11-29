using System.Net;
using System.Net.Http.Json;
using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using Microsoft.IdentityModel.Tokens;
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
    public readonly HttpClient _addPeopleToRoomClient = new() { BaseAddress = new Uri($"{BaseUrl}People/add-people-to-room/") };
    public readonly HttpClient _removePeopleFromRoom = new() { BaseAddress = new Uri($"{BaseUrl}People/remove-people-from-room/") };
    public readonly HttpClient _peopleRoomClient = new() { BaseAddress = new Uri($"{BaseUrl}PeopleRoom/") };
    public readonly HttpClient _roomClient = new() { BaseAddress = new Uri($"{BaseUrl}Room/") };
    public readonly HttpClient _emergencyContactClient = new() { BaseAddress = new Uri($"{BaseUrl}EmergencyContact/") };
    public readonly HttpClient _tourClient = new() { BaseAddress = new Uri($"{BaseUrl}Tour/") };
    public readonly HttpClient _escortClient = new() { BaseAddress = new Uri($"{BaseUrl}Escort/") };
    public readonly HttpClient _hospitalClient = new() { BaseAddress = new Uri($"{BaseUrl}Hospital/") };
    public readonly HttpClient _treatmentClient = new() { BaseAddress = new Uri($"{BaseUrl}Treatment/") };
    public readonly HttpClient _patientClient = new() { BaseAddress = new Uri($"{BaseUrl}Patient/") };
    public readonly HttpClient _hostingClient = new() { BaseAddress = new Uri($"{BaseUrl}Hosting/") };
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

            cfg.CreateMap<TreatmentViewModel, TreatmentPostViewModel>().ReverseMap();
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

    public async Task<T> DeleteByBody<T>(HttpClient client, dynamic obj) where T : class
    {
        var objJson = JsonContent.Create(obj);
        var request = new HttpRequestMessage(HttpMethod.Delete, "");
        request.Content = objJson;
        var response = await client.SendAsync(request);
        return await DeserializeResponse<T>(response);
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK || typeof(T) == typeof(AppException))
            return JsonConvert.DeserializeObject<T>(content) ?? 
                throw new Exception("Deserialized object is null");

        throw new Exception($"Unexpected response status code: {content}, {response.StatusCode}, {response}");
    }

    #region GetEntityFake

    /// <returns>A new room if the id is null, otherwise the updated room.</returns>
    public async Task<RoomViewModel> GetRoom(int? id = null, bool? available = null, string? number = null, int? beds = null, int? storey = null)
    {
        if (!id.HasValue) 
            return await Post<RoomViewModel>(_roomClient, new RoomPostViewModelFake(available, number, beds, storey).Generate());
        
        return await Put<RoomViewModel>(_roomClient, new RoomPutViewModelFake(id.Value, available, number, beds, storey).Generate());
    }

    public async Task<PeopleViewModel> GetPeople() =>
        await Post<PeopleViewModel>(_peopleClient, new PeoplePostViewModelFake().Generate());

    public async Task<PeopleRoomViewModel> GetPeopleRoom(int? patientId = null, int? roomId = null, int? hostingId = null)
    {
        int peopleId = 0;

        if (!patientId.HasValue)
        {
            var patient = await GetPatient();
            patientId = patient.Id;
            peopleId = patient.PeopleId;
        }

        if (!roomId.HasValue)
        {
            var room = await GetRoom(available: true, beds: 1);
            roomId = room.Id;
        }

        if (!hostingId.HasValue)
        {
            var hosting = await GetHosting(patientId: patientId.Value);
            hostingId = hosting.Id;
        }

        var peopleRoomFake = new PeopleRoomViewModelFake(roomId: roomId.Value, peopleId: peopleId, hostingId: hostingId.Value).Generate();
        return await Post<PeopleRoomViewModel>(_addPeopleToRoomClient, peopleRoomFake);
    }

    public async Task<TourViewModel> GetTour(int id = 0, int peopleId = 0)
    {
        if (peopleId == 0)
        {
            var people = await GetPeople();
            peopleId = people.Id;
        }

        if (id == 0)
            return await Post<TourViewModel>(_tourClient, new TourPostViewModelFake(peopleId).Generate());
        
        return await Put<TourViewModel>(_tourClient, new TourPutViewModelFake(id, peopleId).Generate());
    }

    public async Task<HospitalViewModel> GetHospital() =>
        await Post<HospitalViewModel>(_hospitalClient, new HospitalPostViewModelFake().Generate());

    public async Task<TreatmentViewModel> GetTreatment() =>
        await Post<TreatmentViewModel>(_treatmentClient, new TreatmentPostViewModelFake().Generate());
    
    public async Task<PatientViewModel> GetPatient(int numberTreatment = 1)
    {
        var people = await GetPeople();
        var hospital = await GetHospital();
        var treatmentIds = new List<int>();
        for (int i = 0; i < numberTreatment; i++)
        {
            var treatment = await GetTreatment();
            treatmentIds.Add(treatment.Id);
        }

        var patientFake = new PatientPostViewModelFake().Generate();
        patientFake.PeopleId = people.Id;
        patientFake.HospitalId = hospital.Id;
        patientFake.TreatmentIds = treatmentIds;

        return await Post<PatientViewModel>(_patientClient, patientFake);
    } 

    public async Task<EscortViewModel> GetEscort()
    {
        var people = await GetPeople();
        var escortFake = new EscortPostViewModelFake().Generate();
        escortFake.PeopleId = people.Id;
        
        return await Post<EscortViewModel>(_escortClient, escortFake);
    }

    public async Task<HostingViewModel> GetHosting(int? patientId = null, List<int>? escortIds = null, int numberEscorts = 1, bool patientHasCheckOut = true)
    {
        var patient = await GetPatient();
        
        if (escortIds == null) 
        {
            escortIds = new List<int>();
            for (int i = 0; i < numberEscorts; i++)
            {
                var escort = await GetEscort();
                escortIds.Add(escort.Id);
            }
        }

        var hostingFake = new HostingPostViewModelFake().Generate();
        hostingFake.PatientId = patientId.HasValue ? patientId.Value : patient.Id;
        hostingFake.EscortIds = escortIds;
        hostingFake.CheckOut = patientHasCheckOut ? hostingFake.CheckOut : null;

        return await Post<HostingViewModel>(_hostingClient, hostingFake);
    }

    #endregion
}
