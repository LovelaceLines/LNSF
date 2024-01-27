using System.Net;
using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using Xunit;

namespace LNSF.Test.Apis;

public class ServiceRecordTestApiDelete : GlobalClientRequest
{
    [Fact]
    public async Task Delete_ServiceRecord_Ok()
    {
        var serviceRecord = await GetServiceRecord();

        var countBefore = await GetCount(_serviceRecord);
        var serviceRecordDeleted = await Delete<ServiceRecordViewModel>(_serviceRecord, serviceRecord.Id);
        var countAfter = await GetCount(_serviceRecord);

        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(serviceRecord, serviceRecordDeleted);
    }

    [Fact]
    public async Task Delete_ServiceRecordWithNonExistsId_NotFound()
    {
        var countBefore = await GetCount(_serviceRecord);
        var exception = await Delete<AppException>(_serviceRecord, 0);
        var countAfter = await GetCount(_serviceRecord);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}
