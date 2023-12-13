using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IPatientTreatmentService _patientTreatmentService;
    private readonly IMapper _mapper;

    public PatientController(IPatientService patientService,
        IPatientTreatmentService patientTreatmentService,
        IMapper mapper)
    {
        _patientService = patientService;
        _patientTreatmentService = patientTreatmentService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of patients based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PatientViewModel>>> Get([FromQuery] PatientFilter filter)
    {
        var patients = await _patientService.Query(filter);
        return _mapper.Map<List<PatientViewModel>>(patients);
    }

    /// <summary>
    /// Gets the count of patients.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        await _patientService.Count();

    /// <summary>
    /// Creates a new patient.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PatientViewModel>> Post(PatientPostViewModel patientPostViewModel)
    {
        var patient = _mapper.Map<Patient>(patientPostViewModel);
        patient = await _patientService.Create(patient);
        return _mapper.Map<PatientViewModel>(patient);
    }

    /// <summary>
    /// Adds a treatment to a patient.
    /// </summary>
    [HttpPost("add-treatment-to-patient")]
    public async Task<ActionResult<PatientTreatmentViewModel>> AddTreatmentToPatient(PatientTreatmentViewModel patientTreatmentViewModel)
    {
        var patientTreatment = _mapper.Map<PatientTreatment>(patientTreatmentViewModel);
        patientTreatment = await _patientTreatmentService.Create(patientTreatment);
        return _mapper.Map<PatientTreatmentViewModel>(patientTreatment);
    }

    /// <summary>
    /// Updates a patient.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<PatientViewModel>> Put(PatientViewModel patientViewModel)
    {
        var patient = _mapper.Map<Patient>(patientViewModel);
        patient = await _patientService.Update(patient);
        return _mapper.Map<PatientViewModel>(patient);
    }

    /// <summary>
    /// Deletes a patient by id. Note: this action results in a cascade delete (Hosting).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<PatientViewModel>> Delete(int id)
    {
        var patient = await _patientService.Delete(id);
        return _mapper.Map<PatientViewModel>(patient);
    }

    /// <summary>
    /// Removes a treatment from a patient.
    /// </summary>
    [HttpDelete("remove-treatment-from-patient")]
    public async Task<ActionResult<PatientTreatmentViewModel>> RemoveTreatmentFromPatient(PatientTreatmentViewModel patientTreatmentViewModel)
    {
        var patientTreatment = await _patientTreatmentService.Delete(patientTreatmentViewModel.PatientId, patientTreatmentViewModel.TreatmentId);
        return _mapper.Map<PatientTreatmentViewModel>(patientTreatment);
    }
}