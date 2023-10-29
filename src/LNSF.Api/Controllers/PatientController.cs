using Microsoft.AspNetCore.Mvc;
using LNSF.Domain.Entities;
using LNSF.Application.Interfaces;
using LNSF.Api.ViewModels;
using AutoMapper;
using LNSF.Domain.Filters;
namespace LNSF.Api.Controller;
[ApiController]
[Route("api/[controller]")]

public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;
    public PatientController(IPatientService patientService, IMapper mapper)
    {
        _patientService = patientService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<PatientViewModel>> Post(PatientPostViewModel patient)
    {
        var patientmapped = _mapper.Map<Patient>(patient);
        var newPatient = await _patientService.Create(patientmapped);
        var patientViewModel = _mapper.Map<PatientViewModel>(newPatient);
        return Ok(patientViewModel);
    }
    [HttpPut]
    public async Task<ActionResult<PatientViewModel>> Put(PatientViewModel patient)
    {
        var patientmapped = _mapper.Map<Patient>(patient);
        var updatedPatient = await _patientService.Update(patientmapped);
        var patientViewModel = _mapper.Map<PatientViewModel>(updatedPatient);
        return Ok(patientViewModel);
    }

    [HttpGet("count")]
    public async Task<ActionResult> GetCount()
    {
        var count = await _patientService.Count();
        return Ok(count);
    }
    [HttpGet]
    public async Task<ActionResult<List<PatientViewModel>>> Get([FromQuery] PatientFilter filter)
    {
        var patients = await _patientService.Query(filter);
        var patientsViewModel = _mapper.Map<List<PatientViewModel>>(patients);
        return Ok(patientsViewModel);
    }
}