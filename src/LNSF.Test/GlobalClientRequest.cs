using Microsoft.IdentityModel.Tokens;

using LNSF.API.InputModels;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Utils;

using User = LNSF.Test.Fakers.User;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace LNSF.Test.Global;

public class GlobalClientRequest : HttpClientUtil
{
	public const string BaseUrl = "http://localhost:5065/api/";
	public readonly HttpClient _loginClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/login/") };
	public readonly HttpClient _refreshTokenClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/refresh-token/") };
	public readonly HttpClient _authUserClient = new() { BaseAddress = new Uri($"{BaseUrl}Auth/user/") };
	public readonly HttpClient _userClient = new() { BaseAddress = new Uri($"{BaseUrl}User/") };
	public readonly HttpClient _userPasswordClient = new() { BaseAddress = new Uri($"{BaseUrl}User/password/") };
	public readonly HttpClient _addUserToRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/add-user-to-role/") };
	public readonly HttpClient _removeUserFromRoleClient = new() { BaseAddress = new Uri($"{BaseUrl}User/remove-user-from-role/") };
	public readonly HttpClient _roleClient = new() { BaseAddress = new Uri($"{BaseUrl}Role/") };
	public readonly HttpClient _peopleClient = new() { BaseAddress = new Uri($"{BaseUrl}People/") };
	public readonly HttpClient _emergencyContactClient = new() { BaseAddress = new Uri($"{BaseUrl}EmergencyContact/") };
	public readonly HttpClient _tourClient = new() { BaseAddress = new Uri($"{BaseUrl}Tour/") };
	public readonly HttpClient _putAllClient = new() { BaseAddress = new Uri($"{BaseUrl}Tour/put-all/") };
	public readonly HttpClient _hospitalClient = new() { BaseAddress = new Uri($"{BaseUrl}Hospital/") };
	public readonly HttpClient _treatmentClient = new() { BaseAddress = new Uri($"{BaseUrl}Treatment/") };
	public readonly HttpClient _patientClient = new() { BaseAddress = new Uri($"{BaseUrl}Patient/") };
	public readonly HttpClient _addTreatmentToPatientClient = new() { BaseAddress = new Uri($"{BaseUrl}Patient/add-treatment-to-patient/") };
	public readonly HttpClient _removeTreatmentFromPatientClient = new() { BaseAddress = new Uri($"{BaseUrl}Patient/remove-treatment-from-patient/") };
	public readonly HttpClient _familyGroupProfileClient = new() { BaseAddress = new Uri($"{BaseUrl}FamilyGroupProfile/") };
	public readonly HttpClient _escortClient = new() { BaseAddress = new Uri($"{BaseUrl}Escort/") };
	public readonly HttpClient _hostingClient = new() { BaseAddress = new Uri($"{BaseUrl}Hosting/") };
	public readonly HttpClient _addEscortToHostingClient = new() { BaseAddress = new Uri($"{BaseUrl}Hosting/add-escort-to-hosting/") };
	public readonly HttpClient _removeEscortFromHostingClient = new() { BaseAddress = new Uri($"{BaseUrl}Hosting/remove-escort-from-hosting/") };
	public readonly HttpClient _roomClient = new() { BaseAddress = new Uri($"{BaseUrl}Room/") };
	public readonly HttpClient _addPeopleToRoomClient = new() { BaseAddress = new Uri($"{BaseUrl}People/add-people-to-room/") };
	public readonly HttpClient _removePeopleFromRoomClient = new() { BaseAddress = new Uri($"{BaseUrl}People/remove-people-from-room/") };
	public readonly HttpClient _notificationClient = new() { BaseAddress = new Uri($"{BaseUrl}Notification/") };
	public readonly HttpClient _notificationMarkAsReadClient = new() { BaseAddress = new Uri($"{BaseUrl}Notification/mark-as-read/") };

	#region GetEntityFake

	public async Task<UserToken> GetToken()
	{
		var user = await GetUser();
		var userRole = await GetUserRole(userId: user.Id, roleId: 1);
		return await GetToken(user.UserName, user.Password);
	}

	public async Task<UserToken> GetToken(string userName, string password)
	{
		var login = new LoginIM { UserName = userName, Password = password };
		return await PostFromBody<UserToken>(_loginClient, login);
	}

	public async Task<User> GetUser(User? fake = null)
	{
		fake ??= new UserFake().Generate();
		return await PostFromBody<User>(_userClient, fake);
	}

	public async Task<Role> GetRole(Role? fake = null)
	{
		fake ??= new RoleFake().Generate();
		return await PostFromBody<Role>(_roleClient, fake);
	}

	public async Task<UserRole> GetUserRole()
	{
		var user = await GetUser();
		var role = await GetRole();

		return await GetUserRole(user.Id, role.Id);
	}

	public async Task<UserRole> GetUserRole(int userId, int roleId)
	{
		var model = new UserRole { UserId = userId, RoleId = roleId };

		var userRole = await PostFromBody<UserRole>(_addUserToRoleClient, model);

		return userRole;
	}

	public async Task<People> GetPeople()
	{
		var fake = new PeopleFake().Generate();
		return await PostFromBody<People>(_peopleClient, fake);
	}

	public async Task<EmergencyContact> GetEmergencyContact()
	{
		var people = await GetPeople();
		var fake = new EmergencyContactFake(peopleId: people.Id).Generate();
		return await PostFromBody<EmergencyContact>(_emergencyContactClient, fake);
	}

	public async Task<Tour> GetTour(int? openTourId = null, int? peopleId = null)
	{
		if (openTourId.HasValue)
		{
			var result = await GetFromQueryId<QueryResult<Tour>>(_tourClient, openTourId.Value);
			var tour = result.Items.Single();
			return await PutFromBody<Tour>(_tourClient, tour);
		}
		if (!peopleId.HasValue)
		{
			var people = await GetPeople();
			peopleId = people.Id;
		}
		var fake = new TourFake(peopleId: peopleId).Generate();
		return await PostFromBody<Tour>(_tourClient, fake);
	}

	public async Task<Hospital> GetHospital()
	{
		var fake = new HospitalFake().Generate();
		return await PostFromBody<Hospital>(_hospitalClient, fake);
	}

	public async Task<Patient> GetPatient(int? peopleId = null)
	{
		if (!peopleId.HasValue)
		{
			var people = await GetPeople();
			peopleId = people.Id;
		}
		var hospital = await GetHospital();
		var fake = new PatientFake(peopleId: peopleId, hospitalId: hospital.Id).Generate();
		return await PostFromBody<Patient>(_patientClient, fake);
	}

	public async Task<Treatment> GetTreatment()
	{
		var fake = new TreatmentFake().Generate();
		return await PostFromBody<Treatment>(_treatmentClient, fake);
	}

	public async Task<PatientTreatment> GetPatientTreatment()
	{
		var patient = await GetPatient();
		var treatment = await GetTreatment();
		var fake = new PatientTreatment() { PatientId = patient.Id, TreatmentId = treatment.Id };
		return await PostFromBody<PatientTreatment>(_addTreatmentToPatientClient, fake);
	}

	public async Task<FamilyGroupProfile> GetFamilyGroupProfile()
	{
		var patient = await GetPatient();
		var fake = new FamilyGroupProfileFake(patientId: patient.Id).Generate();
		return await PostFromBody<FamilyGroupProfile>(_familyGroupProfileClient, fake);
	}

	public async Task<Escort> GetEscort()
	{
		var people = await GetPeople();
		var fake = new EscortFake(peopleId: people.Id).Generate();
		return await PostFromBody<Escort>(_escortClient, fake);
	}

	public async Task<Hosting> GetHosting(DateTime? checkIn = null, DateTime? checkOut = null, int? patientId = null)
	{
		if (!patientId.HasValue)
		{
			var patient = await GetPatient();
			patientId = patient.Id;
		}
		var fake = new HostingFake(patientId: patientId, checkIn: checkIn, checkOut: checkOut).Generate();
		return await PostFromBody<Hosting>(_hostingClient, fake);
	}

	public async Task<HostingEscort> GetHostingEscort(int hostingId, int? escortId = null)
	{
		if (!escortId.HasValue)
		{
			var escort = await GetEscort();
			escortId = escort.Id;
		}
		var result = await GetFromQueryId<QueryResult<Hosting>>(_hostingClient, hostingId);
		var hosting = result.Items.Single();
		var hostingEscortFake = new HostingEscort() { HostingId = hosting.Id, EscortId = escortId.Value };
		var hostingEscort = await PostFromBody<HostingEscort>(_addEscortToHostingClient, hostingEscortFake);
		return hostingEscort;
	}

	public async Task<Room> GetRoom(bool? available = null, int? beds = null)
	{
		var fake = new RoomFake(available: available, beds: beds).Generate();
		return await PostFromBody<Room>(_roomClient, fake);
	}

	public async Task<PeopleRoomHosting> GetPeopleRoomHosting(int? peopleId = null, int? roomId = null, int? hostingId = null, int? patientId = null)
	{
		if (!peopleId.HasValue)
		{
			var people = await GetPeople();
			peopleId = people.Id;
			var patient = await GetPatient(peopleId: peopleId);
			patientId = patient.Id;
		}
		if (!roomId.HasValue)
		{
			var room = await GetRoom(available: true, beds: 4);
			roomId = room.Id;
		}
		if (!hostingId.HasValue)
		{
			var hosting = await GetHosting(patientId: patientId);
			hostingId = hosting.Id;
		}
		var fake = new PeopleRoomHosting() { PeopleId = peopleId.Value, RoomId = roomId.Value, HostingId = hostingId.Value };
		return await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, fake);
	}

	public async Task<Notification> GetNotification()
	{
		var fake = new NotificationFake().Generate();
		var result = await PostFromBody<Notification>(_notificationClient, fake);
		return result;
	}

	#endregion
}
