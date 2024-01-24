using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingTestApiPost : GlobalClientRequest
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Post_Hosting_Ok(bool hasCheckOut)
    {
        var patient = await GetPatient();
        var hostingFake = new HostingPostViewModelFake(patientId: patient.Id).Generate();
        if (!hasCheckOut) hostingFake.CheckOut = null;

        var countBefore = await GetCount(_hostingClient);
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingFake, hostingPosted);
    }

    [Fact]
    public async Task Post_HostingWithCheckInGreaterThanCheckOut_BadRequest()
    {
        var patient = await GetPatient();
        var checkIn = new Bogus.DataSets.Date().Future();
        var checkOut = new Bogus.DataSets.Date().Past();
        var hostingFake = new HostingPostViewModelFake(patientId: patient.Id, checkIn: checkIn, checkOut: checkOut).Generate();

        var countBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_hostingClient, hostingFake);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task Post_HostingWithCloseHostingWithSomePatientId_Ok()
    {
        var hosting = await GetHosting();
        var hostingFake = new HostingPostViewModelFake(patientId: hosting.PatientId, checkIn: hosting.CheckOut!.Value.AddDays(1), checkOut: hosting.CheckOut!.Value.AddDays(5)).Generate();

        var countBefore = await GetCount(_hostingClient);
        var hostingPosted = await Post<HostingViewModel>(_hostingClient, hostingFake);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingFake, hostingPosted);
    }

    [Fact]
    public async Task Post_HostingWithOpenHostingWithSomePatientId_Conflict()
    {
        var patient = await GetPatient();
        var openHostingFake = new HostingPostViewModelFake(patientId: patient.Id).Generate();
        openHostingFake.CheckOut = null;
        var openHostingPosted = await Post<HostingViewModel>(_hostingClient, openHostingFake);
        var hostingFake = new HostingPostViewModelFake(patientId: patient.Id, checkIn: openHostingPosted.CheckIn.AddDays(1), checkOut: openHostingPosted.CheckIn.AddDays(5)).Generate();

        var countBefore = await GetCount(_hostingClient);
        var exception = await Post<AppException>(_hostingClient, hostingFake);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task Post_HostingWithConflictDates_Conflict()
    {
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

        var hostingFakeWithConflictBeforeCheckInAndDuringCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
        var hostingFakeWithConflictBeforeCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingFakeWithConflictBeforeCheckInAndBeforeCheckIn = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
        var hostingFakeWithConflictBetweenCheckInAndCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingFakeWithConflictDuringCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
        var hostingFakeWithConflictBeforeCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
        var hostingFakeWithConflictDuringCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();

        var countBefore = await GetCount(_hostingClient);
        var exceptionBeforeCheckInAndDuringCheckOut = await Post<AppException>(_hostingClient, hostingFakeWithConflictBeforeCheckInAndDuringCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckOut = await Post<AppException>(_hostingClient, hostingFakeWithConflictBeforeCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckIn = await Post<AppException>(_hostingClient, hostingFakeWithConflictBeforeCheckInAndBeforeCheckIn);
        var exceptionBetweenCheckInAndCheckOut = await Post<AppException>(_hostingClient, hostingFakeWithConflictBetweenCheckInAndCheckOut);
        var exceptionDuringCheckInAndBeforeCheckOut = await Post<AppException>(_hostingClient, hostingFakeWithConflictDuringCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckOutAndAfterCheckOut = await Post<AppException>(_hostingClient, hostingFakeWithConflictBeforeCheckOutAndAfterCheckOut);
        var exceptionDuringCheckOutAndAfterCheckOut = await Post<AppException>(_hostingClient, hostingFakeWithConflictDuringCheckOutAndAfterCheckOut);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndDuringCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckInAndBeforeCheckIn.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBetweenCheckInAndCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckInAndBeforeCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionBeforeCheckOutAndAfterCheckOut.StatusCode);
        Assert.Equal(HttpStatusCode.Conflict, exceptionDuringCheckOutAndAfterCheckOut.StatusCode);
    }
}
