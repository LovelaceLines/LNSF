using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class FamilyGroupProfileTestApi : GlobalClientRequest
{
	[Fact]
	public async Task Get_QueryFamilyGroupProfile_Ok()
	{
		var familyGroupProfile = await GetFamilyGroupProfile();

		var result = await GetFromQuery<QueryResult<FamilyGroupProfile>>(_familyGroupProfileClient, new FamilyGroupProfileFilter
		{
			Id = familyGroupProfile.Id,
			PatientId = familyGroupProfile.PatientId,
			Name = familyGroupProfile.Name,
		});
		var familyGroupProfileQueried = result.Items.Single();

		Assert.Equal(familyGroupProfile.Id, familyGroupProfileQueried.Id);
		Assert.Equal(familyGroupProfile.Name, familyGroupProfileQueried.Name);
		Assert.Equal(familyGroupProfile.Kinship, familyGroupProfileQueried.Kinship);
		Assert.Equal(familyGroupProfile.Age, familyGroupProfileQueried.Age);
		Assert.Equal(familyGroupProfile.Profession, familyGroupProfileQueried.Profession);
		Assert.Equal(familyGroupProfile.Income, familyGroupProfileQueried.Income);
	}

	[Fact]
	public async Task Post_FamilyGroupProfile_Ok()
	{
		var patient = await GetPatient();
		var familyGroupProfileFake = new FamilyGroupProfileFake(patientId: patient.Id).Generate();

		var familyGroupProfilePosted = await PostFromBody<FamilyGroupProfile>(_familyGroupProfileClient, familyGroupProfileFake);

		Assert.Equal(familyGroupProfileFake.PatientId, familyGroupProfilePosted.PatientId);
		Assert.Equal(familyGroupProfileFake.Name, familyGroupProfilePosted.Name);
		Assert.Equal(familyGroupProfileFake.Kinship, familyGroupProfilePosted.Kinship);
		Assert.Equal(familyGroupProfileFake.Age, familyGroupProfilePosted.Age);
		Assert.Equal(familyGroupProfileFake.Profession, familyGroupProfilePosted.Profession);
		Assert.Equal(familyGroupProfileFake.Income, familyGroupProfilePosted.Income);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public async Task Post_FamilyGroupProfileWithNonExistsPatientId_NotFound(int patientId)
	{
		var familyGroupProfileFake = new FamilyGroupProfileFake(patientId: patientId).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_familyGroupProfileClient, familyGroupProfileFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Post_FamilyGroupProfilesWithSamePatientId_Ok()
	{
		var patient = await GetPatient();
		var familyGroupProfileFake1 = new FamilyGroupProfileFake(patientId: patient.Id).Generate();
		var familyGroupProfileFake2 = new FamilyGroupProfileFake(patientId: patient.Id).Generate();

		var familyGroupProfilePosted1 = await PostFromBody<FamilyGroupProfile>(_familyGroupProfileClient, familyGroupProfileFake1);
		var familyGroupProfilePosted2 = await PostFromBody<FamilyGroupProfile>(_familyGroupProfileClient, familyGroupProfileFake2);

		Assert.Equal(familyGroupProfileFake1.PatientId, familyGroupProfilePosted1.PatientId);
		Assert.Equal(familyGroupProfileFake1.Name, familyGroupProfilePosted1.Name);
		Assert.Equal(familyGroupProfileFake1.Kinship, familyGroupProfilePosted1.Kinship);
		Assert.Equal(familyGroupProfileFake1.Age, familyGroupProfilePosted1.Age);
		Assert.Equal(familyGroupProfileFake2.PatientId, familyGroupProfilePosted2.PatientId);
		Assert.Equal(familyGroupProfileFake2.Name, familyGroupProfilePosted2.Name);
		Assert.Equal(familyGroupProfileFake2.Kinship, familyGroupProfilePosted2.Kinship);
		Assert.Equal(familyGroupProfileFake2.Age, familyGroupProfilePosted2.Age);
	}

	[Fact]
	public async Task Put_FamilyGroupProfile_Ok()
	{
		var familyGroupProfile = await GetFamilyGroupProfile();
		var familyGroupProfileFake = new FamilyGroupProfileFake(id: familyGroupProfile.Id, patientId: familyGroupProfile.PatientId).Generate();

		var familyGroupProfilePuted = await PutFromBody<FamilyGroupProfile>(_familyGroupProfileClient, familyGroupProfileFake);

		Assert.Equivalent(familyGroupProfileFake, familyGroupProfilePuted);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public async Task Put_FamilyGroupProfileWithNonExistsId_NotFound(int id)
	{
		var patient = await GetPatient();
		var familyGroupProfileFake = new FamilyGroupProfileFake(id: id, patientId: patient.Id).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_familyGroupProfileClient, familyGroupProfileFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	public async Task Put_FamilyGroupProfileWithNonExistsPatientId_NotFound(int patientId)
	{
		var familyGroupProfile = await GetFamilyGroupProfile();
		var familyGroupProfileFake = new FamilyGroupProfileFake(id: familyGroupProfile.Id, patientId: patientId).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_familyGroupProfileClient, familyGroupProfileFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}
}
