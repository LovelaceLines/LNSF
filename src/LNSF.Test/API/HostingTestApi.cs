using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class HostingTestApi : GlobalClientRequest
{
	[Fact]
	public async Task QueryHosting_Ok()
	{
		var hosting = await GetHosting();

		var result = await GetFromQuery<QueryResult<Hosting>>(_hostingClient, new HostingFilter
		{
			Id = hosting.Id,
			PatientId = hosting.PatientId
		});
		var hostingQueried = result.Items.Single();

		Assert.Equivalent(hosting.Id, hostingQueried.Id);
		Assert.Equivalent(hosting.PatientId, hostingQueried.PatientId);
		Assert.Equivalent(hosting.CheckIn, hostingQueried.CheckIn);
		Assert.Equivalent(hosting.CheckOut, hostingQueried.CheckOut);
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Post_Hosting_Ok(bool hasCheckOut)
	{
		var patient = await GetPatient();
		var hostingFake = new HostingFake(patientId: patient.Id).Generate();
		if (!hasCheckOut) hostingFake.CheckOut = null;

		var hostingPosted = await PostFromBody<Hosting>(_hostingClient, hostingFake);

		Assert.Equal(hostingFake.Patient, hostingPosted.Patient);
		Assert.Equal(hostingFake.CheckIn, hostingPosted.CheckIn);
		Assert.Equal(hostingFake.CheckOut, hostingPosted.CheckOut);
	}

	[Fact]
	public async Task Post_HostingWithCheckInGreaterThanCheckOut_BadRequest()
	{
		var patient = await GetPatient();
		var checkIn = new Bogus.DataSets.Date().Future();
		var checkOut = new Bogus.DataSets.Date().Past();
		var hostingFake = new HostingFake(patientId: patient.Id, checkIn: checkIn, checkOut: checkOut).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFake);

		Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
	}

	[Fact]
	public async Task Post_HostingWithCloseHostingWithSomePatientId_Ok()
	{
		var hosting = await GetHosting();
		var hostingFake = new HostingFake(patientId: hosting.PatientId, checkIn: hosting.CheckOut!.Value.AddDays(1), checkOut: hosting.CheckOut!.Value.AddDays(5)).Generate();

		var hostingPosted = await PostFromBody<Hosting>(_hostingClient, hostingFake);

		Assert.Equal(hostingFake.PatientId, hostingPosted.PatientId);
		Assert.Equal(hostingFake.CheckIn, hostingPosted.CheckIn);
		Assert.Equal(hostingFake.CheckOut, hostingPosted.CheckOut);
	}

	[Fact]
	public async Task Post_HostingWithOpenHostingWithSomePatientId_Conflict()
	{
		var patient = await GetPatient();
		var openHostingFake = new HostingFake(patientId: patient.Id).Generate();
		openHostingFake.CheckOut = null;
		var openHostingPosted = await PostFromBody<Hosting>(_hostingClient, openHostingFake);
		var hostingFake = new HostingFake(patientId: patient.Id, checkIn: openHostingPosted.CheckIn.AddDays(1), checkOut: openHostingPosted.CheckIn.AddDays(5)).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Fact]
	public async Task Post_HostingWithConflictDates_Conflict()
	{
		var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
		var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
		var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

		var hostingFakeWithConflictBeforeCheckInAndDuringCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
		var hostingFakeWithConflictBeforeCheckInAndBeforeCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
		var hostingFakeWithConflictBeforeCheckInAndBeforeCheckIn = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
		var hostingFakeWithConflictBetweenCheckInAndCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
		var hostingFakeWithConflictDuringCheckInAndBeforeCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
		var hostingFakeWithConflictBeforeCheckOutAndAfterCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
		var hostingFakeWithConflictDuringCheckOutAndAfterCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();

		var exceptionBeforeCheckInAndDuringCheckOut = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFakeWithConflictBeforeCheckInAndDuringCheckOut);
		var exceptionBeforeCheckInAndBeforeCheckOut = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFakeWithConflictBeforeCheckInAndBeforeCheckOut);
		var exceptionBeforeCheckInAndBeforeCheckIn = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFakeWithConflictBeforeCheckInAndBeforeCheckIn);
		var exceptionBetweenCheckInAndCheckOut = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFakeWithConflictBetweenCheckInAndCheckOut);
		var exceptionDuringCheckInAndBeforeCheckOut = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFakeWithConflictDuringCheckInAndBeforeCheckOut);
		var exceptionBeforeCheckOutAndAfterCheckOut = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFakeWithConflictBeforeCheckOutAndAfterCheckOut);
		var exceptionDuringCheckOutAndAfterCheckOut = await PostFromBody<AppHttpResponse>(_hostingClient, hostingFakeWithConflictDuringCheckOutAndAfterCheckOut);

		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndDuringCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckIn.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBetweenCheckInAndCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckInAndBeforeCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckOutAndAfterCheckOut.StatusCode);
		Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckOutAndAfterCheckOut.StatusCode);
	}

	[Fact]
	public async Task Put_Hosting_Ok()
	{
		var hosting = await GetHosting();
		var hostingFake = new HostingFake(id: hosting.Id, patientId: hosting.PatientId).Generate();

		var hostingPutted = await PutFromBody<Hosting>(_hostingClient, hostingFake);

		Assert.Equivalent(hostingFake, hostingPutted);
	}

	[Fact]
	public async Task Put_ValidHostingUpdateDates_Ok()
	{
		var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
		var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
		var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

		var hostingFakeToPutWithBeforeCheckInAndDuringCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
		var hostingFakeToPutWithBeforeCheckInAndBeforeCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
		var hostingFakeToPutWithBeforeCheckInAndBeforeCheckIn = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
		var hostingFakeToPutWithBetweenCheckInAndCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
		var hostingFakeToPutWithDuringCheckInAndBeforeCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
		var hostingFakeToPutWithBeforeCheckOutAndAfterCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
		var hostingFakeToPutWithDuringCheckOutAndAfterCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();
		var hostingFakeToPutWithAfterCheckOutAndAfterCheckOut = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5)).Generate();

		var hostingPuttedWithBeforeCheckInAndDuringCheckOut = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithBeforeCheckInAndDuringCheckOut);
		var hostingPuttedWithBeforeCheckInAndBeforeCheckOut = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithBeforeCheckInAndBeforeCheckOut);
		var hostingPuttedWithBeforeCheckInAndBeforeCheckIn = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithBeforeCheckInAndBeforeCheckIn);
		var hostingPuttedWithBetweenCheckInAndCheckOut = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithBetweenCheckInAndCheckOut);
		var hostingPuttedWithDuringCheckInAndBeforeCheckOut = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithDuringCheckInAndBeforeCheckOut);
		var hostingPuttedWithBeforeCheckOutAndAfterCheckOut = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithBeforeCheckOutAndAfterCheckOut);
		var hostingPuttedWithDuringCheckOutAndAfterCheckOut = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithDuringCheckOutAndAfterCheckOut);
		var hostingPuttedWithAfterCheckOutAndAfterCheckOut = await PutFromBody<Hosting>(_hostingClient, hostingFakeToPutWithAfterCheckOutAndAfterCheckOut);

		Assert.Equivalent(hostingFakeToPutWithBeforeCheckInAndDuringCheckOut, hostingPuttedWithBeforeCheckInAndDuringCheckOut);
		Assert.Equivalent(hostingFakeToPutWithBeforeCheckInAndBeforeCheckOut, hostingPuttedWithBeforeCheckInAndBeforeCheckOut);
		Assert.Equivalent(hostingFakeToPutWithBeforeCheckInAndBeforeCheckIn, hostingPuttedWithBeforeCheckInAndBeforeCheckIn);
		Assert.Equivalent(hostingFakeToPutWithBetweenCheckInAndCheckOut, hostingPuttedWithBetweenCheckInAndCheckOut);
		Assert.Equivalent(hostingFakeToPutWithDuringCheckInAndBeforeCheckOut, hostingPuttedWithDuringCheckInAndBeforeCheckOut);
		Assert.Equivalent(hostingFakeToPutWithBeforeCheckOutAndAfterCheckOut, hostingPuttedWithBeforeCheckOutAndAfterCheckOut);
		Assert.Equivalent(hostingFakeToPutWithDuringCheckOutAndAfterCheckOut, hostingPuttedWithDuringCheckOutAndAfterCheckOut);
		Assert.Equivalent(hostingFakeToPutWithAfterCheckOutAndAfterCheckOut, hostingPuttedWithAfterCheckOutAndAfterCheckOut);
	}

	[Fact]
	public async Task Put_HostingWithOtherPatient_NotFound()
	{
		var patient = await GetPatient();
		var hosting = await GetHosting();
		var hostingFake = new HostingFake(id: hosting.Id, patientId: patient.Id).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_hostingClient, hostingFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Put_HostingWithCheckInGreaterThanCheckOut_BadRequest()
	{
		var hosting = await GetHosting();
		var checkIn = new Bogus.DataSets.Date().Future();
		var checkOut = new Bogus.DataSets.Date().Past();
		var hostingFake = new HostingFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_hostingClient, hostingFake);

		Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
	}
}
