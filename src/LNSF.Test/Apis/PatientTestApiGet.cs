using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class PatientTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryPatient_Ok()
    {
        var patient = await GetPatient();

        var patientQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id));

        Assert.Equivalent(patient.Id, patientQueried.Id);
        Assert.Equivalent(patient.SocioeconomicRecord, patientQueried.SocioeconomicRecord);
        Assert.Equivalent(patient.Term, patientQueried.Term);
        Assert.Equivalent(patient.PeopleId, patientQueried.PeopleId);
        Assert.Equivalent(patient.HospitalId, patientQueried.HospitalId);
    }

    [Fact]
    public async Task QueryPatientSocioEconomicRecord_Ok()
    {
        var patient = await GetPatient();

        var patientSocioEconomicRecordQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, socioEconomicRecord: patient.SocioeconomicRecord));

        Assert.Equivalent(patient.SocioeconomicRecord, patientSocioEconomicRecordQueried.SocioeconomicRecord);
    }

    [Fact]
    public async Task QueryPatientTerm_Ok()
    {
        var patient = await GetPatient();

        var patientTermQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, term: patient.Term));

        Assert.Equivalent(patient.Term, patientTermQueried.Term);
    }

    [Fact]
    public async Task QueryPatientPeopleId_Ok()
    {
        var patient = await GetPatient();

        var patientPeopleIdQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, peopleId: patient.PeopleId));

        Assert.Equivalent(patient.PeopleId, patientPeopleIdQueried.PeopleId);
    }

    [Fact]
    public async Task QueryPatientHospitalId_Ok()
    {
        var patient = await GetPatient();

        var patientHospitalIdQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, hospitalId: patient.HospitalId));

        Assert.Equivalent(patient.HospitalId, patientHospitalIdQueried.HospitalId);
    }

    [Fact]
    public async Task QueryPatientActive_Ok()
    {
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        var patientActiveQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, active: true));

        Assert.Equivalent(patient, patientActiveQueried);
    }

    [Fact]
    public async Task QueryPatientInactive_Ok()
    {
        var patient = await GetPatient();
        var hosting = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-5));

        var patientInactiveQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, active: false));

        Assert.Equivalent(patient, patientInactiveQueried);
    }

    [Fact]
    public async Task QueryPatientIsVeteran_Ok()
    {
        var patient = await GetPatient();
        var hosting1 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-10), checkOut: DateTime.Now.AddDays(-5));
        var hosting2 = await GetHosting(patientId: patient.Id, checkIn: DateTime.Now.AddDays(-1), checkOut: DateTime.Now.AddDays(1));

        var patientIsVeteranQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, isVeteran: true));

        Assert.Equivalent(patient, patientIsVeteranQueried);
    }

    [Fact]
    public async Task QueryPatientGetPeople_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);

        var patientGetPeopleQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, getPeople: true));

        Assert.Equivalent(people, patientGetPeopleQueried!.People);
    }

    [Fact]
    public async Task QueryPatientGetHospital_Ok()
    {

        var hospital = await GetHospital();
        var patient = await GetPatient(hospitalId: hospital.Id);

        var patientGetHospitalQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, getHospital: true));

        Assert.Equivalent(hospital, patientGetHospitalQueried!.Hospital);
    }

    [Fact]
    public async Task QueryPatientGetTreatments_Ok()
    {
        var treatment1 = await GetTreatment();
        var treatment2 = await GetTreatment();
        var patient = await GetPatient();
        var patientTreatment1 = await GetPatientTreatment(patientId: patient.Id, treatmentId: treatment1.Id);
        var patientTreatment2 = await GetPatientTreatment(patientId: patient.Id, treatmentId: treatment2.Id);

        var patientGetTreatmentsQueried = await QuerySingle<PatientViewModel>(_patientClient, new PatientFilter(id: patient.Id, getTreatments: true));

        Assert.Equivalent(new List<TreatmentViewModel> { treatment1, treatment2 }, patientGetTreatmentsQueried!.Treatments!);
    }
}
