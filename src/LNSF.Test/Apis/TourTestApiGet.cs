using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class TourTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryTour_Ok()
    {
        var tour = await GetTour();

        var tourQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id));

        Assert.Equivalent(tour.Id, tourQueried.Id);
        Assert.Equivalent(tour.PeopleId, tourQueried.PeopleId);
        Assert.Equivalent(tour.Input, tourQueried.Input);
        Assert.Equivalent(tour.Output, tourQueried.Output);
        Assert.Equivalent(tour.Note, tourQueried.Note);
    }

    [Fact]
    public async Task QueryTourOutput_Ok()
    {
        var tour = await GetTour();

        var tourOutputQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id, output: tour.Output));

        Assert.Equivalent(tour.Output, tourOutputQueried.Output);
    }

    [Fact]
    public async Task QueryTourInputWithOpenTour_Ok()
    {
        var openTour = await GetTour();

        var tourInputQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: openTour.Id, input: openTour.Input));

        Assert.Equivalent(openTour.Input, tourInputQueried.Input);
    }

    [Fact]
    public async Task QueryTourInputWithCloseTour_Ok()
    {
        var openTour = await GetTour();
        var closeTour = await GetTour(openTour.Id, openTour.PeopleId);

        var tourInputQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: closeTour.Id, input: closeTour.Input));

        Assert.Equivalent(closeTour.Input, tourInputQueried.Input);
    }

    [Fact]
    public async Task QueryTourNote_Ok()
    {
        var tour = await GetTour();

        var tourNoteQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id, note: tour.Note));

        Assert.Equivalent(tour.Note, tourNoteQueried.Note);
    }

    [Fact]
    public async Task QueryTourPeopleId_Ok()
    {
        var tour = await GetTour();

        var tourPeopleIdQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id, peopleId: tour.PeopleId));

        Assert.Equivalent(tour.PeopleId, tourPeopleIdQueried.PeopleId);
    }

    [Fact]
    public async Task QueryTourInOpen_Ok()
    {
        var openTour = await GetTour();

        var tourInOpenQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: openTour.Id, inOpen: true));

        Assert.Equivalent(openTour.Id, tourInOpenQueried.Id);
    }

    [Fact]
    public async Task QueryTourClosed_Ok()
    {
        var openTour = await GetTour();
        var tourClosed = await GetTour(openTour.Id, openTour.PeopleId);

        var tourClosedQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tourClosed.Id, inOpen: false));

        Assert.Equivalent(tourClosed.Id, tourClosedQueried.Id);
    }

    [Fact]
    public async Task QueryTourGetPeople_Ok()
    {
        var people = await GetPeople();
        var tour = await GetTour(peopleId: people.Id);

        var tourGetPeopleQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id, getPeople: true));

        Assert.Equivalent(people, tourGetPeopleQueried.People);
    }

    [Fact]
    public async Task QueryTourGlobalFilter_Ok()
    {
        var people = await GetPeople();
        var tour = await GetTour(peopleId: people.Id);
        var tourQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id));

        var tourPeopleNameQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id, globalFilter: people.Name));
        var tourNoteQueried = await QueryFirst<TourViewModel>(_tourClient, new TourFilter(id: tour.Id, globalFilter: tour.Note));

        Assert.Equivalent(tourQueried, tourPeopleNameQueried);
        Assert.Equivalent(tourQueried, tourNoteQueried);
    }
}
