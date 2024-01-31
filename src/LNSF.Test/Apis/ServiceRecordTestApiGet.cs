using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class ServiceRecordTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryServiceRecord_Ok()
    {
        var serviceRecord = await GetServiceRecord();

        var serviceRecordQueried = await QueryFirst<ServiceRecordViewModel>(_serviceRecord, new ServiceRecordFilter(id: serviceRecord.Id));

        Assert.Equivalent(serviceRecord, serviceRecordQueried);
    }

    [Fact]
    public async Task QueryServiceRecordPatientId_Ok()
    {
        var serviceRecord = await GetServiceRecord();

        var serviceRecordQueried = await QueryFirst<ServiceRecordViewModel>(_serviceRecord, new ServiceRecordFilter(id: serviceRecord.Id, patientId: serviceRecord.PatientId));

        Assert.Equivalent(serviceRecord, serviceRecordQueried);
    }

    [Fact]
    public async Task QueryServiceRecordGetPatientId_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        patient.People = people;
        var serviceRecord = await GetServiceRecord(patientId: patient.Id);
        serviceRecord.Patient = patient;

        var serviceRecordGetPatientQueried = await QueryFirst<ServiceRecordViewModel>(_serviceRecord, new ServiceRecordFilter(id: serviceRecord.Id, getPatient: true));

        Assert.Equivalent(serviceRecord, serviceRecordGetPatientQueried);
    }

    [Fact]
    public async Task QueryServiceRecordGlobalFilter_Ok()
    {
        var people = await GetPeople();
        var patient = await GetPatient(peopleId: people.Id);
        var serviceRecord = await GetServiceRecord(patientId: patient.Id);

        var serviceRecordPeopleNameQueried = await QueryFirst<ServiceRecordViewModel>(_serviceRecord, new ServiceRecordFilter(id: serviceRecord.Id, globalFilter: people.Name));

        Assert.Equivalent(serviceRecord, serviceRecordPeopleNameQueried);
    }
}
