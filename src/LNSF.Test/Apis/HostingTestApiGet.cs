using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class HostingTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidHostingId_Ok()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Act - Hosting
        var queryId = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id));
        var hostingIdQueried = queryId.First();

        Assert.Equivalent(hosting.Id, hostingIdQueried.Id);
    }

    [Fact]
    public async Task Get_ValidHostingPatientId_Ok()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Act - Hosting
        var queryPatientId = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id, patientId: hosting.PatientId));
        var hostingPatientIdQueried = queryPatientId.First();

        Assert.Equivalent(hosting.PatientId, hostingPatientIdQueried.PatientId);
    }

    [Fact]
    public async Task Get_ValidHostingEscortId_Ok()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - HostingEscort
        var hostingEscort = await GetAddEscortToHosting(hostingId: hosting.Id);

        // Act - Hosting
        var queryEscortId = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id, escortId: hostingEscort.EscortId));
        var hostingEscortIdQueried = queryEscortId.First();

        Assert.Equivalent(hosting, hostingEscortIdQueried);
    }

    [Fact]
    public async Task Get_ValidHostingCheckIn_Ok()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Act - Hosting
        var queryCheckIn = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id, checkIn: hosting.CheckIn));
        var hostingCheckInQueried = queryCheckIn.First();

        Assert.Equivalent(hosting.CheckIn, hostingCheckInQueried.CheckIn);
    }

    [Fact]
    public async Task Get_ValidHostingCheckOut_Ok()
    {
        // Arrange - Hosting
        var hosting = await GetHosting();

        // Act - Hosting
        var queryCheckOut = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id, checkOut: hosting.CheckOut!.Value.AddSeconds(1)));
        var hostingCheckOutQueried = queryCheckOut.First();

        Assert.Equivalent(hosting.CheckOut, hostingCheckOutQueried.CheckOut);
    }

    [Fact]
    public async Task Get_ValidHostingActive_Ok()
    {
        // Arrange - Hosting
        var hosting = await GetHosting(checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        // Act - Hosting
        var queryActive = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id, active: true));
        var hostingActiveQueried = queryActive.First();

        Assert.Equivalent(hosting, hostingActiveQueried);
    }

    [Fact]
    public async Task Get_ValidHostingGetPatient_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Patient
        var patient = await GetPatient(peopleId: people.Id);
        patient.People = people;

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id);

        // Act - Hosting
        var queryGetPatient = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id, getPatient: true));
        var hostingGetPatientQueried = queryGetPatient.First();

        Assert.Equivalent(patient, hostingGetPatientQueried.Patient);
    }

    [Fact]
    public async Task Get_ValidHostingGetEscort_Ok()
    {
        // Arrange - People
        var people1 = await GetPeople();
        var people2 = await GetPeople();

        // Arrange - Escort
        var escort1 = await GetEscort(peopleId: people1.Id);
        escort1.People = people1;
        var escort2 = await GetEscort(peopleId: people2.Id);
        escort2.People = people2;

        var escorts = new List<EscortViewModel> { escort1, escort2 };

        // Arrange - Hosting
        var hosting = await GetHosting();

        // Arrange - HostingEscort
        var hostingEscort1 = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort1.Id);
        var hostingEscort2 = await GetAddEscortToHosting(hostingId: hosting.Id, escortId: escort2.Id);

        // Act - Hosting
        var queryGetEscort = await Query<List<HostingViewModel>>(_hostingClient, new HostingFilter(id: hosting.Id, getEscort: true));
        var hostingGetEscortQueried = queryGetEscort.First();

        Assert.Equivalent(escorts, hostingGetEscortQueried.Escorts);
    }
}
