using LNSF.Api.ViewModels;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace LNSF.Test;

public class GlobalClientRequest
{
    public const string BaseUrl = "http://localhost:5206/api/";
    public string _acessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6ImF0K2p3dCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJnZW9yZ2VkZXYiLCJyb2xlIjpbIkRlc2Vudm9sdmVkb3IiLCJBZG1pbmlzdHJhZG9yIiwiQXNzaXN0ZW50ZVNvY2lhbCJdLCJuYmYiOjE3MDYxNDI0NzcsImV4cCI6MTcwNjE0OTY3NywiaWF0IjoxNzA2MTQyNDc3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUyMDYiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUyMDYifQ.-HCJy9Xc3gnCienYj7p6gTig83s16xOFKyHMyDc9-3Y";
    public string _refreshToken = "";
    public readonly HttpClient _authClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/") };
    public readonly HttpClient _loginClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/login") };
    public readonly HttpClient _refreshTokenClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/refresh-token") };
    public readonly HttpClient _authUserClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/user") };
    public readonly HttpClient _userClient = new() { BaseAddress = new Uri($"{BaseUrl}User/") };
    public readonly HttpClient _userRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}UserRole/") };
    public readonly HttpClient _addUserToRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/add-user-to-role/") };
    public readonly HttpClient _removeUserFromRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/remove-user-from-role/") };
    public readonly HttpClient _roleClient = new() { BaseAddress = new Uri($"{BaseUrl}Role/") };
    public readonly HttpClient _peopleClient = new() { BaseAddress = new Uri($"{BaseUrl}People/") };
    public readonly HttpClient _addPeopleToRoomClient = new() { BaseAddress = new Uri($"{BaseUrl}People/add-people-to-room/") };
    public readonly HttpClient _removePeopleFromRoom = new() { BaseAddress = new Uri($"{BaseUrl}People/remove-people-from-room/") };
    public readonly HttpClient _peopleRoomHostingClient = new() { BaseAddress = new Uri($"{BaseUrl}PeopleRoomHosting/") };
    public readonly HttpClient _roomClient = new() { BaseAddress = new Uri($"{BaseUrl}Room/") };
    public readonly HttpClient _emergencyContactClient = new() { BaseAddress = new Uri($"{BaseUrl}EmergencyContact/") };
    public readonly HttpClient _tourClient = new() { BaseAddress = new Uri($"{BaseUrl}Tour/") };
    public readonly HttpClient _putAllClient = new() { BaseAddress = new Uri($"{BaseUrl}Tour/put-all") };
    public readonly HttpClient _escortClient = new() { BaseAddress = new Uri($"{BaseUrl}Escort/") };
    public readonly HttpClient _hospitalClient = new() { BaseAddress = new Uri($"{BaseUrl}Hospital/") };
    public readonly HttpClient _treatmentClient = new() { BaseAddress = new Uri($"{BaseUrl}Treatment/") };
    public readonly HttpClient _patientClient = new() { BaseAddress = new Uri($"{BaseUrl}Patient/") };
    public readonly HttpClient _addTreatmentToPatient = new() { BaseAddress = new Uri($"{BaseUrl}Patient/add-treatment-to-patient/") };
    public readonly HttpClient _removeTreatmentFromPatient = new() { BaseAddress = new Uri($"{BaseUrl}Patient/remove-treatment-from-patient/") };
    public readonly HttpClient _patientTreatmentClient = new() { BaseAddress = new Uri($"{BaseUrl}PatientTreatment/") };
    public readonly HttpClient _hostingClient = new() { BaseAddress = new Uri($"{BaseUrl}Hosting/") };
    public readonly HttpClient _addEscortToHostingClient = new() { BaseAddress = new Uri($"{BaseUrl}Hosting/add-escort-to-hosting/") };
    public readonly HttpClient _removeEscortFromHostingClient = new() { BaseAddress = new Uri($"{BaseUrl}Hosting/remove-escort-from-hosting/") };
    public readonly HttpClient _hostingEscortClient = new() { BaseAddress = new Uri($"{BaseUrl}HostingEscort/") };
    public readonly HttpClient _familyGroupProfileClient = new() { BaseAddress = new Uri($"{BaseUrl}FamilyGroupProfile/") };
    public readonly HttpClient _serviceRecord = new() { BaseAddress = new Uri($"{BaseUrl}ServiceRecord/") };

    /// <summary>
    /// Executes a query using the provided HttpClient and filter, and returns the result as an instance of type T.
    /// Note: Pagination and OrderBy are unstable.
    /// </summary>
    public async Task<T> Query<T>(HttpClient client, dynamic filter) where T : class
    {
        var query = BuildQuery(filter);
        client = AddAuthorization(client);
        var response = await client.GetAsync($"?{query}");
        return await DeserializeResponse<T>(response);
    }

    public async Task<T> QueryFirst<T>(HttpClient client, dynamic filter) where T : class
    {
        List<T> query = await Query<List<T>>(client, filter);
        var queryFirst = query.First();
        return queryFirst;
    }

    private string BuildQuery(object filter)
    {
        var properties = filter.GetType().GetProperties();
        var queryParameters = new List<string>();

        foreach (var property in properties)
        {
            var value = property.GetValue(filter);

            if (value is null) continue;

            var propertyName = property.Name;
            var encodedValue = Uri.EscapeDataString(value is DateTime date ? date.ToString("yyyy-MM-ddTHH:mm:ss.fffffff") : value.ToString()!);
            queryParameters.Add($"{propertyName}={encodedValue}");
        }

        return string.Join("&", queryParameters);
    }

    public virtual async Task<T> GetById<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var response = await client.GetAsync($"?Id={obj}");
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<int> GetCount(HttpClient client)
    {
        client = AddAuthorization(client);
        var response = await client.GetAsync("count");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return await DeserializeResponse<int>(response);
    }

    public virtual async Task<T> Get<T>(HttpClient client) where T : class
    {
        client = AddAuthorization(client);
        var response = await client.GetAsync("");
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<T> Post<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var objJson = JsonContent.Create(obj);
        var response = await client.PostAsync("", objJson);
        return await DeserializeResponse<T>(response);
    }

    public virtual async Task<T> Put<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
        var objJson = JsonContent.Create(obj);
        var response = await client.PutAsync("", objJson);
        return await DeserializeResponse<T>(response);
    }

    public async Task<T> Delete<T>(HttpClient client, dynamic id) where T : class
    {
        client = AddAuthorization(client);
        var response = await client.DeleteAsync($"{id}");
        return await DeserializeResponse<T>(response);
    }

    public async Task<T> DeleteByBody<T>(HttpClient client, dynamic obj) where T : class
    {
        client = AddAuthorization(client);
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

        throw new Exception($"Unexpected response status code: {content},\n{response.StatusCode},\n{response},\n{response!.RequestMessage!.RequestUri!.AbsoluteUri}}}");
    }

    private HttpClient AddAuthorization(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _acessToken);
        client.DefaultRequestHeaders.Add("RefreshToken", _refreshToken);
        return client;
    }

    #region GetEntityFake

    public async Task<UserViewModel> GetUser(string? userName = null, string? email = null, string? phoneNumber = null, string? password = null, string? role = null)
    {
        var userFake = new UserPostViewModelFake(userName, email, phoneNumber, password).Generate();
        var user = await Post<UserViewModel>(_userClient, userFake);

        if (role.IsNullOrEmpty()) return user;

        return await Post<UserViewModel>(_addUserToRoleClient, new UserRoleViewModel() { UserId = user.Id, RoleName = role! });
    }

    /// <returns>A new room if the id is null, otherwise the updated room.</returns>
    public async Task<RoomViewModel> GetRoom(int? id = null, bool? available = null, string? number = null, int? beds = null, int? storey = null)
    {
        if (!id.HasValue)
            return await Post<RoomViewModel>(_roomClient, new RoomPostViewModelFake(available: available, number: number, beds: beds, storey: storey).Generate());

        return await Put<RoomViewModel>(_roomClient, new RoomViewModelFake(id: id.Value, available: available, number: number, beds: beds, storey: storey).Generate());
    }

    public async Task<PeopleViewModel> GetPeople(string? name = null, Gender? gender = null, DateTime? birthDate = null, MaritalStatus? maritalStatus = null, RaceColor? raceColor = null, string? email = null, string? rg = null, string? issuingBody = null, string? cpf = null, string? street = null, string? houseNumber = null, string? neighborhood = null, string? city = null, string? state = null, string? phone = null, string? note = null) =>
        await Post<PeopleViewModel>(_peopleClient, new PeoplePostViewModelFake(name: name, gender: gender, birthDate: birthDate, maritalStatus: maritalStatus, raceColor: raceColor, email: email, rg: rg, issuingBody: issuingBody, cpf: cpf, street: street, houseNumber: houseNumber, neighborhood: neighborhood, city: city, state: state, phone: phone, note: note).Generate());

    public async Task<PeopleRoomHostingViewModel> GetPeopleRoomHosting(int? peopleId = null, int? roomId = null, int? hostingId = null, int? beds = null)
    {
        var patient = new PatientViewModel();

        if (!peopleId.HasValue)
        {
            patient = await GetPatient();
            peopleId = patient.PeopleId;
        }

        if (!roomId.HasValue)
        {
            var room = await GetRoom(available: true, beds: beds ?? 2);
            roomId = room.Id;
        }

        if (!hostingId.HasValue)
        {
            var hosting = await GetHosting(patientId: patient.Id);
            hostingId = hosting.Id;
        }

        var peopleRoomHostingFake = new PeopleRoomHostingViewModelFake(peopleId: peopleId.Value, roomId: roomId.Value, hostingId: hostingId.Value).Generate();
        return await Post<PeopleRoomHostingViewModel>(_addPeopleToRoomClient, peopleRoomHostingFake);
    }

    public async Task<EmergencyContactViewModel> GetEmergencyContact(int? id = null, int? peopleId = null, string? name = null, string? phone = null)
    {
        if (!peopleId.HasValue)
        {
            var people = await GetPeople();
            peopleId = people.Id;
        }

        if (!id.HasValue)
            return await Post<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactPostViewModelFake(peopleId: peopleId.Value, name: name, phone: phone).Generate());

        if (id.HasValue && peopleId.HasValue)
            return await Put<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactViewModelFake(id: id.Value, peopleId: peopleId.Value, name: name, phone: phone).Generate());

        throw new AppException("Invalid parameters! You must provide id and peopleId or only peopleId.", HttpStatusCode.BadRequest);
    }

    public async Task<TourViewModel> GetTour(int id = 0, int peopleId = 0)
    {
        if (peopleId == 0)
        {
            var people = await GetPeople();
            peopleId = people.Id;
        }

        if (id == 0)
            return await Post<TourViewModel>(_tourClient, new TourPostViewModelFake(peopleId: peopleId).Generate());

        return await Put<TourViewModel>(_tourClient, new TourPutViewModelFake(id: id, peopleId: peopleId).Generate());
    }

    public async Task<HospitalViewModel> GetHospital(int? id = null, string? name = null, string? acronym = null)
    {
        if (!id.HasValue)
            return await Post<HospitalViewModel>(_hospitalClient, new HospitalPostViewModelFake(name: name, acronym: acronym).Generate());

        return await Put<HospitalViewModel>(_hospitalClient, new HospitalViewModelFake(id.Value, name: name, acronym: acronym).Generate());
    }

    public async Task<TreatmentViewModel> GetTreatment(int? id = null, string? name = null, TypeTreatment? type = null)
    {
        if (!id.HasValue)
            return await Post<TreatmentViewModel>(_treatmentClient, new TreatmentPostViewModelFake(name: name, type: type).Generate());

        return await Put<TreatmentViewModel>(_treatmentClient, new TreatmentViewModelFake(id.Value, name: name, type: type).Generate());
    }

    public async Task<PatientViewModel> GetPatient(int? id = null, int? peopleId = null, int? hospitalId = null, bool? socioeconomicRecord = null, bool? term = null)
    {
        if (!peopleId.HasValue)
        {
            var people = await GetPeople();
            peopleId = people.Id;
        }

        if (!hospitalId.HasValue)
        {
            var hospital = await GetHospital();
            hospitalId = hospital.Id;
        }

        if (!id.HasValue)
            return await Post<PatientViewModel>(_patientClient, new PatientPostViewModelFake(peopleId: peopleId.Value, hospitalId: hospitalId.Value, socioeconomicRecord: socioeconomicRecord, term: term).Generate());

        return await Put<PatientViewModel>(_patientClient, new PatientViewModelFake(id.Value, peopleId: peopleId.Value, hospitalId: hospitalId.Value, socioeconomicRecord: socioeconomicRecord, term: term).Generate());
    }

    public async Task<PatientTreatmentViewModel> GetPatientTreatment(int? patientId = null, int? treatmentId = null)
    {
        if (!patientId.HasValue)
        {
            var patient = await GetPatient();
            patientId = patient.Id;
        }

        if (!treatmentId.HasValue)
        {
            var treatment = await GetTreatment();
            treatmentId = treatment.Id;
        }

        var patientTreatmentFake = new PatientTreatmentViewModelFake(patientId: patientId.Value, treatmentId: treatmentId.Value).Generate();
        return await Post<PatientTreatmentViewModel>(_addTreatmentToPatient, patientTreatmentFake);
    }

    public async Task<EscortViewModel> GetEscort(int? id = null, int? peopleId = null)
    {
        if (!peopleId.HasValue)
        {
            var people = await GetPeople();
            peopleId = people.Id;
        }

        if (!id.HasValue)
            return await Post<EscortViewModel>(_escortClient, new EscortPostViewModelFake(peopleId: peopleId.Value).Generate());

        return await Put<EscortViewModel>(_escortClient, new EscortViewModelFake(id.Value, peopleId: peopleId.Value).Generate());
    }

    public async Task<HostingViewModel> GetHosting(int? id = null, int? patientId = null, DateTime? checkIn = null, DateTime? checkOut = null)
    {
        if (!patientId.HasValue)
        {
            var patient = await GetPatient();
            patientId = patient.Id;
        }

        if (!id.HasValue)
            return await Post<HostingViewModel>(_hostingClient, new HostingPostViewModelFake(patientId: patientId.Value, checkIn: checkIn, checkOut: checkOut).Generate());

        return await Put<HostingViewModel>(_hostingClient, new HostingViewModelFake(id.Value, patientId: patientId.Value, checkIn: checkIn, checkOut: checkOut).Generate());
    }

    public async Task<HostingEscortViewModel> GetHostingEscort(int? hostingId = null, int? escortId = null)
    {
        if (!hostingId.HasValue)
        {
            var hosting = await GetHosting();
            hostingId = hosting.Id;
        }

        if (!escortId.HasValue)
        {
            var escort = await GetEscort();
            escortId = escort.Id;
        }

        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hostingId.Value, escortId: escortId.Value).Generate();
        return await Post<HostingEscortViewModel>(_addEscortToHostingClient, hostingEscortFake);
    }

    public async Task<HostingEscortViewModel> GetAddEscortToHosting(int? hostingId = null, int? escortId = null)
    {
        if (!hostingId.HasValue)
        {
            var hosting = await GetHosting();
            hostingId = hosting.Id;
        }

        if (!escortId.HasValue)
        {
            var escort = await GetEscort();
            escortId = escort.Id;
        }

        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hostingId.Value, escortId: escortId.Value).Generate();
        return await Post<HostingEscortViewModel>(_addEscortToHostingClient, hostingEscortFake);
    }

    public async Task<FamilyGroupProfileViewModel> GetFamilyGroupProfile(int? id = null, int? patientId = null, string? name = null, string? kinship = null, int? age = null, string? profession = null, double? income = null)
    {
        if (!patientId.HasValue)
        {
            var patient = await GetPatient();
            patientId = patient.Id;
        }

        if (!id.HasValue)
            return await Post<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfilePostViewModelFake(patientId: patientId.Value, name: name, kinship: kinship, age: age, profession: profession, income: income).Generate());

        return await Put<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileViewModelFake(id.Value, patientId: patientId.Value, name: name, kinship: kinship, age: age, profession: profession, income: income).Generate());
    }

    public async Task<ServiceRecordViewModel> GetServiceRecord(int? id = null, int? patientId = null, List<SocialProgram>? socialProgram = null, string? socialProgramNote = null, DomicileType? domicileType = null, List<MaterialExternalWallsDomicile>? materialExternalWallsDomicile = null, AccessElectricalEnergy? accessElectricalEnergy = null, bool? hasWaterSupply = null, List<WayWaterSupply>? wayWaterSupply = null, SanitaryDrainage? sanitaryDrainage = null, GarbageCollection? garbageCollection = null, int? numberRooms = null, int? numberBedrooms = null, int? numberPeoplePerBedroom = null, DomicileHasAccessibility? domicileHasAccessibility = null, bool? isLocatedInRiskArea = null, bool? isLocatedInDifficultAccessArea = null, bool? isLocatedInConflictViolenceArea = null, AccessToUnit? accessToUnit = null, string? accessToUnitNote = null, string? firstAttendanceReason = null, string? demandPresented = null, string? referrals = null, string? observations = null)
    {
        if (!patientId.HasValue)
        {
            var patient = await GetPatient();
            patientId = patient.Id;
        }

        if (!id.HasValue)
            return await Post<ServiceRecordViewModel>(_serviceRecord, new ServiceRecordPostViewModelFake(patientId: patientId.Value, socialProgram: socialProgram, socialProgramNote: socialProgramNote, domicileType: domicileType, materialExternalWallsDomicile: materialExternalWallsDomicile, accessElectricalEnergy: accessElectricalEnergy, hasWaterSupply: hasWaterSupply, wayWaterSupply: wayWaterSupply, sanitaryDrainage: sanitaryDrainage, garbageCollection: garbageCollection, numberRooms: numberRooms, numberBedrooms: numberBedrooms, numberPeoplePerBedroom: numberPeoplePerBedroom, domicileHasAccessibility: domicileHasAccessibility, isLocatedInRiskArea: isLocatedInRiskArea, isLocatedInDifficultAccessArea: isLocatedInDifficultAccessArea, isLocatedInConflictViolenceArea: isLocatedInConflictViolenceArea, accessToUnit: accessToUnit, accessToUnitNote: accessToUnitNote, firstAttendanceReason: firstAttendanceReason, demandPresented: demandPresented, referrals: referrals, observations: observations).Generate());

        return await Put<ServiceRecordViewModel>(_serviceRecord, new ServiceRecordViewModelFake(id: id.Value, patientId: patientId.Value, socialProgram: socialProgram, socialProgramNote: socialProgramNote, domicileType: domicileType, materialExternalWallsDomicile: materialExternalWallsDomicile, accessElectricalEnergy: accessElectricalEnergy, hasWaterSupply: hasWaterSupply, wayWaterSupply: wayWaterSupply, sanitaryDrainage: sanitaryDrainage, garbageCollection: garbageCollection, numberRooms: numberRooms, numberBedrooms: numberBedrooms, numberPeoplePerBedroom: numberPeoplePerBedroom, domicileHasAccessibility: domicileHasAccessibility, isLocatedInRiskArea: isLocatedInRiskArea, isLocatedInDifficultAccessArea: isLocatedInDifficultAccessArea, isLocatedInConflictViolenceArea: isLocatedInConflictViolenceArea, accessToUnit: accessToUnit, accessToUnitNote: accessToUnitNote, firstAttendanceReason: firstAttendanceReason, demandPresented: demandPresented, referrals: referrals, observations: observations).Generate());
    }

    #endregion
}
