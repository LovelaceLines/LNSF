using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class PatientTestApi : GlobalClientRequest
{
	[Fact]
	public async Task Get_QueryPatient_Ok()
	{
		var patient = await GetPatient();

		var result = await GetFromQuery<QueryResult<Patient>>(_patientClient, new PatientFilter
		{
			Id = patient.Id,
			PeopleId = patient.PeopleId,
			HospitalId = patient.HospitalId,
			SocioEconomicRecord = patient.SocioeconomicRecord,
			Term = patient.Term,
		});
		var patientQueried = result.Items.Single();

		Assert.Equal(patient.Id, patientQueried.Id);
		Assert.Equal(patient.SocioeconomicRecord, patientQueried.SocioeconomicRecord);
		Assert.Equal(patient.Term, patientQueried.Term);
		Assert.Equal(patient.PeopleId, patientQueried.PeopleId);
		Assert.Equal(patient.HospitalId, patientQueried.HospitalId);
	}

	[Fact]
	public async Task Post_Patient_Ok()
	{
		var people = await GetPeople();
		var hospital = await GetHospital();
		var patientFake = new PatientFake(peopleId: people.Id, hospitalId: hospital.Id).Generate();

		var patientPosted = await PostFromBody<Patient>(_patientClient, patientFake);

		Assert.Equal(patientFake.PeopleId, patientPosted.PeopleId);
		Assert.Equal(patientFake.HospitalId, patientPosted.HospitalId);
		Assert.Equal(patientFake.SocioeconomicRecord, patientPosted.SocioeconomicRecord);
		Assert.Equal(patientFake.Term, patientPosted.Term);
	}

	[Fact]
	public async Task Post_PatientsWithSameHospital_Ok()
	{
		var hospital = await GetHospital();
		var people1 = await GetPeople();
		var people2 = await GetPeople();
		var patientFake1 = new PatientFake(peopleId: people1.Id, hospitalId: hospital.Id).Generate();
		var patientFake2 = new PatientFake(peopleId: people2.Id, hospitalId: hospital.Id).Generate();

		var patientPosted1 = await PostFromBody<Patient>(_patientClient, patientFake1);
		var patientPosted2 = await PostFromBody<Patient>(_patientClient, patientFake2);

		Assert.Equal(patientFake1.PeopleId, patientPosted1.PeopleId);
		Assert.Equal(patientFake1.HospitalId, patientPosted1.HospitalId);
		Assert.Equal(patientFake2.PeopleId, patientPosted2.PeopleId);
		Assert.Equal(patientFake2.HospitalId, patientPosted2.HospitalId);
	}

	[Fact]
	public async Task Post_PatientWithExistsPeopleId_Conflict()
	{
		var people = await GetPeople();
		var patient = await GetPatient(peopleId: people.Id);
		var patientFake = new PatientFake(peopleId: patient.PeopleId, hospitalId: patient.HospitalId).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_patientClient, patientFake);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Fact]
	public async Task Post_PatientWithInvalidHospital_NotFound()
	{
		var people = await GetPeople();
		var patientFake = new PatientFake(peopleId: people.Id, hospitalId: 0).Generate();

		var exception = await PostFromBody<AppHttpResponse>(_patientClient, patientFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task Put_ValidPatient_Ok()
	{
		var patient = await GetPatient();
		var patientFake = new PatientFake(id: patient.Id, peopleId: patient.PeopleId, hospitalId: patient.HospitalId).Generate();

		var patientPosted = await PutFromBody<Patient>(_patientClient, patientFake);

		Assert.Equal(patientFake.PeopleId, patientPosted.PeopleId);
		Assert.Equal(patientFake.HospitalId, patientPosted.HospitalId);
		Assert.Equal(patientFake.SocioeconomicRecord, patientPosted.SocioeconomicRecord);
		Assert.Equal(patientFake.Term, patientPosted.Term);
	}

	[Fact]
	public async Task Put_PatientWithNonExistsPeopleId_NotFound()
	{
		var patient1 = await GetPatient();
		var patient2 = await GetPatient();
		var patientFake = new PatientFake(patient1.Id, patient2.PeopleId, patient1.HospitalId).Generate();

		var exception = await PutFromBody<AppHttpResponse>(_patientClient, patientFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}
}
