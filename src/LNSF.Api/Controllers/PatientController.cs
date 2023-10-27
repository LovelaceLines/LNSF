using Microsoft.AspNetCore.Mvc;
using LNSF.Domain.Entities;
using LNSF.Application.Interfaces;
namespace LNSF.Api.Controller;
[ApiController]
[Route("api/[controller]")]

public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Patient patient)
    {
        var newPatient = await _patientService.Create(patient);
        return Ok(newPatient);
    }
    [HttpPut]
    public async Task<IActionResult> Put(Patient patient)
    {
        var updatedPatient = await _patientService.Update(patient);
        return Ok(updatedPatient);
    }

    [HttpGet]
    public async Task<IActionResult> GetCount()
    {
        var count = await _patientService.Count();
        return Ok(count);
    }
}