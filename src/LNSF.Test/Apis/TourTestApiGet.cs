using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class TourTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidTourId_Ok()
    {
        // Arrange - Tour
        var tour = await GetTour();

        // Act - Tour
        var queryId = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id));
        var tourQueriedId = queryId.First();

        Assert.Equivalent(tour, tourQueriedId);
    }

    [Fact]
    public async Task Get_ValidTourPeopleId_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Tour
        var tour = await GetTour(peopleId: people.Id);

        // Act - Tour
        var queryPeopleId = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, peopleId: tour.PeopleId));
        var tourQueriedPeopleId = queryPeopleId.First();

        Assert.Equivalent(tour, tourQueriedPeopleId);
    }

    [Fact]
    public async Task Get_ValidTourOutput_Ok()
    {
        // Arrange - Tour
        var tour = await GetTour();

        // Act - Tour
        var queryOutput = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, output: tour.Output));
        var tourQueriedOutput = queryOutput.First();

        Assert.Equivalent(tour, tourQueriedOutput);
    }

    [Fact]
    public async Task Get_ValidTourNote_Ok()
    {
        // Arrange - Tour
        var tour = await GetTour();

        // Act - Tour
        var queryNote = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, note: tour.Note));
        var tourQueriedNote = queryNote.First();

        Assert.Equivalent(tour, tourQueriedNote);
    }

    [Fact]
    public async Task Get_ValidTourInOpen_Ok()
    {
        // Arrange - Tour
        var tour = await GetTour();

        // Act - Tour
        var queryInOpen = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, inOpen: true));
        var tourQueriedInOpen = queryInOpen.First();

        Assert.Equivalent(tour, tourQueriedInOpen);
    }

    [Fact]
    public async Task Get_ValidTourClosed_Ok()
    {
        // Arrange - Tour
        var tourInOpen = await GetTour();
        var tourClosed = await GetTour(tourInOpen.Id, tourInOpen.PeopleId);

        // Act - Tour
        var queryClosed = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tourClosed.Id, inOpen: false));
        var tourQueriedClosed = queryClosed.First();

        Assert.Equivalent(tourClosed, tourQueriedClosed);
    }

    [Fact]
    public async Task Get_ValidTourInput_Ok()
    {
        // Arrange - Tour
        var tour = await GetTour();

        // Act - Tour
        var queryInput = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, input: tour.Input));
        var tourQueriedInput = queryInput.First();

        Assert.Equivalent(tour, tourQueriedInput);
    }

    [Fact]
    public async Task Get_ValidTourGetPeople_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Tour
        var tour = await GetTour(peopleId: people.Id);

        // Act - Tour
        var queryGetPeople = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, getPeople: true));
        var tourQueriedGetPeople = queryGetPeople.First();

        Assert.Equivalent(people, tourQueriedGetPeople.People);
    }

    [Fact]
    public async Task Get_ValidTourGlobalQuery_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Tour
        var tour = await GetTour(peopleId: people.Id);

        // Act - Tour
        var queryGlobalNote = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, globalFilter: tour.Note));
        var queryGlobalPeopleName = await Query<List<TourViewModel>>(_tourClient, new TourFilter(id: tour.Id, globalFilter: people.Name));

        var tourQueriedGlobalNote = queryGlobalNote.First();
        var tourQueriedGlobalPeopleName = queryGlobalPeopleName.First();

        Assert.Equivalent(tour, tourQueriedGlobalNote);
        Assert.Equivalent(tour, tourQueriedGlobalPeopleName);
    }
}
