using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class EscortTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidEscort_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Escort
        var escortFake = new EscortPostViewModel { PeopleId = people.Id };

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var escortPosted = await Post<EscortViewModel>(_escortClient, escortFake);

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Act - Query
        var query = await Query<List<EscortViewModel>>(_escortClient, new EscortFilter(id: escortPosted.Id));
        var escortQueried = query.First();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equal(escortFake.PeopleId, escortPosted.PeopleId);
        Assert.Equal(escortPosted.Id, escortQueried.Id);
        Assert.Equal(escortPosted.PeopleId, escortQueried.PeopleId);
    }

    [Fact]
    public async Task Post_InvalidEscortWithNotExistsPeopleId_NotFound()
    {
        // Arrange - Escort
        var escortFake = new EscortPostViewModelFake(peopleId: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Assert - Escort
        var exception = await Post<AppException>(_escortClient, escortFake);

        // Arrange - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidEscortWithUsePeopleId_Conflict()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Escort
        var escortFake1 = new EscortPostViewModelFake(peopleId: people.Id).Generate();
        var escortPosted1 = await Post<EscortViewModel>(_escortClient, escortFake1);
        var escortFake2 = new EscortPostViewModelFake(peopleId: people.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var exception = await Post<AppException>(_escortClient, escortFake2);

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_InvalidEscort_NotFound(dynamic peopleId)
    {
        // Arrange - Escort
        var escort = await GetEscort();
        var escortToPut = new EscortViewModelFake(id: escort.Id, peopleId: peopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var exception = await Put<AppException>(_escortClient, escortToPut);

        // Arrange - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Put_InvalidEscortWithUsePeopleId_NotFound()
    {
        // Arrange - Escort
        var escort1 = await GetEscort();
        var escort2 = await GetEscort();
        var escortToPut = new EscortViewModelFake(id: escort1.Id, peopleId: escort2.PeopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var exception = await Put<AppException>(_escortClient, escortToPut);

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Delete_ValidEscort_Ok()
    {
        // Arrange - Escort
        var escort = await GetEscort();

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var escortDeleted = await Delete<EscortViewModel>(_escortClient, escort.Id);

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equal(escort.Id, escortDeleted.Id);
    }

    [Fact]
    public async Task Delete_InvalidEscortWithNotExistsId_NotFound()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Escort
        var escortFake = new EscortViewModelFake(id: 0, peopleId: people.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var exception = await Delete<AppException>(_escortClient, escortFake.Id);

        // Arrange - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
