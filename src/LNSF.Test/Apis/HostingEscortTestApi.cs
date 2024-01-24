using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingEscortTestApi : GlobalClientRequest
{
    [Fact]
    public async Task AddEscortToHosting_HostingEscort_OK()
    {
        var hosting = await GetHosting();
        var escort1 = await GetEscort();
        var escort2 = await GetEscort();
        var hostingEscort1 = new HostingEscortViewModelFake(hostingId: hosting.Id, escortId: escort1.Id).Generate();
        var hostingEscort2 = new HostingEscortViewModelFake(hostingId: hosting.Id, escortId: escort2.Id).Generate();

        var countBefore = await GetCount(_hostingEscortClient);
        var hostingEscortPosted1 = await Post<HostingEscortViewModel>(_addEscortToHostingClient, hostingEscort1);
        var hostingEscortPosted2 = await Post<HostingEscortViewModel>(_addEscortToHostingClient, hostingEscort2);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore + 2, countAfter);
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
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);
        var hostingWithoutConflict = await GetHosting(checkIn: checkOut.AddDays(1), checkOut: checkOut.AddDays(5));
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hostingWithoutConflict.Id, escortId: escort.Id).Generate();

        var countBefore = await GetCount(_hostingEscortClient);
        var hostingEscortWithoutConflictPosted = await Post<HostingEscortViewModel>(_addEscortToHostingClient, hostingEscortFake);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(hostingEscortFake, hostingEscortWithoutConflictPosted);
    }

    [Fact]
    public async Task AddEscortToHosting_HostingEscortWithConflicDates_Conflict()
    {
        var checkIn = new Bogus.DataSets.Date().Past().AddDays(-5);
        var checkOut = new Bogus.DataSets.Date().Future().AddDays(5);
        var hosting = await GetHosting(checkIn: checkIn, checkOut: checkOut);
        var escort = await GetEscort();
        var escortHosting = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);

        var hostingWithConflictBeforeCheckInAndDuringCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut).Generate();
        var hostingWithConflictBeforeCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictBeforeCheckInAndBeforeCheckIn = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(-5), checkOut: checkIn.AddDays(-1)).Generate();
        var hostingWithConflictBetweenCheckInAndCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn.AddDays(1), checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictDuringCheckInAndBeforeCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkIn, checkOut: checkOut.AddDays(-1)).Generate();
        var hostingWithConflictBeforeCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut.AddDays(-1), checkOut: checkOut.AddDays(1)).Generate();
        var hostingWithConflictDuringCheckOutAndAfterCheckOut = new HostingViewModelFake(id: hosting.Id, patientId: hosting.PatientId, checkIn: checkOut, checkOut: checkOut.AddDays(1)).Generate();

        var hostingEscortWithConflictBeforeCheckInAndDuringCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckInAndDuringCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBeforeCheckInAndBeforeCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckInAndBeforeCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBeforeCheckInAndBeforeCheckIn = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckInAndBeforeCheckIn.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBetweenCheckInAndCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBetweenCheckInAndCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictDuringCheckInAndBeforeCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictDuringCheckInAndBeforeCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictBeforeCheckOutAndAfterCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictBeforeCheckOutAndAfterCheckOut.Id, escortId: escort.Id).Generate();
        var hostingEscortWithConflictDuringCheckOutAndAfterCheckOut = new HostingEscortViewModelFake(hostingId: hostingWithConflictDuringCheckOutAndAfterCheckOut.Id, escortId: escort.Id).Generate();

        var countBefore = await GetCount(_hostingEscortClient);
        var exceptionBeforeCheckInAndDuringCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndDuringCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckInAndBeforeCheckIn = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckInAndBeforeCheckIn);
        var exceptionBetweenCheckInAndCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBetweenCheckInAndCheckOut);
        var exceptionDuringCheckInAndBeforeCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictDuringCheckInAndBeforeCheckOut);
        var exceptionBeforeCheckOutAndAfterCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictBeforeCheckOutAndAfterCheckOut);
        var exceptionDuringCheckOutAndAfterCheckOut = await Post<AppException>(_addEscortToHostingClient, hostingEscortWithConflictDuringCheckOutAndAfterCheckOut);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore, countAfter);
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
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: 0, escortId: escort.Id).Generate();

        var countBefore = await GetCount(_hostingEscortClient);
        var exception = await Post<AppException>(_addEscortToHostingClient, hostingEscortFake);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddEscortToHosting_HostingEscortWithNotExistsEscortId_NotFound()
    {
        var hosting = await GetHosting();
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hosting.Id, escortId: 0).Generate();

        var countBefore = await GetCount(_hostingEscortClient);
        var exception = await Post<AppException>(_addEscortToHostingClient, hostingEscortFake);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveEscortFromHosting_HostingEscort_Ok()
    {
        var hostingEscort = await GetAddEscortToHosting();

        var countBefore = await GetCount(_hostingEscortClient);
        var hostingEscortDeleted = await DeleteByBody<HostingEscortViewModel>(_removeEscortFromHostingClient, hostingEscort);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(hostingEscort, hostingEscortDeleted);
    }

    [Fact]
    public async Task RemoveEscortFromHosting_HostingEscortWithNotExistsHosting_NotFound()
    {
        var escort = await GetEscort();
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: 0, escortId: escort.Id).Generate();

        var countBefore = await GetCount(_hostingEscortClient);
        var exception = await DeleteByBody<AppException>(_removeEscortFromHostingClient, hostingEscortFake);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveEscortFromHosting_HostingEscortWithNotExistsEscort_NotFound()
    {
        var hosting = await GetHosting();
        var hostingEscortFake = new HostingEscortViewModelFake(hostingId: hosting.Id, escortId: 0).Generate();

        var countBefore = await GetCount(_hostingEscortClient);
        var exception = await DeleteByBody<AppException>(_removeEscortFromHostingClient, hostingEscortFake);
        var countAfter = await GetCount(_hostingEscortClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
