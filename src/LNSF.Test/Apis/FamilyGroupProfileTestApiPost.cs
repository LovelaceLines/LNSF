using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class FamilyGroupProfileTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task FamilyGroupProfile_Ok()
    {
        var patient = await GetPatient();
        var familyGroupProfileFake = new FamilyGroupProfilePostViewModelFake(patientId: patient.Id).Generate();

        var countBefore = await GetCount(_familyGroupProfileClient);
        var familyGroupProfilePosted = await Post<FamilyGroupProfileViewModel>(_familyGroupProfileClient, familyGroupProfileFake);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(familyGroupProfileFake, familyGroupProfilePosted);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task FamilyGroupProfileWithNonExistsPatientId_NotFound(int patientId)
    {
        var familyGroupProfileFake = new FamilyGroupProfilePostViewModelFake(patientId: patientId).Generate();

        var countBefore = await GetCount(_familyGroupProfileClient);
        var exception = await Post<AppException>(_familyGroupProfileClient, familyGroupProfileFake);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task FamilyGroupProfilesWithSamePatientId_Ok()
    {
        var patient = await GetPatient();
        var familyGroupProfileFake1 = new FamilyGroupProfilePostViewModelFake(patientId: patient.Id).Generate();
        var familyGroupProfileFake2 = new FamilyGroupProfilePostViewModelFake(patientId: patient.Id).Generate();

        var countBefore = await GetCount(_familyGroupProfileClient);
        var familyGroupProfilePosted1 = await Post<FamilyGroupProfileViewModel>(_familyGroupProfileClient, familyGroupProfileFake1);
        var familyGroupProfilePosted2 = await Post<FamilyGroupProfileViewModel>(_familyGroupProfileClient, familyGroupProfileFake2);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equivalent(familyGroupProfileFake1, familyGroupProfilePosted1);
        Assert.Equivalent(familyGroupProfileFake2, familyGroupProfilePosted2);
    }
}
