using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TreatmentController : ControllerBase
{
    private readonly ITreatmentService _treatmentService;
    private readonly IMapper _mapper;

    public TreatmentController(ITreatmentService treatmentService, 
        IMapper mapper)
    {
        _treatmentService = treatmentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TreatmentViewModel>>> Query([FromQuery] TreatmentFilter filter)
    {
        var treatments = await _treatmentService.Query(filter);
        var treatmentViewModels = _mapper.Map<List<TreatmentViewModel>>(treatments);

        return Ok(treatmentViewModels);
    }

    [HttpGet("Count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _treatmentService.GetCount());

    [HttpPost]
    public async Task<ActionResult<TreatmentViewModel>> Post(TreatmentPostViewModel treatment)
    {
        var treatmentMapped = _mapper.Map<Treatment>(treatment);
        var treatmentCreated = await _treatmentService.Create(treatmentMapped);
        var treatmentViewModel = _mapper.Map<TreatmentViewModel>(treatmentCreated);

        return Ok(treatmentViewModel);
    }

    [HttpPut]
    public async Task<ActionResult<TreatmentViewModel>> Put(TreatmentViewModel treatment)
    {
        var treatmentMapped = _mapper.Map<Treatment>(treatment);
        var treatmentUpdated = await _treatmentService.Update(treatmentMapped);
        var treatmentViewModel = _mapper.Map<TreatmentViewModel>(treatmentUpdated);
        
        return Ok(treatmentViewModel);
    }

}
