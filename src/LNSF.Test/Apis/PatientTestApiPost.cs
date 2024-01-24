using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task Post_Patient_Ok()
    {
        var people = await GetPeople();
        var hospital = await GetHospital();
        var patientFake = new PatientPostViewModelFake(peopleId: people.Id, hospitalId: hospital.Id).Generate();

        var countBefore = await GetCount(_patientClient);
        var patientPosted = await Post<PatientViewModel>(_patientClient, patientFake);
        var countAfter = await GetCount(_patientClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(patientFake, patientPosted);
    }

    [Fact]
    public async Task Post_PatientsWithSameHospital_Ok()
    {
        var hospital = await GetHospital();
        var people1 = await GetPeople();
        var people2 = await GetPeople();
        var patientFake1 = new PatientPostViewModelFake(peopleId: people1.Id, hospitalId: hospital.Id).Generate();
        var patientFake2 = new PatientPostViewModelFake(peopleId: people2.Id, hospitalId: hospital.Id).Generate();

        var countBefore = await GetCount(_patientClient);
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);
        var patientPosted2 = await Post<PatientViewModel>(_patientClient, patientFake2);
        var countAfter = await GetCount(_patientClient);

        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equivalent(patientFake1, patientPosted1);
        Assert.Equivalent(patientFake2, patientPosted2);
    }

    [Fact]
    public async Task Post_PatientWithExistsPeopleId_Conflict()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var patientFake = new PatientPostViewModelFake(peopleId: patient.PeopleId, hospitalId: patient.HospitalId).Generate();

        var countBefore = await GetCount(_patientClient);
        var exception = await Post<AppException>(_patientClient, patientFake);
        var countAfter = await GetCount(_patientClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task Post_PatientWithInvalidHospital_NotFound()
    {
        var people = await GetPeople();
        var patientFake = new PatientPostViewModelFake(peopleId: people.Id, hospitalId: 0).Generate();

        var countBefore = await GetCount(_patientClient);
        var exception = await Post<AppException>(_patientClient, patientFake);
        var countAfter = await GetCount(_patientClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
