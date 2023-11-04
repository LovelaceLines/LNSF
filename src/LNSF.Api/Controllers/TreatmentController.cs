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

    /// <summary>
    /// Retrieves a list of TreatmentViewModel objects based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TreatmentViewModel>>> Query([FromQuery] TreatmentFilter filter)
    {
        var treatments = await _treatmentService.Query(filter);
        var treatmentViewModels = _mapper.Map<List<TreatmentViewModel>>(treatments);

        return Ok(treatmentViewModels);
    }

    /// <summary>
    /// Gets the count of treatments.
    /// </summary>
    [HttpGet("Count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _treatmentService.GetCount());

    /// <summary>
    /// Creates a new Treatment. Note: the new Treatment's must have a unique name or different type from the existing ones.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TreatmentViewModel>> Post(TreatmentPostViewModel treatment)
    {
        var treatmentMapped = _mapper.Map<Treatment>(treatment);
        var treatmentCreated = await _treatmentService.Create(treatmentMapped);
        var treatmentViewModel = _mapper.Map<TreatmentViewModel>(treatmentCreated);

        return Ok(treatmentViewModel);
    }

    /// <summary>
    /// Updates a Treatment.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<TreatmentViewModel>> Put(TreatmentViewModel treatment)
    {
        var treatmentMapped = _mapper.Map<Treatment>(treatment);
        var treatmentUpdated = await _treatmentService.Update(treatmentMapped);
        var treatmentViewModel = _mapper.Map<TreatmentViewModel>(treatmentUpdated);
        
        return Ok(treatmentViewModel);
    }
}
