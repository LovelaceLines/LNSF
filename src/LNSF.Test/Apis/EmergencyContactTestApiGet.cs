using LNSF.Api.ViewModels;
using LNSF.Domain.Filters;
using Xunit;

namespace LNSF.Test.Apis;

public class EmergencyContactTestApiGet : GlobalClientRequest
{
    [Fact]
    public async Task QueryContact_OK()
    {
        var contact = await GetEmergencyContact();

        var contactQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id));

        Assert.Equivalent(contact.Id, contactQueried.Id);
        Assert.Equivalent(contact.Name, contactQueried.Name);
        Assert.Equivalent(contact.Phone, contactQueried.Phone);
        Assert.Equivalent(contact.PeopleId, contactQueried.PeopleId);
    }

    [Fact]
    public async Task QueryContactName_OK()
    {
        var contact = await GetEmergencyContact();

        var contactNameQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, name: contact.Name));

        Assert.Equivalent(contact.Name, contactNameQueried.Name);
    }

    [Fact]
    public async Task QueryContactPhone_OK()
    {
        var contact = await GetEmergencyContact();

        var contactPhoneQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, phone: contact.Phone));

        Assert.Equivalent(contact.Phone, contactPhoneQueried.Phone);
    }

    [Fact]
    public async Task QueryContactPeopleId_OK()
    {
        var contact = await GetEmergencyContact();

        var contactPeopleIdQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, peopleId: contact.PeopleId));

        Assert.Equivalent(contact.PeopleId, contactPeopleIdQueried.PeopleId);
    }

    [Fact]
    public async Task QueryContactGetPeople_OK()
    {
        var people = await GetPeople();
        var contact = await GetEmergencyContact(peopleId: people.Id);

        var contactGetPeopleQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, getPeople: true));

        Assert.Equivalent(people, contactGetPeopleQueried.People);
    }

    [Fact]
    public async Task QueryContactGlobalFilter_OK()
    {
        var people = await GetPeople();
        var contact = await GetEmergencyContact(peopleId: people.Id);
        var contactQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id));

        var contactNameQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, globalFilter: contact.Name));
        var contactPhoneQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, globalFilter: contact.Phone));
        var contactPeopleNameQueried = await QuerySingle<EmergencyContactViewModel>(_emergencyContactClient, new EmergencyContactFilter(id: contact.Id, globalFilter: people.Name));

        Assert.Equivalent(contactQueried, contactNameQueried);
        Assert.Equivalent(contactQueried, contactPhoneQueried);
        Assert.Equivalent(contactQueried, contactPeopleNameQueried);
    }

}
