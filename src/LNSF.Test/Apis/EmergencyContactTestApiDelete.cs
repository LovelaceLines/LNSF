using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApiDelete : GlobalClientRequest
{
    [Fact]
    public async Task Contact_OK()
    {
        var contact = await GetEmergencyContact();

        var countBefore = await GetCount(_emergencyContactClient);
        var contactDeleted = await Delete<EmergencyContactViewModel>(_emergencyContactClient, contact.Id);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(contact, contactDeleted);
    }

    [Fact]
    public async Task ContactNonExists_NotFound()
    {
        var countBefore = await GetCount(_emergencyContactClient);
        var exception = await Delete<AppException>(_emergencyContactClient, -1);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}