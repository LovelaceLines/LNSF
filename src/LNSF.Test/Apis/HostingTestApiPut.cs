using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Put_Hosting_Ok()
    {
        var hosting = await GetHosting();
        var hostingFake = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId).Generate();

        var countBefore = await GetCount(_hostingClient);
        var hostingPutted = await Put<HostingViewModel>(_hostingClient, hostingFake);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(hostingFake, hostingPutted);
    }

    [Fact]
    public async Task Put_ValidHostingUpdateDates_Ok()
    {
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);

        var hostingFakeToPutWithBeforeCheckInAndDuringCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
        var hostingFakeToPutWithBeforeCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingFakeToPutWithBeforeCheckInAndBeforeCheckIn = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
        var hostingFakeToPutWithBetweenCheckInAndCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingFakeToPutWithDuringCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
        var hostingFakeToPutWithBeforeCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
        var hostingFakeToPutWithDuringCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();
        var hostingFakeToPutWithAfterCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5)).Generate();

        var countBefore = await GetCount(_hostingClient);
        var hostingPuttedWithBeforeCheckInAndDuringCheckOut = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithBeforeCheckInAndDuringCheckOut);
        var hostingPuttedWithBeforeCheckInAndBeforeCheckOut = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithBeforeCheckInAndBeforeCheckOut);
        var hostingPuttedWithBeforeCheckInAndBeforeCheckIn = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithBeforeCheckInAndBeforeCheckIn);
        var hostingPuttedWithBetweenCheckInAndCheckOut = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithBetweenCheckInAndCheckOut);
        var hostingPuttedWithDuringCheckInAndBeforeCheckOut = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithDuringCheckInAndBeforeCheckOut);
        var hostingPuttedWithBeforeCheckOutAndAfterCheckOut = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithBeforeCheckOutAndAfterCheckOut);
        var hostingPuttedWithDuringCheckOutAndAfterCheckOut = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithDuringCheckOutAndAfterCheckOut);
        var hostingPuttedWithAfterCheckOutAndAfterCheckOut = await Put<HostingViewModel>(_hostingClient, hostingFakeToPutWithAfterCheckOutAndAfterCheckOut);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore, countAfter);
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
        var hostingFake = new HostingViewModelFake(id: hosting.Id, patientId: patient.Id).Generate();

        var countBefore = await GetCount(_hostingClient);
        var exception = await Put<AppException>(_hostingClient, hostingFake);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Put_HostingWithCheckInGreaterThanCheckOut_BadRequest()
    {
        var hosting = await GetHosting();
        var checkIn = new Bogus.DataSets.Date().Future();
        var checkOut = new Bogus.DataSets.Date().Past();
        var hostingFake = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut).Generate();

        var countBefore = await GetCount(_hostingClient);
        var exception = await Put<AppException>(_hostingClient, hostingFake);
        var countAfter = await GetCount(_hostingClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }
}
