using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class EscortTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidEscortId_Ok()
    {
        // Arrange - Escort
        var escort = await GetEscort();

        // Act - Escort
        var queryId = await Query<List<EscortViewModel>>(_escortClient, new EscortFilter(id: escort.Id));
        var escortIdQueried = queryId.First();

        Assert.Equivalent(escort.Id, escortIdQueried.Id);
    }

    [Fact]
    public async Task Get_ValidEscortPeopleId_Ok()
    {
        // Arrange - Escort
        var escort = await GetEscort();

        // Act - Escort
        var queryPeopleId = await Query<List<EscortViewModel>>(_escortClient, new EscortFilter(id: escort.Id, peopleId: escort.PeopleId));
        var escortPeopleIdQueried = queryPeopleId.First();

        Assert.Equivalent(escort.PeopleId, escortPeopleIdQueried.PeopleId);
    }

    [Fact]
    public async Task Get_ValidEscortActive_Ok()
    {
        // Arrange - Escort
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting = await GetHosting(checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        // Arrange - EscortHosting
        var escortHosting = await GetHostingEscort(hostingId: hosting.Id, escortId: escort.Id);

        // Act - Escort
        var queryActive = await Query<List<EscortViewModel>>(_escortClient, new EscortFilter(id: escort.Id, active: true));
        var escortActiveQueried = queryActive.First();

        Assert.Equivalent(escort, escortActiveQueried);
    }

    [Fact]
    public async Task Get_ValidEscortIsVeteran_Ok()
    {
        // Arrange - Escort
        var escort = await GetEscort();

        // Arrange - Hosting
        var hosting1 = await GetHosting(checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-9));
        var hosting2 = await GetHosting(checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        // Arrange - EscortHosting
        var escortHosting1 = await GetHostingEscort(hostingId: hosting1.Id, escortId: escort.Id);
        var escortHosting2 = await GetHostingEscort(hostingId: hosting2.Id, escortId: escort.Id);

        // Act - Escort
        var queryIsVeteran = await Query<List<EscortViewModel>>(_escortClient, new EscortFilter(id: escort.Id, isVeteran: true));
        var escortIsVeteranQueried = queryIsVeteran.First();

        Assert.Equivalent(escort, escortIsVeteranQueried);
    }

    [Fact]
    public async Task Get_ValidEscortGetPeople()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Escort
        var escort = await GetEscort(peopleId: people.Id);

        // Act - Escort
        var queryGetPeople = await Query<List<EscortViewModel>>(_escortClient, new EscortFilter(id: escort.Id, getPeople: true));
        var escortGetPeopleQueried = queryGetPeople.First();

        Assert.Equivalent(people, escortGetPeopleQueried.People);
    }

    [Fact]
    public async Task Get_ValidEscortGlobalFilter()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Escort
        var escort = await GetEscort(peopleId: people.Id);

        // Act - Escort
        var queryGlobalFilter = await Query<List<EscortViewModel>>(_escortClient, new EscortFilter(id: escort.Id, globalFilter: people.Name));
        var escortGlobalFilterQueried = queryGlobalFilter.First();

        Assert.Equivalent(escort, escortGlobalFilterQueried);
    }

}
