using LNSF.Api.ViewModels;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Test.Fakers;
using Xunit;
using System.Net;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApi : GlobalClientRequest
{
    [Fact]
    public async Task Post_ValidContact_OK()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Contact
        var contactFake = new EmergencyContactPostViewModelFake(peopleId: people.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act - Contact
        var contactPosted = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Act - Query
        var query = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contactPosted.Id));
        var contactQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore + 1, countAfter);
        Assert.Equivalent(contactFake, contactPosted);
        Assert.Equivalent(contactPosted, contactQueried);
    }

    [Fact]
    public async Task Post_ValidContactsWithSamePeopleId_OK()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Contact
        var contactFake1 = new EmergencyContactPostViewModelFake(peopleId: people.Id).Generate();
        var contactFake2 = new EmergencyContactPostViewModelFake(peopleId: people.Id).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act
        var contactPosted1 = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake1);
        var contactPosted2 = await Post<EmergencyContactViewModel>(_emergencyContactClient, contactFake2);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Act - Query
        var query = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(peopleId: people.Id));
        var contactQueried1 = query.FirstOrDefault(c => c.Id == contactPosted1.Id);
        var contactQueried2 = query.FirstOrDefault(c => c.Id == contactPosted2.Id);

        // Assert
        Assert.Equal(countBefore + 2, countAfter);
        Assert.Equivalent(contactFake1, contactPosted1);
        Assert.Equivalent(contactFake2, contactPosted2);
        Assert.Equivalent(contactPosted1, contactQueried1);
        Assert.Equivalent(contactPosted2, contactQueried2);
    }

    [Fact]
    public async Task Post_InvalidContact_BadRequest()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Contact
        var contactFakeWithInvalidPeopleId = new EmergencyContactPostViewModelFake(peopleId: -1).Generate();
        var contactFakeWithoutName = new EmergencyContactPostViewModelFake(peopleId: people.Id, name: "").Generate();
        var contactFakeWithoutPhone = new EmergencyContactPostViewModelFake(peopleId: people.Id, phone: "").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act - Contact
        var exceptionWithInvalidPeopleId = await Post<AppException>(_emergencyContactClient, contactFakeWithInvalidPeopleId);
        var exceptionWithoutName = await Post<AppException>(_emergencyContactClient, contactFakeWithoutName);
        var exceptionWithoutPhone = await Post<AppException>(_emergencyContactClient, contactFakeWithoutPhone);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exceptionWithInvalidPeopleId.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutPhone.StatusCode);
    }
    
    [Fact]
    public async Task Put_ValidContact_OK()
    {   
        // Arrange - Contact
        var contact = await GetEmergencyContact();
        var contactToPut = new EmergencyContactViewModelFake(contact.Id, contact.PeopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act - Contact
        var contactPuted = await Put<EmergencyContactViewModel>(_emergencyContactClient, contactToPut);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Act - Query
        var query = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id));
        var contactQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equivalent(contactToPut, contactPuted);
        Assert.Equivalent(contactPuted, contactQueried);
    }

    [Fact]
    public async Task Put_InvalidContact_BadRequest()
    {
        // Arrange - Contact
        var contact = await GetEmergencyContact();

        var contactWithInvalidPeopleId = new EmergencyContactViewModelFake(id: contact.Id, peopleId: -1).Generate();
        var contactWithoutName = new EmergencyContactViewModelFake(id: contact.Id, peopleId: contact.PeopleId, name: "").Generate();
        var contactWithoutPhone = new EmergencyContactViewModelFake(id: contact.Id, peopleId: contact.PeopleId, phone: "").Generate();

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act - Contact
        var exceptionWithInvalidPeopleId = await Put<AppException>(_emergencyContactClient, contactWithInvalidPeopleId);
        var exceptionWithoutName = await Put<AppException>(_emergencyContactClient, contactWithoutName);
        var exceptionWithoutPhone = await Put<AppException>(_emergencyContactClient, contactWithoutPhone);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Act - Query
        var query = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id));
        var contactQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exceptionWithInvalidPeopleId.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutPhone.StatusCode);
        Assert.Equivalent(contact, contactQueried);
    }

    [Fact]
    public async Task Put_InvalidContactWithOtherPeopleId_NotFound()
    {
        // Arrange - Contact
        var contact1 = await GetEmergencyContact();
        var contact2 = await GetEmergencyContact();

        var contactToPut = new EmergencyContactViewModelFake(id: contact1.Id, peopleId: contact2.PeopleId).Generate();

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);

        // Act - Contact
        var exception = await Put<AppException>(_emergencyContactClient, contactToPut);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Act - Query
        var query = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter { Id = contact1.Id });
        var contactQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
        Assert.Equivalent(contact1, contactQueried);
    }

    [Fact]
    public async Task Delete_ValidContact_OK()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Contact
        var contact = await GetEmergencyContact(peopleId: people.Id);

        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);
 
        // Act - Contact
        var contactDeleted = await Delete<EmergencyContactViewModel>(_emergencyContactClient, contact.Id);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Act - Query
        var query = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id));
        var contactQueried = query.FirstOrDefault();

        // Assert
        Assert.Equal(countBefore - 1, countAfter);
        Assert.Equivalent(contact, contactDeleted);
        Assert.Null(contactQueried);
    }

    [Fact]
    public async Task Delete_InvalidContact_NotFound()
    {
        // Arrange - Count
        var countBefore = await GetCount(_emergencyContactClient);
 
        // Act - Contact
        var exception = await Delete<AppException>(_emergencyContactClient, -1);

        // Act - Count
        var countAfter = await GetCount(_emergencyContactClient);

        // Assert
        Assert.Equal(countBefore, countAfter);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

}