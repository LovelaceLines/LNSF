using System.Net;
using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using Xunit;

namespace LNSF.Test.Apis;

public class FamilyGroupProfileTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Put_FamilyGroupProfile_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();
        var familyGroupProfileFake = new FamilyGroupProfileViewModelFake(id: familyGroupProfile.Id, patientId: familyGroupProfile.PatientId).Generate();

        var countBefore = await GetCount(_familyGroupProfileClient);
        var familyGroupProfilePuted = await Put<FamilyGroupProfileViewModel>(_familyGroupProfileClient, familyGroupProfileFake);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(familyGroupProfileFake, familyGroupProfilePuted);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_FamilyGroupProfileWithNonExistsId_NotFoun(int id)
    {
        var patient = await GetPatient();
        var familyGroupProfileFake = new FamilyGroupProfileViewModelFake(id: id, patientId: patient.Id).Generate();

        var countBefore = await GetCount(_familyGroupProfileClient);
        var exception = await Put<AppException>(_familyGroupProfileClient, familyGroupProfileFake);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Put_FamilyGroupProfileWithNonExistsPatientId_NotFoun(int patientId)
    {
        var familyGroupProfile = await GetFamilyGroupProfile();
        var familyGroupProfileFake = new FamilyGroupProfileViewModelFake(id: familyGroupProfile.Id, patientId: patientId).Generate();

        var countBefore = await GetCount(_familyGroupProfileClient);
        var exception = await Put<AppException>(_familyGroupProfileClient, familyGroupProfileFake);
        var countAfter = await GetCount(_familyGroupProfileClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
