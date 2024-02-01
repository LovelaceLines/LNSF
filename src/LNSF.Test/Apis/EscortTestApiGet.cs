using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class EscortTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryEscort_Ok()
    {
        var escort = await GetEscort();

        var escortQueried = await QuerySingle<EscortViewModel>(_escortClient, new EscortFilter(id: escort.Id));

        Assert.Equivalent(escort.Id, escortQueried.Id);
        Assert.Equivalent(escort.PeopleId, escortQueried.PeopleId);
    }

    [Fact]
    public async Task QueryEscortPeopleId_Ok()
    {
        var escort = await GetEscort();

        var escortPeopleIdQueried = await QuerySingle<EscortViewModel>(_escortClient, new EscortFilter(id: escort.Id, peopleId: escort.PeopleId));

        Assert.Equivalent(escort.PeopleId, escortPeopleIdQueried.PeopleId);
    }

    [Fact]
    public async Task QueryEscortGetPeople()
    {
        var people = await GetPeople();
        var escort = await GetEscort(peopleId: people.Id);

        var escortGetPeopleQueried = await QuerySingle<EscortViewModel>(_escortClient, new EscortFilter(id: escort.Id, getPeople: true));

        Assert.Equivalent(people, escortGetPeopleQueried.People);
    }

    [Fact]
    public async Task QueryEscortActive_Ok()
    {
        var escort = await GetEscort();
        var hosting = await GetHosting(checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));
        var escortHosting = await GetHostingEscort(hostingId: hosting.Id, escortId: escort.Id);

        var escortActiveQueried = await QuerySingle<EscortViewModel>(_escortClient, new EscortFilter(id: escort.Id, active: true));

        Assert.Equivalent(escort, escortActiveQueried);
    }

    [Fact]
    public async Task QueryEscortInactive_Ok()
    {
        var escort = await GetEscort();
        var hosting = await GetHosting(checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-9));
        var escortHosting = await GetHostingEscort(hostingId: hosting.Id, escortId: escort.Id);

        var escortInactiveQueried = await QuerySingle<EscortViewModel>(_escortClient, new EscortFilter(id: escort.Id, active: false));

        Assert.Equivalent(escort, escortInactiveQueried);
    }

    [Fact]
    public async Task QueryEscortIsVeteran_Ok()
    {
        var escort = await GetEscort();
        var hosting1 = await GetHosting(checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-9));
        var escortHosting1 = await GetHostingEscort(hostingId: hosting1.Id, escortId: escort.Id);
        var hosting2 = await GetHosting(checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));
        var escortHosting2 = await GetHostingEscort(hostingId: hosting2.Id, escortId: escort.Id);

        var escortIsVeteranQueried = await QuerySingle<EscortViewModel>(_escortClient, new EscortFilter(id: escort.Id, isVeteran: true));

        Assert.Equivalent(escort, escortIsVeteranQueried);
    }

    [Fact]
    public async Task QueryEscortGlobalFilter()
    {
        var people = await GetPeople();
        var escort = await GetEscort(peopleId: people.Id);

        var escortPeopleNameQueried = await QuerySingle<EscortViewModel>(_escortClient, new EscortFilter(id: escort.Id, globalFilter: people.Name));

        Assert.Equivalent(escort, escortPeopleNameQueried);
    }

}
