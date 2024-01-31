using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class ServiceRecordTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task ServiceRecord_Ok()
    {
        var patient = await GetPatient();
        var serviceRecordFake = new ServiceRecordPostViewModelFake(patientId: patient.Id).Generate();

        var countBefore = await GetCount(_serviceRecord);
        var serviceRecord = await Post<ServiceRecordViewModel>(_serviceRecord, serviceRecordFake);
        var countAfter = await GetCount(_serviceRecord);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(serviceRecordFake, serviceRecord);
    }

    [Fact]
    public async Task ServiceRecordWithNonExistsPatientId_NotFound()
    {
        var serviceRecordFake = new ServiceRecordPostViewModelFake(patientId: 0).Generate();

        var countBefore = await GetCount(_serviceRecord);
        var exception = await Post<AppException>(_serviceRecord, serviceRecordFake);
        var countAfter = await GetCount(_serviceRecord);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public async Task ServiceRecordWithUsedPatientId_Conflict()
    {
        var serviceRecord = await GetServiceRecord();
        var serviceRecordFake = new ServiceRecordPostViewModelFake(patientId: serviceRecord.PatientId).Generate();

        var countBefore = await GetCount(_serviceRecord);
        var exception = await Post<AppException>(_serviceRecord, serviceRecordFake);
        var countAfter = await GetCount(_serviceRecord);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
