using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class ServiceRecordTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task ServiceRecord_Ok()
    {
        var serviceRecord = await GetServiceRecord();
        var serviceRecordFake = new ServiceRecordViewModelFake(id: serviceRecord.Id, patientId: serviceRecord.PatientId).Generate();

        var serviceRecordPuted = await Put<ServiceRecordViewModel>(_serviceRecord, serviceRecordFake);

        Assert.Equivalent(serviceRecordFake, serviceRecordPuted);
    }

    [Fact]
    public async Task ServiceRecordWithUsedPatientId_Conflict()
    {
        var serviceRecord1 = await GetServiceRecord();
        var serviceRecord2 = await GetServiceRecord();
        var serviceRecordFake = new ServiceRecordViewModelFake(id: serviceRecord1.Id, patientId: serviceRecord2.PatientId).Generate();

        var countBefore = await GetCount(_serviceRecord);
        var exception = await Put<AppException>(_serviceRecord, serviceRecordFake);
        var countAfter = await GetCount(_serviceRecord);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }
}
