using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApiPut : GlobalClientRequest
{
    [Fact]
    public async Task Put_Contact_OK()
    {
        var contact = await GetEmergencyContact();
        var contactFake = new EmergencyContactViewModelFake(contact.Id, contact.PeopleId).Generate();

        var countBefore = await GetCount(_emergencyContactClient);
        var contactPuted = await Put<EmergencyContactViewModel>(_emergencyContactClient, contactFake);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(contactFake, contactPuted);
    }

    [Fact]
    public async Task Put_Contact_BadRequest()
    {
        var contact = await GetEmergencyContact();
        var contactFakeWithInvalidPeopleId = new EmergencyContactViewModelFake(id: contact.Id, peopleId: -1).Generate();
        var contactFakeWithoutName = new EmergencyContactViewModelFake(id: contact.Id, peopleId: contact.PeopleId, name: "").Generate();
        var contactFakeWithoutPhone = new EmergencyContactViewModelFake(id: contact.Id, peopleId: contact.PeopleId, phone: "").Generate();

        var countBefore = await GetCount(_emergencyContactClient);
        var exceptionWithInvalidPeopleId = await Put<AppException>(_emergencyContactClient, contactFakeWithInvalidPeopleId);
        var exceptionWithoutName = await Put<AppException>(_emergencyContactClient, contactFakeWithoutName);
        var exceptionWithoutPhone = await Put<AppException>(_emergencyContactClient, contactFakeWithoutPhone);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exceptionWithInvalidPeopleId.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutPhone.StatusCode);
    }

    [Fact]
    public async Task Put_ContactWithOtherPeopleId_NotFound()
    {
        var contact1 = await GetEmergencyContact();
        var contact2 = await GetEmergencyContact();
        var contactFakeWithOtherPeopleId = new EmergencyContactViewModelFake(id: contact1.Id, peopleId: contact2.PeopleId).Generate();

        var countBefore = await GetCount(_emergencyContactClient);
        var exception = await Put<AppException>(_emergencyContactClient, contactFakeWithOtherPeopleId);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }
}