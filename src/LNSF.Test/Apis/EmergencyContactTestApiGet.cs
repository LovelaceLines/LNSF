using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task Get_ValidContactId_OK()
    {
        // Arrange - Contact
        var contact = await GetEmergencyContact();

        // Act - Contact
        var queryId = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id));
        var contactQueriedId = queryId.First();

        Assert.Equivalent(contact, contactQueriedId);
    }

    [Fact]
    public async Task Get_ValidContactPeopleId_OK()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Contact
        var contact = await GetEmergencyContact(peopleId: people.Id);

        // Act - Contact
        var queryPeopleId = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, peopleId: people.Id));
        var contactQueriedPeopleId = queryPeopleId.First();

        Assert.Equivalent(contact, contactQueriedPeopleId);
    }

    [Fact]
    public async Task Get_ValidContactName_OK()
    {
        // Arrange - Contact
        var contact = await GetEmergencyContact();

        // Act - Contact
        var queryName = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, name: contact.Name));
        var contactQueriedName = queryName.First();

        Assert.Equivalent(contact, contactQueriedName);
    }

    [Fact]
    public async Task Get_ValidContactPhone_OK()
    {
        // Arrange - Contact
        var contact = await GetEmergencyContact();

        // Act - Contact
        var queryPhone = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, phone: contact.Phone));
        var contactQueriedPhone = queryPhone.First();

        Assert.Equivalent(contact, contactQueriedPhone);
    }

    [Fact]
    public async Task Get_ValidContactGetPeople_OK()
    {
        // Arrange - People
        var people = await GetPeople();

        // Arrange - Contact
        var contact = await GetEmergencyContact(peopleId: people.Id);

        // Act - Contact
        var queryGetPeople = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, getPeople: true));
        var contactQueriedGetPeople = queryGetPeople.First();

        Assert.Equivalent(people, contactQueriedGetPeople.People);
    }

    [Fact]
    public async Task Get_ValidContactGlobalFilter_OK()
    {
        // Arrange - Contact
        var contact = await GetEmergencyContact();

        // Act - Contact
        var queryGlobalFilterName = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, globalFilter: contact.Name));
        var contactNameQueriedGlobalFilter = queryGlobalFilterName.First();
        var queryGlobalFilterPhone = await Query<List<EmergencyContactViewModel>>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, globalFilter: contact.Phone));
        var contactPhoneQueriedGlobalFilter = queryGlobalFilterPhone.First();

        Assert.Equivalent(contact, contactNameQueriedGlobalFilter);
        Assert.Equivalent(contact, contactPhoneQueriedGlobalFilter);
    }

}
