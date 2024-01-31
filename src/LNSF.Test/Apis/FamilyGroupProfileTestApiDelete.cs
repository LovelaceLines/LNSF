using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class FamilyGroupProfileTestApiDelete : GlobalClientRequest
{
    [Fact]
    public async Task FamilyGroupProfile_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();

        var countBefore = await GetCount(_familyGroupProfileClient);
        var familyGroupProfileDeleted = await Delete<FamilyGroupProfileViewModel>(_familyGroupProfileClient, familyGroupProfile.Id);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(familyGroupProfile, familyGroupProfileDeleted);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task FamilyGroupProfileWithNonExistsId_NotFoun(int id)
    {
        var countBefore = await GetCount(_familyGroupProfileClient);
        var exception = await Delete<AppException>(_familyGroupProfileClient, id);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
