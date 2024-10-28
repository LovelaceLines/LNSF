using LNSF.Domain.Entities;
using LNSF.Test.DTOs;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class PeopleRoomHostingTestApiPost : GlobalClientRequest
{
	[Fact]
	public async Task Post_AddPeopleToRoom_ValidPeoplePatientHotingRoom_OK()
	{
		var room = await GetRoom(beds: 1, available: true);
		var patient = await GetPatient();
		var hosting = await GetHosting(patientId: patient.Id);
		var prhFake = new PeopleRoomHosting { RoomId = room.Id, PeopleId = patient.PeopleId, HostingId = hosting.Id };

		var prhPosted = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(prhFake.RoomId, prhPosted.RoomId);
		Assert.Equal(prhFake.PeopleId, prhPosted.PeopleId);
		Assert.Equal(prhFake.HostingId, prhPosted.HostingId);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_ValidPeopleEscortHotingRoomWithEscort_OK()
	{
		var room = await GetRoom(beds: 1, available: true);
		var patient = await GetPatient();
		var escort = await GetEscort();
		var hosting = await GetHosting(patientId: patient.Id);
		var hostingEscort = await GetHostingEscort(escortId: escort.Id, hostingId: hosting.Id);
		var prhFake = new PeopleRoomHosting { RoomId = room.Id, PeopleId = escort.PeopleId, HostingId = hosting.Id };

		var prhPosted = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(prhFake.RoomId, prhPosted.RoomId);
		Assert.Equal(prhFake.PeopleId, prhPosted.PeopleId);
		Assert.Equal(prhFake.HostingId, prhPosted.HostingId);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_ValidPeopleEscortHotingRoomWithPatientAndEscort_Ok()
	{
		var room = await GetRoom(beds: 2, available: true);
		var patient = await GetPatient();
		var escort = await GetEscort();
		var hosting = await GetHosting(patientId: patient.Id);
		var hostingEscort = await GetHostingEscort(escortId: escort.Id, hostingId: hosting.Id);
		var prhFakeWithPatient = new PeopleRoomHosting { RoomId = room.Id, PeopleId = patient.PeopleId, HostingId = hosting.Id };
		var prhFakeWithEscort = new PeopleRoomHosting { RoomId = room.Id, PeopleId = escort.PeopleId, HostingId = hosting.Id };

		var peopleRoomHostingPostedWithPatient = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFakeWithPatient);
		var peopleRoomHostingPostedWithEscort = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFakeWithEscort);

		Assert.Equal(prhFakeWithPatient.RoomId, peopleRoomHostingPostedWithPatient.RoomId);
		Assert.Equal(prhFakeWithPatient.PeopleId, peopleRoomHostingPostedWithPatient.PeopleId);
		Assert.Equal(prhFakeWithPatient.HostingId, peopleRoomHostingPostedWithPatient.HostingId);
		Assert.Equal(prhFakeWithEscort.RoomId, peopleRoomHostingPostedWithEscort.RoomId);
		Assert.Equal(prhFakeWithEscort.PeopleId, peopleRoomHostingPostedWithEscort.PeopleId);
		Assert.Equal(prhFakeWithEscort.HostingId, peopleRoomHostingPostedWithEscort.HostingId);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_ValidPeoplePatientActiveHosting_OK()
	{
		var room = await GetRoom(beds: 1, available: true);
		var patient = await GetPatient();
		var hosting = await GetHosting(patientId: patient.Id,
			checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
			checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));
		var prhFake = new PeopleRoomHosting { RoomId = room.Id, PeopleId = patient.PeopleId, HostingId = hosting.Id };

		var prhPosted = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(prhFake.RoomId, prhPosted.RoomId);
		Assert.Equal(prhFake.PeopleId, prhPosted.PeopleId);
		Assert.Equal(prhFake.HostingId, prhPosted.HostingId);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_ValidPeoplePatientActiveHostingWithEscort_OK()
	{
		var room = await GetRoom(beds: 2, available: true);
		var patient = await GetPatient();
		var escort = await GetEscort();
		var hosting = await GetHosting(patientId: patient.Id,
			checkIn: DateTime.Now.AddDays(new Random().Next(1, 10) * -1),
			checkOut: DateTime.Now.AddDays(new Random().Next(1, 10)));
		var hostingEscort = await GetHostingEscort(escortId: escort.Id, hostingId: hosting.Id);
		var prhFakeWithPatient = new PeopleRoomHosting { RoomId = room.Id, PeopleId = patient.PeopleId, HostingId = hosting.Id };
		var prhFakeWithEscort = new PeopleRoomHosting { RoomId = room.Id, PeopleId = escort.PeopleId, HostingId = hosting.Id };


		var peopleRoomHostingPostedWithPatient = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFakeWithPatient);
		var peopleRoomHostingPostedWithEscort = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFakeWithEscort);


		Assert.Equal(prhFakeWithPatient.RoomId, peopleRoomHostingPostedWithPatient.RoomId);
		Assert.Equal(prhFakeWithPatient.PeopleId, peopleRoomHostingPostedWithPatient.PeopleId);
		Assert.Equal(prhFakeWithPatient.HostingId, peopleRoomHostingPostedWithPatient.HostingId);
		Assert.Equal(prhFakeWithEscort.RoomId, peopleRoomHostingPostedWithEscort.RoomId);
		Assert.Equal(prhFakeWithEscort.PeopleId, peopleRoomHostingPostedWithEscort.PeopleId);
		Assert.Equal(prhFakeWithEscort.HostingId, peopleRoomHostingPostedWithEscort.HostingId);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidHosting_NotFound()
	{
		var room = await GetRoom(available: true);
		var patient = await GetPatient();
		var prhFake = new PeopleRoomHosting { RoomId = room.Id, PeopleId = patient.PeopleId, HostingId = 0 };

		var exception = await PostFromBody<AppHttpResponse>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidRoom_NotFound()
	{
		var patient = await GetPatient();
		var hosting = await GetHosting(patientId: patient.Id);
		var prhFake = new PeopleRoomHosting { RoomId = 0, PeopleId = patient.PeopleId, HostingId = hosting.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithInvalidPeople_BadRequest()
	{
		var roomFake = await GetRoom(available: true);
		var patientFake = await GetPatient();
		var hostingFake = await GetHosting(patientId: patientFake.Id);
		var prhFake = new PeopleRoomHosting { RoomId = roomFake.Id, PeopleId = 0, HostingId = hostingFake.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithValidHostingPatientButInvalidPeople_BadRequest()
	{
		var room = await GetRoom(available: true);
		var hosting = await GetHosting();
		var prhFake = new PeopleRoomHosting { RoomId = room.Id, PeopleId = 0, HostingId = hosting.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithValidHostingPatientAndEscortButInvalidPeople_BadRequest()
	{
		var room = await GetRoom(available: true);
		var hosting = await GetHosting();
		var hostingEscort = await GetHostingEscort(hostingId: hosting.Id);
		var prhFake = new PeopleRoomHosting { RoomId = room.Id, PeopleId = 0, HostingId = hosting.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithRoomNotAvailable_Conflict()
	{
		var roomFake = await GetRoom(available: false);
		var patientFake = await GetPatient();
		var hostingFake = await GetHosting(patientId: patientFake.Id);
		var prhFake = new PeopleRoomHosting { RoomId = roomFake.Id, PeopleId = patientFake.PeopleId, HostingId = hostingFake.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addPeopleToRoomClient, prhFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Fact]
	public async Task Post_AddPeopleToRoom_InvalidPeoplePatientHotingRoomWithoutVacancyToPatientAndEscort_Conflict()
	{
		var room = await GetRoom(available: true, beds: 1);
		var patient = await GetPatient();
		var escort = await GetEscort();
		var hosting = await GetHosting(patientId: patient.Id);
		var hostingEscort = await GetHostingEscort(escortId: escort.Id, hostingId: hosting.Id);
		var peopleRoomHostingWithPatient = new PeopleRoomHosting { RoomId = room.Id, PeopleId = patient.PeopleId, HostingId = hosting.Id };
		var peopleRoomHostingPostedWithPatient = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, peopleRoomHostingWithPatient);
		var peopleRoomHostingWithEscort = new PeopleRoomHosting { RoomId = room.Id, PeopleId = escort.PeopleId, HostingId = hosting.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addPeopleToRoomClient, peopleRoomHostingWithEscort);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Fact]
	public async Task Delete_RemovePeopleFromRoom_ValidPeopleHostingWithPatient_OK()
	{
		var prh = await GetPeopleRoomHosting();

		var peopleRoomHostingRemoved = await DeleteFromBody<PeopleRoomHosting>(_removePeopleFromRoomClient, prh);

		Assert.Equal(prh.RoomId, peopleRoomHostingRemoved.RoomId);
		Assert.Equal(prh.PeopleId, peopleRoomHostingRemoved.PeopleId);
		Assert.Equal(prh.HostingId, peopleRoomHostingRemoved.HostingId);
	}

	[Fact]
	public async Task Delete_RemovePeopleFromRoom_ValidPeopleHostingWithEscort_OK()
	{
		var escort = await GetEscort();
		var patient = await GetPatient();
		var hosting = await GetHosting(patientId: patient.Id);
		var hostingEscort = await GetHostingEscort(hostingId: hosting.Id, escortId: escort.Id);
		var room = await GetRoom(available: true, beds: 2);
		var prhFakeWithPatient = new PeopleRoomHosting { PeopleId = patient.PeopleId, RoomId = room.Id, HostingId = hosting.Id };
		var peopleRoomHostingPostedWithPatient = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFakeWithPatient);
		var prhFakeWithEscort = new PeopleRoomHosting { PeopleId = escort.PeopleId, RoomId = room.Id, HostingId = hosting.Id };
		var peopleRoomHostingPostedWithEscort = await PostFromBody<PeopleRoomHosting>(_addPeopleToRoomClient, prhFakeWithEscort);

		var peopleRoomHostingRemovedWithEscort = await DeleteFromBody<PeopleRoomHosting>(_removePeopleFromRoomClient, peopleRoomHostingPostedWithEscort);

		Assert.Equivalent(peopleRoomHostingPostedWithEscort, peopleRoomHostingRemovedWithEscort);
	}

	[Fact]
	public async Task Delete_RemovePeopleFromRoom_InvalidPeopleHostingWithNotExistEntity_BadRequest()
	{
		var peopleRoomHosting = await GetPeopleRoomHosting();

		var exceptionRoom = await DeleteFromBody<AppHttpResponse>(_removePeopleFromRoomClient, new PeopleRoomHosting() { RoomId = 0, PeopleId = peopleRoomHosting.PeopleId, HostingId = peopleRoomHosting.HostingId });
		var exceptionPeople = await DeleteFromBody<AppHttpResponse>(_removePeopleFromRoomClient, new PeopleRoomHosting() { RoomId = peopleRoomHosting.RoomId, PeopleId = 0, HostingId = peopleRoomHosting.HostingId });
		var exceptionHosting = await DeleteFromBody<AppHttpResponse>(_removePeopleFromRoomClient, new PeopleRoomHosting() { RoomId = peopleRoomHosting.RoomId, PeopleId = peopleRoomHosting.PeopleId, HostingId = 0 });

		Assert.NotEqual(HttpStatusCode.OK, exceptionRoom.StatusCode);
		Assert.NotEqual(HttpStatusCode.OK, exceptionPeople.StatusCode);
		Assert.NotEqual(HttpStatusCode.OK, exceptionHosting.StatusCode);
		Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionRoom.StatusCode);
		Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionPeople.StatusCode);
		Assert.NotEqual(HttpStatusCode.InternalServerError, exceptionHosting.StatusCode);
	}
}
