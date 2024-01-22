using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidPatient_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Hospital
        var hospital = await GetHospital();

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake(peopleId: people.Id, hospitalId: hospital.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var patientPosted = await Post<PatientViewModel>(_patientClient, patientFake);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Act - Query
        var query = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patientPosted.Id));
        var patientQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(patientFake, patientPosted);
        Assert.Equivalent(patientFake, patientQueried);
    }

    [Fact]
    public async Task Post_ValidPatientsWithSameHospital_Ok()
    {
        // Arrange - People
        var people1 = await GetPeople();
        var people2 = await GetPeople();

        // Arrange - Hospital
        var hospital = await GetHospital();

        // Arrange - Patient
        var patientFake1 = new PatientPostViewModelFake(peopleId: people1.Id, hospitalId: hospital.Id).Generate();
        var patientFake2 = new PatientPostViewModelFake(peopleId: people2.Id, hospitalId: hospital.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var patientPosted1 = await Post<PatientViewModel>(_patientClient, patientFake1);
        var patientPosted2 = await Post<PatientViewModel>(_patientClient, patientFake2);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        var query = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(hospitalId: hospital.Id));
        var patientQueried1 = query.FirstOrDefault(x => x.Id == patientPosted1.Id);
        var patientQueried2 = query.FirstOrDefault(x => x.Id == patientPosted2.Id);

        // Assert
        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equivalent(patientFake1, patientPosted1);
        Assert.Equivalent(patientPosted1, patientQueried1);
        Assert.Equivalent(patientFake2, patientPosted2);
        Assert.Equivalent(patientPosted2, patientQueried2);
    }

    [Fact]
    public async Task Post_InvalidPatientWithExistsPeopleId_Conflict()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Hospital
        var hospital = await GetHospital();

        // Arrange - Patient
        var newPatientFake = new PatientPostViewModelFake(peopleId: patient.PeopleId, hospitalId: hospital.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var exception = await Post<AppException>(_patientClient, newPatientFake);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidPatientWithInvalidHospital_NotFound()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Patient
        var patientFake = new PatientPostViewModelFake(peopleId: people.Id, hospitalId: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var exception = await Post<AppException>(_patientClient, patientFake);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task Put_ValidPatient_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();
        var patientToPut = new PatientViewModelFake(patient.Id, patient.PeopleId, patient.HospitalId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var patientPosted = await Put<PatientViewModel>(_patientClient, patientToPut);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Act - Query
        var query = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patientPosted.Id));
        var patientQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(patientToPut, patientPosted);
        Assert.Equivalent(patientPosted, patientQueried);
    }

    [Fact]
    public async Task Put_InvalidPatientWithExistsPeopleId_NotFound()
    {
        // Arrange - Patient
        var patient1 = await GetPatient();
        var patient2 = await GetPatient();

        var patientToPut = new PatientViewModelFake(patient1.Id, patient2.PeopleId, patient1.HospitalId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientClient);

        // Act - Patient
        var exception = await Put<AppException>(_patientClient, patientToPut);

        // Act - Count
        var countAfter = await GetCount(_patientClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task AddTreatmentToPatient_ValidPatientTreatment_Ok(int count)
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Treatment
        var treatments = new List<TreatmentViewModel>();
        for (int i = 0; i < count; i++)
        {
            var treatment = await GetTreatment();
            treatments.Add(treatment);
        }

        // Arrange - PatientTreatment
        var patientTreatmentsFake = new List<PatientTreatmentViewModel>();
        foreach (var treatment in treatments)
        {
            var patientTreatmentFake = new PatientTreatmentViewModelFake(patient.Id, treatment.Id).Generate();
            patientTreatmentsFake.Add(patientTreatmentFake);
        }

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var patientTreatmentsPosted = new List<PatientTreatmentViewModel>();
        foreach (var patientTreatmentFake in patientTreatmentsFake)
        {
            var patientTreatmentPosted = await Post<PatientTreatmentViewModel>(_addTreatmentToPatient, patientTreatmentFake);
            patientTreatmentsPosted.Add(patientTreatmentPosted);
        }

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Act - Query
        var patientTreatmentQueried = await Query<List<PatientTreatmentViewModel>>(_patientTreatmentClient, new PatientTreatmentFilter(patientId: patient.Id));

        // Assert
        Assert.Equal(countBefore + count, countAfter);
        Assert.Equivalent(patientTreatmentsFake, patientTreatmentsPosted);
        Assert.Equivalent(patientTreatmentsPosted, patientTreatmentQueried);
    }

    [Fact]
    public async Task AddTreatmentToPatient_InvalidPatientTreatmentWithExistsPatientIdAndTreatmentId_Conflict()
    {
        // Arrange - PatientTreatment
        var patientTreatment = await GetPatientTreatment();

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var exception = await Post<AppException>(_addTreatmentToPatient, patientTreatment);

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public async Task AddTreatmentToPatient_InvalidPatientTreatmentWithNotExistsPatientId_NotFound()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();

        // Arrange - PatientTreatment
        var patientTreatmentFake = new PatientTreatmentViewModelFake(patientId: 0, treatmentId: treatment.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var exception = await Post<AppException>(_addTreatmentToPatient, patientTreatmentFake);

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task AddTreatmentToPatient_InvalidPatientTreatmentWithNotExistsTreatmentId_NotFound()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - PatientTreatment
        var patientTreatmentFake = new PatientTreatmentViewModelFake(patientId: patient.Id, treatmentId: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var exception = await Post<AppException>(_addTreatmentToPatient, patientTreatmentFake);

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_ValidPatientTreatment_Ok()
    {
        // Arrange - PatientTreatment
        var patientTreatment = await GetPatientTreatment();

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var patientTreatmentDeleted = await DeleteByBody<PatientTreatmentViewModel>(_removeTreatmentFromPatient, patientTreatment);

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Assert
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(patientTreatment, patientTreatmentDeleted);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_InvalidPatientTreatmentWithNotExistsPatientId_NotFound()
    {
        // Arrange - Treatment
        var treatment = await GetTreatment();

        // Arrange - PatientTreatment
        var patientTreatment = new PatientTreatmentViewModelFake(patientId: 0, treatmentId: treatment.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var exception = await DeleteByBody<AppException>(_removeTreatmentFromPatient, patientTreatment);

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_InvalidPatientTreatmentWithNotExistsTreatmentId_NotFound()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - PatientTreatment
        var patientTreatment = new PatientTreatmentViewModelFake(patientId: patient.Id, treatmentId: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var exception = await DeleteByBody<AppException>(_removeTreatmentFromPatient, patientTreatment);

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task RemoveTreatmentFromPatient_InvalidPatientTreatmentWithNotExistsPatientTreatmentId_NotFound()
    {
        // Arrange - PatientTreatment
        var patientTreatment = new PatientTreatmentViewModelFake(patientId: 0, treatmentId: 0).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_patientTreatmentClient);

        // Act - PatientTreatment
        var exception = await DeleteByBody<AppException>(_removeTreatmentFromPatient, patientTreatment);

        // Act - Count
        var countAfter = await GetCount(_patientTreatmentClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
