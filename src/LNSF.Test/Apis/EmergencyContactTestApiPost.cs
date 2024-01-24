using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Test.Fakers;
using System.Net;
using Xunit;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApiPost : GlobalClientRequest
{
    [Fact]
    public async Task Post_Contact_OK()
    {
        var people = await GetPeople();
        var contactFake = new EmergencyContactPostViewModelFake(peopleId: people.Id).Generate();

        var countBefore = await GetCount(_emergencyContactClient);
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(contactFake, contactPosted);
    }

    [Fact]
    public async Task Post_ContactsWithSamePeopleId_OK()
    {
        var people = await GetPeople();
        var contactFake1 = new EmergencyContactPostViewModelFake(peopleId: people.Id).Generate();
        var contactFake2 = new EmergencyContactPostViewModelFake(peopleId: people.Id).Generate();

        var countBefore = await GetCount(_emergencyContactClient);
        var contactPosted1 = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake1);
        var contactPosted2 = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake2);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equivalent(contactFake1, contactPosted1);
        Assert.Equivalent(contactFake2, contactPosted2);
    }

    [Fact]
    public async Task Post_Contact_BadRequest()
    {
        var people = await GetPeople();
        var contactFakeWithInvalidPeopleId = new EmergencyContactPostViewModelFake(peopleId: -1).Generate();
        var contactFakeWithoutName = new EmergencyContactPostViewModelFake(peopleId: people.Id, name: "").Generate();
        var contactFakeWithoutPhone = new EmergencyContactPostViewModelFake(peopleId: people.Id, phone: "").Generate();

        var countBefore = await GetCount(_emergencyContactClient);
        var exceptionWithInvalidPeopleId = await Post<AppException>(_emergencyContactClient, contactFakeWithInvalidPeopleId);
        var exceptionWithoutName = await Post<AppException>(_emergencyContactClient, contactFakeWithoutName);
        var exceptionWithoutPhone = await Post<AppException>(_emergencyContactClient, contactFakeWithoutPhone);
        var countAfter = await GetCount(_emergencyContactClient);

        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exceptionWithInvalidPeopleId.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutPhone.StatusCode);
    }
}