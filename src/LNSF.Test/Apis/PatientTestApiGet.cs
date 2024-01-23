using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidPatientId_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Act - Patient
        var queryId = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id));
        var patientIdQueried = queryId.First();

        Assert.Equivalent(patient, patientIdQueried);
    }

    [Fact]
    public async Task Get_ValidPeopleId_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Act - Patient
        var queryPeopleId = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, peopleId: patient.PeopleId));
        var patientPeopleIdQueried = queryPeopleId.First();

        Assert.Equivalent(patient, patientPeopleIdQueried);
    }

    [Fact]
    public async Task Get_ValidHospitalId_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Act - Patient
        var queryHospitalId = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, hospitalId: patient.HospitalId));
        var patientHospitalIdQueried = queryHospitalId.First();

        Assert.Equivalent(patient, patientHospitalIdQueried);
    }

    [Fact]
    public async Task Get_ValidSocioEconomicRecord_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Act - Patient
        var querySocioEconomicRecord = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, socioEconomicRecord: patient.SocioeconomicRecord));
        var patientSocioEconomicRecordQueried = querySocioEconomicRecord.First();

        Assert.Equivalent(patient, patientSocioEconomicRecordQueried);
    }

    [Fact]
    public async Task Get_ValidTerm_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Act - Patient
        var queryTerm = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, term: patient.Term));
        var patientTermQueried = queryTerm.First();

        Assert.Equivalent(patient, patientTermQueried);
    }

    [Fact]
    public async Task Get_ValidTreatmentId_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - PatientTreatment
        var patientTreatment = await GetPatientTreatment(patientId: patient.Id);

        // Act - Patient
        var queryTreatmentId = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, treatmentId: patientTreatment.TreatmentId));
        var patientTreatmentIdQueried = queryTreatmentId.First();

        Assert.Equivalent(patient, patientTreatmentIdQueried);
    }

    [Fact]
    public async Task Get_ValidActive_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        // Act - Patient
        var queryActive = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, active: true));
        var patientActiveQueried = queryActive.First();

        Assert.Equivalent(patient, patientActiveQueried);
    }

    [Fact]
    public async Task Get_ValidIsVeteran_Ok()
    {
        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - Hosting
        var hosting1 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-5));
        var hosting2 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        // Act - Patient
        var queryIsVeteran = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, isVeteran: true));
        var patientIsVeteranQueried = queryIsVeteran.First();

        Assert.Equivalent(patient, patientIsVeteranQueried);
    }

    [Fact]
    public async Task Get_ValidGetPeople_Ok()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Patient
        var patient = await GetPatient(peopleId: people.Id);

        // Act - Patient
        var queryGetPeople = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, getPeople: true));
        var patientGetPeopleQueried = queryGetPeople.First();

        Assert.Equivalent(people, patientGetPeopleQueried!.People);
    }

    [Fact]
    public async Task Get_ValidGetHospital_Ok()
    {
        // Arrange - Hospital
        var hospital = await GetHospital();

        // Arrange - Patient
        var patient = await GetPatient(hospitalId: hospital.Id);

        // Act - Patient
        var queryGetHospital = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, getHospital: true));
        var patientGetHospitalQueried = queryGetHospital.First();

        Assert.Equivalent(hospital, patientGetHospitalQueried!.Hospital);
    }

    [Fact]
    public async Task Get_ValidGetTreatments_Ok()
    {
        // Arrange - Treatment
        var treatment1 = await GetTreatment();
        var treatment2 = await GetTreatment();

        var treatments = new List<TreatmentViewModel> { treatment1, treatment2 };

        // Arrange - Patient
        var patient = await GetPatient();

        // Arrange - PatientTreatment
        var patientTreatment1 = await GetPatientTreatment(patientId: patient.Id, treatmentId: treatment1.Id);
        var patientTreatment2 = await GetPatientTreatment(patientId: patient.Id, treatmentId: treatment2.Id);

        // Act - Patient
        var queryGetTreatments = await Query<List<PatientViewModel>>(_patientClient, new PatientFilter(id: patient.Id, getTreatments: true));
        var patientGetTreatmentsQueried = queryGetTreatments.First();

        Assert.Equivalent(treatments, patientGetTreatmentsQueried!.Treatments!);
    }
}
