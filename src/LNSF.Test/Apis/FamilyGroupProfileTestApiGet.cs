using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class FamilyGroupProfileTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryFamilyGroupProfile_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();

        var familyGroupProfileQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id));

        Assert.Equal(familyGroupProfile.Id, familyGroupProfileQueried.Id);
        Assert.Equal(familyGroupProfile.Name, familyGroupProfileQueried.Name);
        Assert.Equal(familyGroupProfile.Kinship, familyGroupProfileQueried.Kinship);
        Assert.Equal(familyGroupProfile.Age, familyGroupProfileQueried.Age);
        Assert.Equal(familyGroupProfile.Profession, familyGroupProfileQueried.Profession);
        Assert.Equal(familyGroupProfile.Income, familyGroupProfileQueried.Income);
    }

    [Fact]
    public async Task QueryFamilyGroupProfileName_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();

        var familyGroupProfileQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, name: familyGroupProfile.Name));

        Assert.Equal(familyGroupProfile.Name, familyGroupProfileQueried.Name);
    }

    [Fact]
    public async Task QueryFamilyGroupProfileKinship_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();

        var familyGroupProfileQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, kinship: familyGroupProfile.Kinship));

        Assert.Equal(familyGroupProfile.Kinship, familyGroupProfileQueried.Kinship);
    }

    [Fact]
    public async Task QueryFamilyGroupProfileAge_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();

        var familyGroupProfileQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, age: familyGroupProfile.Age));

        Assert.Equal(familyGroupProfile.Age, familyGroupProfileQueried.Age);
    }

    [Fact]
    public async Task QueryFamilyGroupProfileProfession_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();

        var familyGroupProfileQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, profession: familyGroupProfile.Profession));

        Assert.Equal(familyGroupProfile.Profession, familyGroupProfileQueried.Profession);
    }

    [Fact]
    public async Task QueryFamilyGroupProfileIncome_Ok()
    {
        var familyGroupProfile = await GetFamilyGroupProfile();

        var familyGroupProfileQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, income: familyGroupProfile.Income));

        Assert.Equal(familyGroupProfile.Income, familyGroupProfileQueried.Income);
    }

    [Fact]
    public async Task QueryFamilyGroupProfileGlobalFilter_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var familyGroupProfile = await GetFamilyGroupProfile(patientId: patient.Id);
        var familyGroupProfileQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id));

        var familyGroupProfileNameQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, globalFilter: familyGroupProfile.Name));
        var familyGroupProfileKinshipQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, globalFilter: familyGroupProfile.Kinship));
        var familyGroupProfileProfessionQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, globalFilter: familyGroupProfile.Profession));
        var familyGroupProfilePatientNameQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, globalFilter: people.Name));

        Assert.Equal(familyGroupProfileQueried.Id, familyGroupProfileNameQueried.Id);
        Assert.Equal(familyGroupProfileQueried.Id, familyGroupProfileKinshipQueried.Id);
        Assert.Equal(familyGroupProfileQueried.Id, familyGroupProfileProfessionQueried.Id);
        Assert.Equal(familyGroupProfileQueried.Id, familyGroupProfilePatientNameQueried.Id);
    }

    [Fact]
    public async Task QueryFamilyGroupProfileGetPatient_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        patient.People = people;
        var familyGroupProfile = await GetFamilyGroupProfile(patientId: patient.Id);
        familyGroupProfile.Patient = patient;

        var familyGroupProfileGetPatientQueried = await QuerySingle<FamilyGroupProfileViewModel>(_familyGroupProfileClient, new FamilyGroupProfileFilter(id: familyGroupProfile.Id, getPatient: true));

        Assert.Equivalent(familyGroupProfile, familyGroupProfileGetPatientQueried);
        Assert.Equivalent(patient, familyGroupProfileGetPatientQueried.Patient);
        Assert.Equivalent(people, familyGroupProfileGetPatientQueried.Patient!.People);
    }
}
