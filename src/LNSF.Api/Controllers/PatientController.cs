using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;

    public PatientController(IPatientService patientService, 
        IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of patients based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PatientViewModel>>> Get([FromQuery] PatientFilter filter)
    {
        var patients = await _patientService.Query(filter);
        var patientsViewModel = _mapper.Map<List<PatientViewModel>>(patients);

        return Ok(patientsViewModel);
    }

    /// <summary>
    /// Gets the count of patients.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult> GetCount() => 
        Ok(await _patientService.Count());

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PatientViewModel>> Post(PatientPostViewModel patient)
    {
        var patientMapped = _mapper.Map<Patient>(patient);
        var patientCreated = await _patientService.Create(patientMapped);
        var patientViewModel = _mapper.Map<PatientViewModel>(patientCreated);

        return Ok(patientViewModel);
    }

    /// <summary>
    /// Updates a patient.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<PatientViewModel>> Put(PatientViewModel patient)
    {
        var patientMapped = _mapper.Map<Patient>(patient);
        var patientUpdated = await _patientService.Update(patientMapped);
        var patientViewModel = _mapper.Map<PatientViewModel>(patientUpdated);

        return Ok(patientViewModel);
    }
}