using LNSF.Domain.Entities;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class HostingEscortTestApi : GlobalClientRequest
{
	[Fact]
	public async Task AddEscortToHosting_HostingEscort_OK()
	{
		var hosting = await GetHosting();
		var escort1 = await GetEscort();
		var escort2 = await GetEscort();
		var hostingEscort1 = new HostingEscort() { HostingId = hosting.Id, EscortId = escort1.Id };
		var hostingEscort2 = new HostingEscort() { HostingId = hosting.Id, EscortId = escort2.Id };

		var hostingEscortPosted1 = await PostFromBody<HostingEscort>(_addEscortToHostingClient, hostingEscort1);
		var hostingEscortPosted2 = await PostFromBody<HostingEscort>(_addEscortToHostingClient, hostingEscort2);

		Assert.Equivalent(hostingEscort1, hostingEscortPosted1);
		Assert.Equivalent(hostingEscort2, hostingEscortPosted2);
	}

	[Fact]
	public async Task AddEscortToHosting_HostingWithoutConflictDates_Ok()
	{
		var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
		var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
		var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);
		var escort = await GetEscort();
		var hostingEscort = await GetHostingEscort(hostingId: hosting.Id, escortId: escort.Id);
		var hostingWithoutConflict = await GetHosting(checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5));
		var hostingEscortFake = new HostingEscort() { HostingId = hostingWithoutConflict.Id, EscortId = escort.Id };

		var hostingEscortWithoutConflictPosted = await PostFromBody<HostingEscort>(_addEscortToHostingClient, hostingEscortFake);

		Assert.Equivalent(hostingEscortFake, hostingEscortWithoutConflictPosted);
	}

	[Fact]
	public async Task AddEscortToHosting_HostingEscortWithConflicDates_Conflict()
	{
		var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
		var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
		var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);
		var escort = await GetEscort();
		var escortHosting = await GetHostingEscort(hostingId: hosting.Id, escortId: escort.Id);

		var hostingWithConflictBeforeCheckInAndDuringCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
		var hostingWithConflictBeforeCheckInAndBeforeCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
		var hostingWithConflictBeforeCheckInAndBeforeCheckIn = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
		var hostingWithConflictBetweenCheckInAndCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
		var hostingWithConflictDuringCheckInAndBeforeCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
		var hostingWithConflictBeforeCheckOutAndAfterCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
		var hostingWithConflictDuringCheckOutAndAfterCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();

		var hostingEscortWithConflictBeforeCheckInAndDuringCheckOut = new HostingEscort() { HostingId = hostingWithConflictBeforeCheckInAndDuringCheckOut.Id, EscortId = escort.Id };
		var hostingEscortWithConflictBeforeCheckInAndBeforeCheckOut = new HostingEscort() { HostingId = hostingWithConflictBeforeCheckInAndBeforeCheckOut.Id, EscortId = escort.Id };
		var hostingEscortWithConflictBeforeCheckInAndBeforeCheckIn = new HostingEscort() { HostingId = hostingWithConflictBeforeCheckInAndBeforeCheckIn.Id, EscortId = escort.Id };
		var hostingEscortWithConflictBetweenCheckInAndCheckOut = new HostingEscort() { HostingId = hostingWithConflictBetweenCheckInAndCheckOut.Id, EscortId = escort.Id };
		var hostingEscortWithConflictDuringCheckInAndBeforeCheckOut = new HostingEscort() { HostingId = hostingWithConflictDuringCheckInAndBeforeCheckOut.Id, EscortId = escort.Id };
		var hostingEscortWithConflictBeforeCheckOutAndAfterCheckOut = new HostingEscort() { HostingId = hostingWithConflictBeforeCheckOutAndAfterCheckOut.Id, EscortId = escort.Id };
		var hostingEscortWithConflictDuringCheckOutAndAfterCheckOut = new HostingEscort() { HostingId = hostingWithConflictDuringCheckOutAndAfterCheckOut.Id, EscortId = escort.Id };

		var exceptionBeforeCheckInAndDuringCheckOut = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndDuringCheckOut);
		var exceptionBeforeCheckInAndBeforeCheckOut = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndBeforeCheckOut);
		var exceptionBeforeCheckInAndBeforeCheckIn = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndBeforeCheckIn);
		var exceptionBetweenCheckInAndCheckOut = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortWithConflictBetweenCheckInAndCheckOut);
		var exceptionDuringCheckInAndBeforeCheckOut = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortWithConflictDuringCheckInAndBeforeCheckOut);
		var exceptionBeforeCheckOutAndAfterCheckOut = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckOutAndAfterCheckOut);
		var exceptionDuringCheckOutAndAfterCheckOut = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortWithConflictDuringCheckOutAndAfterCheckOut);

		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndDuringCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckIn.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBetweenCheckInAndCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckInAndBeforeCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckOutAndAfterCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckOutAndAfterCheckOut.StatusCode);

	}

	[Fact]
	public async Task AddEscortToHosting_HostingEscortWithNotExistsHosting_NotFound()
	{
		var escort = await GetEscort();
		var hostingEscortFake = new HostingEscort() { HostingId = 0, EscortId = escort.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task AddEscortToHosting_HostingEscortWithNotExistsEscortId_NotFound()
	{
		var hosting = await GetHosting();
		var hostingEscortFake = new HostingEscort() { HostingId = hosting.Id, EscortId = 0 };

		var exception = await PostFromBody<AppHttpResponse>(_addEscortToHostingClient, hostingEscortFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task RemoveEscortFromHosting_HostingEscort_Ok()
	{
		var hosting = await GetHosting();
		var hostingEscort = await GetHostingEscort(hostingId: hosting.Id);

		var hostingEscortDeleted = await DeleteFromBody<HostingEscort>(_removeEscortFromHostingClient, hostingEscort);

		Assert.Equivalent(hostingEscort, hostingEscortDeleted);
	}

	[Fact]
	public async Task RemoveEscortFromHosting_HostingEscortWithNotExistsHosting_NotFound()
	{
		var escort = await GetEscort();
		var hostingEscortFake = new HostingEscort() { HostingId = 0, EscortId = escort.Id };

		var exception = await DeleteFromBody<AppHttpResponse>(_removeEscortFromHostingClient, hostingEscortFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task RemoveEscortFromHosting_HostingEscortWithNotExistsEscort_NotFound()
	{
		var hosting = await GetHosting();
		var hostingEscortFake = new HostingEscort() { HostingId = hosting.Id, EscortId = 0 };

		var exception = await DeleteFromBody<AppHttpResponse>(_removeEscortFromHostingClient, hostingEscortFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}
}
