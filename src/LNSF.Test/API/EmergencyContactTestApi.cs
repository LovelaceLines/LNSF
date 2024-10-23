using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class EmergencyContactTestApi : GlobalClientRequest
{
	[Fact]
	public async Task Get_Contact_OK()
	{
		var contact = await GetEmergencyContact();

		var result = await GetFromQuery<QueryResult<EmergencyContact>>(_emergencyContactClient, new EmergencyContactFilter
		{
			Id = contact.Id,
			PeopleId = contact.PeopleId,
			Name = contact.Name,
			Phone = contact.Phone,
		});
		var contactQueried = result.Items.Single();

		Assert.Equal(contact.Id, contactQueried.Id);
		Assert.Equal(contact.Name, contactQueried.Name);
		Assert.Equal(contact.Phone, contactQueried.Phone);
		Assert.Equal(contact.PeopleId, contactQueried.PeopleId);
	}

	[Fact]
	public async Task Post_Contact_OK()
	{
		var people = await GetPeople();
		var fake1 = new EmergencyContactFake(peopleId: people.Id).Generate();
		var fake2 = new EmergencyContactFake(peopleId: people.Id).Generate();

		var contact1 = await PostFromBody<EmergencyContact>(_emergencyContactClient, fake1);
		var contact2 = await PostFromBody<EmergencyContact>(_emergencyContactClient, fake2);

		Assert.Equal(fake1.Name, contact1.Name);
		Assert.Equal(fake1.Phone, contact1.Phone);
		Assert.Equal(fake2.Name, contact2.Name);
		Assert.Equal(fake2.Phone, contact2.Phone);
	}

	[Fact]
	public async Task Post_ContactWithInvalidProps_BadRequest()
	{
		var people = await GetPeople();
		var contactFakeWithInvalidPeopleId = new EmergencyContactFake(peopleId: -1).Generate();
		var contactFakeWithoutName = new EmergencyContactFake(peopleId: people.Id, name: "").Generate();
		var contactFakeWithoutPhone = new EmergencyContactFake(peopleId: people.Id, phone: "").Generate();

		var exceptionWithInvalidPeopleId = await PostFromBody<AppHttpResponse>(_emergencyContactClient, contactFakeWithInvalidPeopleId);
		var exceptionWithoutName = await PostFromBody<AppHttpResponse>(_emergencyContactClient, contactFakeWithoutName);
		var exceptionWithoutPhone = await PostFromBody<AppHttpResponse>(_emergencyContactClient, contactFakeWithoutPhone);

		Assert.Equal(HttpStatusCode.NotFound, exceptionWithInvalidPeopleId.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutPhone.StatusCode);
	}

	[Fact]
	public async Task Put_Contact_OK()
	{
		var contact = await GetEmergencyContact();
		var fake = new EmergencyContactFake(contact.Id, contact.PeopleId).Generate();

		var contactPuted = await PutFromBody<EmergencyContact>(_emergencyContactClient, fake);

		Assert.Equivalent(fake, contactPuted);
	}

	[Fact]
	public async Task Put_Contact_BadRequest()
	{
		var contact = await GetEmergencyContact();
		var contactFakeWithInvalidPeopleId = new EmergencyContactFake(id: contact.Id, peopleId: -1).Generate();
		var contactFakeWithoutName = new EmergencyContactFake(id: contact.Id, peopleId: contact.PeopleId, name: "").Generate();
		var contactFakeWithoutPhone = new EmergencyContactFake(id: contact.Id, peopleId: contact.PeopleId, phone: "").Generate();

		var exceptionWithInvalidPeopleId = await PutFromBody<AppHttpResponse>(_emergencyContactClient, contactFakeWithInvalidPeopleId);
		var exceptionWithoutName = await PutFromBody<AppHttpResponse>(_emergencyContactClient, contactFakeWithoutName);
		var exceptionWithoutPhone = await PutFromBody<AppHttpResponse>(_emergencyContactClient, contactFakeWithoutPhone);

		Assert.Equal(HttpStatusCode.NotFound, exceptionWithInvalidPeopleId.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutName.StatusCode);
		Assert.Equal(HttpStatusCode.BadRequest, exceptionWithoutPhone.StatusCode);
	}

	[Fact]
	public async Task Put_ContactWithOtherPeopleId_NotFound()
	{
		var contact1 = await GetEmergencyContact();
		var contact2 = await GetEmergencyContact();
		var contactFakeWithOtherPeopleId = new EmergencyContactFake(id: contact1.Id, peopleId: contact2.PeopleId).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_emergencyContactClient, contactFakeWithOtherPeopleId);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Delete_Contact_OK()
	{
		var contact = await GetEmergencyContact();

		var contactDeleted = await DeleteFromUri<EmergencyContact>(_emergencyContactClient, contact.Id);

		Assert.Equivalent(contact, contactDeleted);
	}

	[Fact]
	public async Task Delete_ContactNonExists_NotFound()
	{
		var exception = await DeleteFromUri<AppHttpResponse>(_emergencyContactClient, -1);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}
}
