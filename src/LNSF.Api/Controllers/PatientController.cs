using LNSF.Application.Interfaces;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IPatientRepository repository, IPatientService service, IPatientTreatmentService patientTreatmentService) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<PatientDTO>>> Query([FromQuery] PatientFilter filter) =>
		Ok(await repository.Query(filter));

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<Patient>> Post(Patient patient) =>
		Ok(await service.Create(patient));

	[Authorize(Policy = "Base")]
	[HttpPost("add-treatment-to-patient")]
	public async Task<ActionResult<PatientTreatment>> AddTreatmentToPatient(PatientTreatment patientTreatment) =>
		Ok(await patientTreatmentService.Create(patientTreatment));

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<Patient>> Put(Patient patient) =>
		Ok(await service.Update(patient));

	[Authorize(Policy = "Base")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<Patient>> Delete(int id) =>
		Ok(await service.Delete(id));

	[Authorize(Policy = "Base")]
	[HttpDelete("remove-treatment-from-patient")]
	public async Task<ActionResult<PatientTreatment>> RemoveTreatmentFromPatient(PatientTreatment patientTreatment) =>
		Ok(await patientTreatmentService.Delete(patientTreatment));
}
