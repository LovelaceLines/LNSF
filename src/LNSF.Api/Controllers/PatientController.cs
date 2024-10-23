using LNSF.Application.Interfaces;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IPatientRepository repository, IPatientService service, IPatientTreatmentService patientTreatmentService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<PatientDTO>>> Query([FromQuery] PatientFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<Patient>> Post(Patient patient) =>
		Ok(await service.Create(patient));

	[HttpPost("add-treatment-to-patient")]
	public async Task<ActionResult<PatientTreatment>> AddTreatmentToPatient(PatientTreatment patientTreatment) =>
		Ok(await patientTreatmentService.Create(patientTreatment));

	[HttpPut]
	public async Task<ActionResult<Patient>> Put(Patient patient) =>
		Ok(await service.Update(patient));

	[HttpDelete("{id}")]
	public async Task<ActionResult<Patient>> Delete(int id) =>
		Ok(await service.Delete(id));

	[HttpDelete("remove-treatment-from-patient")]
	public async Task<ActionResult<PatientTreatment>> RemoveTreatmentFromPatient(PatientTreatment patientTreatment) =>
		Ok(await patientTreatmentService.Delete(patientTreatment));
}
