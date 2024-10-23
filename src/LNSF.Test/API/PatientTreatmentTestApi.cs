using LNSF.Domain.Entities;
using LNSF.Test.DTOs;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class PatientTreatmentTestApi : GlobalClientRequest
{
	[Fact]
	public async Task Post_AddTreatmentToPatient_Ok()
	{
		var patient = await GetPatient();
		var treatment1 = await GetTreatment();
		var treatment2 = await GetTreatment();
		var patientTreatmentFake1 = new PatientTreatment() { PatientId = patient.Id, TreatmentId = treatment1.Id };
		var patientTreatmentFake2 = new PatientTreatment() { PatientId = patient.Id, TreatmentId = treatment2.Id };

		var patientTreatmentPosted1 = await PostFromBody<PatientTreatment>(_addTreatmentToPatientClient, patientTreatmentFake1);
		var patientTreatmentPosted2 = await PostFromBody<PatientTreatment>(_addTreatmentToPatientClient, patientTreatmentFake2);

		Assert.Equal(patientTreatmentFake1.PatientId, patientTreatmentPosted1.PatientId);
		Assert.Equal(patientTreatmentFake1.TreatmentId, patientTreatmentPosted1.TreatmentId);
		Assert.Equal(patientTreatmentFake2.PatientId, patientTreatmentPosted2.PatientId);
		Assert.Equal(patientTreatmentFake2.TreatmentId, patientTreatmentPosted2.TreatmentId);
	}

	[Fact]
	public async Task AddTreatmentToPatient_PatientTreatmentWithExistsPatientIdAndTreatmentId_Conflict()
	{
		var patientTreatment = await GetPatientTreatment();

		var exception = await PostFromBody<AppHttpResponse>(_addTreatmentToPatientClient, patientTreatment);

		Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
	}

	[Fact]
	public async Task AddTreatmentToPatient_PatientTreatmentWithNotExistsPatientId_NotFound()
	{
		var treatment = await GetTreatment();
		var patientTreatmentFake = new PatientTreatment() { PatientId = 0, TreatmentId = treatment.Id };

		var exception = await PostFromBody<AppHttpResponse>(_addTreatmentToPatientClient, patientTreatmentFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task AddTreatmentToPatient_PatientTreatmentWithNotExistsTreatmentId_NotFound()
	{
		var patient = await GetPatient();
		var patientTreatmentFake = new PatientTreatment() { PatientId = patient.Id, TreatmentId = 0 };

		var exception = await PostFromBody<AppHttpResponse>(_addTreatmentToPatientClient, patientTreatmentFake);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task RemoveTreatmentFromPatient_PatientTreatment_Ok()
	{
		var patientTreatment = await GetPatientTreatment();

		var patientTreatmentDeleted = await DeleteFromBody<PatientTreatment>(_removeTreatmentFromPatientClient, patientTreatment);

		Assert.Equivalent(patientTreatment, patientTreatmentDeleted);
	}

	[Fact]
	public async Task RemoveTreatmentFromPatient_PatientTreatmentWithNotExistsPatientId_NotFound()
	{
		var treatment = await GetTreatment();
		var patientTreatment = new PatientTreatment() { PatientId = 0, TreatmentId = treatment.Id };

		var exception = await DeleteFromBody<AppHttpResponse>(_removeTreatmentFromPatientClient, patientTreatment);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task RemoveTreatmentFromPatient_PatientTreatmentWithNotExistsTreatmentId_NotFound()
	{
		var patient = await GetPatient();
		var patientTreatment = new PatientTreatment() { PatientId = patient.Id, TreatmentId = 0 };

		var exception = await DeleteFromBody<AppHttpResponse>(_removeTreatmentFromPatientClient, patientTreatment);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}

	[Fact]
	public async Task RemoveTreatmentFromPatient_PatientTreatmentWithNotExistsPatientIdAndTreatmentId_NotFound()
	{
		var patientTreatment = new PatientTreatment() { PatientId = 0, TreatmentId = 0 };

		var exception = await DeleteFromBody<AppHttpResponse>(_removeTreatmentFromPatientClient, patientTreatment);

		Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
	}
}
