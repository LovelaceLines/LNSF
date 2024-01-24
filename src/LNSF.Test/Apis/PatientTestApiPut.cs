using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Put_ValidPatient_Ok()
    {
        var patient = await GetPatient();
        var patientFake = new PatientViewModelFake(patient.Id, patient.PeopleId, patient.HospitalId).Generate();

        var countBefore = await GetCount(_patientClient);
        var patientPosted = await Put<PatientViewModel>(_patientClient, patientFake);
        var countAfter = await GetCount(_patientClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(patientFake, patientPosted);
    }

    [Fact]
    public async Task Put_PatientWithNonExistsPeopleId_NotFound()
    {
        var patient1 = await GetPatient();
        var patient2 = await GetPatient();
        var patientFake = new PatientViewModelFake(patient1.Id, patient2.PeopleId, patient1.HospitalId).Generate();

        var countBefore = await GetCount(_patientClient);
        var exception = await Put<AppException>(_patientClient, patientFake);
        var countAfter = await GetCount(_patientClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
