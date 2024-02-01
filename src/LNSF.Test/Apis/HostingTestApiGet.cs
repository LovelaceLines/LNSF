using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryHosting_Ok()
    {
        var hosting = await GetHosting();

        var hostingQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id));

        Assert.Equivalent(hosting.Id, hostingQueried.Id);
        Assert.Equivalent(hosting.PatientId, hostingQueried.PatientId);
        Assert.Equivalent(hosting.CheckIn, hostingQueried.CheckIn);
        Assert.Equivalent(hosting.CheckOut, hostingQueried.CheckOut);
    }

    [Fact]
    public async Task QueryHostingCheckIn_Ok()
    {
        var hosting = await GetHosting();

        var hostingCheckInQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, checkIn: hosting.CheckIn));

        Assert.Equivalent(hosting.CheckIn, hostingCheckInQueried.CheckIn);
    }

    [Fact]
    public async Task QueryHostingCheckOut_Ok()
    {
        var hosting = await GetHosting();

        var hostingCheckOutQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, checkOut: hosting.CheckOut));

        Assert.Equivalent(hosting.CheckOut, hostingCheckOutQueried.CheckOut);
    }

    [Fact]
    public async Task QueryHostingCheckInCheckOut_Ok()
    {
        var hosting = await GetHosting();

        var hostingCheckInCheckOutQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, checkIn: hosting.CheckIn, checkOut: hosting.CheckOut));

        Assert.Equivalent(hosting.CheckIn, hostingCheckInCheckOutQueried.CheckIn);
        Assert.Equivalent(hosting.CheckOut, hostingCheckInCheckOutQueried.CheckOut);
    }

    [Fact]
    public async Task QueryHostingPatientId_Ok()
    {
        var hosting = await GetHosting();

        var hostingPatientIdQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, patientId: hosting.PatientId));

        Assert.Equivalent(hosting.PatientId, hostingPatientIdQueried.PatientId);
    }

    [Fact]
    public async Task QueryHostingEscortId_Ok()
    {
        var escort = await GetEscort();
        var hosting = await GetHosting();
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort.Id);

        var hostingEscortIdQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, escortId: escort.Id));

        Assert.Equivalent(hosting.Id, hostingEscortIdQueried.Id);
    }

    [Fact]
    public async Task QueryHostingActive_Ok()
    {
        var hosting = await GetHosting(checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        var hostingActiveQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, active: true));

        Assert.Equivalent(hosting.Id, hostingActiveQueried.Id);
    }

    [Fact]
    public async Task QueryHostingInactive_Ok()
    {
        var hosting = await GetHosting(checkIn: DateTime.Now.AddDays(-2), checkOut: DateTime.Now.AddDays(-1));

        var hostingInactiveQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, active: false));

        Assert.Equivalent(hosting.Id, hostingInactiveQueried.Id);
    }

    [Fact]
    public async Task QueryHostingGetPatient_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        patient.People = people;
        var hosting = await GetHosting(patientId: patient.Id);

        var hostingGetPatientQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, getPatient: true));

        Assert.Equivalent(patient, hostingGetPatientQueried.Patient);
    }

    [Fact]
    public async Task QueryHostingGetEscort_Ok()
    {
        var people1 = await GetPeople();
        var people2 = await GetPeople();
        var escort1 = await GetEscort(peopleId: people1.Id);
        escort1.People = people1;
        var escort2 = await GetEscort(peopleId: people2.Id);
        escort2.People = people2;
        var hosting = await GetHosting();
        var hostingEscort1 = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort1.Id);
        var hostingEscort2 = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort2.Id);

        var hostingGetEscortQueried = await QueryFirst<HostingViewModel>(_hostingClient, new HostingFilter(id: hosting.Id, getEscort: true));

        Assert.Equivalent(new List<EscortViewModel> { escort1, escort2 }, hostingGetEscortQueried.Escorts);
    }
}
