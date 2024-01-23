using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientTreatmentController : ControllerBase
{
    private readonly IPatientTreatmentService _service;
    private readonly IMapper _mapper;

    public PatientTreatmentController(IPatientTreatmentService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PatientTreatmentViewModel>>> Get([FromQuery] PatientTreatmentFilter filter)
    {
        var patientsTreatments = await _service.Query(filter);
        return _mapper.Map<List<PatientTreatmentViewModel>>(patientsTreatments);
    }

    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _service.GetCount();
}
