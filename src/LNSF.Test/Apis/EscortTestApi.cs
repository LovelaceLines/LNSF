using LNSF.Api.ViewModels;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class EscortTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidEscort_Ok()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var escort = new EscortPostViewModel{ PeopleId = peoplePosted.Id };
        var escortPosted = await Post<EscortViewModel>(_escortClient, escort);

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equal(peoplePosted.Id, escortPosted.PeopleId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Post_InvalidEscort_BadRequest(dynamic peopleId)
    {
        // Arrange - Escort
        var escort = new EscortPostViewModel{ PeopleId = peopleId };

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Assert - Escort
        await Assert.ThrowsAsync<Exception>(() => Post<EscortViewModel>(_escortClient, escort));

        // Arrange - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Post_InvalidEscortRelationship_BadRequesto()
    {
        // Arrange - People
        var peopleFake = new PeoplePostViewModelFake().Generate();
        var peoplePosted = await Post<PeopleViewModel>(_peopleClient, peopleFake);

        // Arrange - Escort
        var escortPost = new EscortPostViewModel{ PeopleId = peoplePosted.Id};
        var escortPosted = await Post<EscortViewModel>(_escortClient, escortPost);

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        await Assert.ThrowsAsync<Exception>(() => Post<EscortViewModel>(_escortClient, escortPost));

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_ValidEscort_Ok()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, peopleFake1);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

        // Arrange - Escort
        var escortPost = new EscortPostViewModel{ PeopleId = peoplePosted1.Id};
        var escortPosted = await Post<EscortViewModel>(_escortClient, escortPost);

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var escortPut = new EscortViewModel{ Id = escortPosted.Id, PeopleId = peoplePosted2.Id };
        var escortPutted = await Put<EscortViewModel>(_escortClient, escortPut);

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(escortPut, escortPutted);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_InvalidEscort_BadRequest(dynamic peopleId)
    {
        // Arrange - Escort
        var escort = new EscortViewModel{ Id = 1, PeopleId = peopleId };

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Assert - Escort
        await Assert.ThrowsAsync<Exception>(() => Put<EscortViewModel>(_escortClient, escort));

        // Arrange - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }

    [Fact]
    public async Task Put_InvalidEscortRelationship_BadRequesto()
    {
        // Arrange - People
        var peopleFake1 = new PeoplePostViewModelFake().Generate();
        var peoplePosted1 = await Post<PeopleViewModel>(_peopleClient, peopleFake1);
        var peopleFake2 = new PeoplePostViewModelFake().Generate();
        var peoplePosted2 = await Post<PeopleViewModel>(_peopleClient, peopleFake2);

        // Arrange - Escort
        var escortPost1 = new EscortPostViewModel{ PeopleId = peoplePosted1.Id};
        var escortPosted1 = await Post<EscortViewModel>(_escortClient, escortPost1);
        var escortPost2 = new EscortPostViewModel{ PeopleId = peoplePosted2.Id};
        var escortPosted2 = await Post<EscortViewModel>(_escortClient, escortPost2);

        // Arrange - Count
        var countBefore = await GetCount(_escortClient);

        // Act - Escort
        var escortPut = new EscortViewModel{ Id = escortPosted1.Id, PeopleId = escortPosted2.PeopleId };
        await Assert.ThrowsAsync<Exception>(() => Put<EscortViewModel>(_escortClient, escortPut));

        // Act - Count
        var countAfter = await GetCount(_escortClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
    }
}
