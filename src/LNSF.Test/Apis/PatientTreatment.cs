using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTreatment : GlobalClientRequest
{
    [Fact]
    public async Task AddTreatmentToPatient_PatientTreatment_Ok()
    {
        var patient = await GetPatient();
        var treatment1 = await GetTreatment();
        var treatment2 = await GetTreatment();
        var patientTreatmentFake1 = new PatientTreatmentViewModelFake(patient.Id, treatment1.Id).Generate();
        var patientTreatmentFake2 = new PatientTreatmentViewModelFake(patient.Id, treatment2.Id).Generate();

        var countBefore = await GetCount(_patientTreatmentClient);
        var patientTreatmentPosted1 = await Post<PatientTreatmentViewModel>(_addTreatmentToPatient, patientTreatmentFake1);
        var patientTreatmentPosted2 = await Post<PatientTreatmentViewModel>(_addTreatmentToPatient, patientTreatmentFake2);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equivalent(patientTreatmentFake1, patientTreatmentPosted1);
        Assert.Equivalent(patientTreatmentFake2, patientTreatmentPosted2);
    }

    [Fact]
    public async Task AddTreatmentToPatient_PatientTreatmentWithExistsPatientIdAndTreatmentId_Conflict()
    {
        var patientTreatment = await GetPatientTreatment();

        var countBefore = await GetCount(_patientTreatmentClient);
        var exception = await Post<AppException>(_addTreatmentToPatient, patientTreatment);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task AddTreatmentToPatient_PatientTreatmentWithNotExistsPatientId_NotFound()
    {
        var treatment = await GetTreatment();
        var patientTreatmentFake = new PatientTreatmentViewModelFake(patientId: 0, treatmentId: treatment.Id).Generate();

        var countBefore = await GetCount(_patientTreatmentClient);
        var exception = await Post<AppException>(_addTreatmentToPatient, patientTreatmentFake);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddTreatmentToPatient_PatientTreatmentWithNotExistsTreatmentId_NotFound()
    {
        var patient = await GetPatient();
        var patientTreatmentFake = new PatientTreatmentViewModelFake(patientId: patient.Id, treatmentId: 0).Generate();

        var countBefore = await GetCount(_patientTreatmentClient);
        var exception = await Post<AppException>(_addTreatmentToPatient, patientTreatmentFake);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_PatientTreatment_Ok()
    {
        var patientTreatment = await GetPatientTreatment();

        var countBefore = await GetCount(_patientTreatmentClient);
        var patientTreatmentDeleted = await DeleteByBody<PatientTreatmentViewModel>(_removeTreatmentFromPatient, patientTreatment);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(patientTreatment, patientTreatmentDeleted);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_PatientTreatmentWithNotExistsPatientId_NotFound()
    {
        var treatment = await GetTreatment();
        var patientTreatment = new PatientTreatmentViewModelFake(patientId: 0, treatmentId: treatment.Id).Generate();

        var countBefore = await GetCount(_patientTreatmentClient);
        var exception = await DeleteByBody<AppException>(_removeTreatmentFromPatient, patientTreatment);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_PatientTreatmentWithNotExistsTreatmentId_NotFound()
    {
        var patient = await GetPatient();
        var patientTreatment = new PatientTreatmentViewModelFake(patientId: patient.Id, treatmentId: 0).Generate();

        var countBefore = await GetCount(_patientTreatmentClient);
        var exception = await DeleteByBody<AppException>(_removeTreatmentFromPatient, patientTreatment);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_PatientTreatmentWithNotExistsPatientIdAndTreatmentId_NotFound()
    {
        var patientTreatment = new PatientTreatmentViewModelFake(patientId: 0, treatmentId: 0).Generate();

        var countBefore = await GetCount(_patientTreatmentClient);
        var exception = await DeleteByBody<AppException>(_removeTreatmentFromPatient, patientTreatment);
        var countAfter = await GetCount(_patientTreatmentClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
