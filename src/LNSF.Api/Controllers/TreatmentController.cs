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
        return _mapper.Map<List<TreatmentViewModel>>(treatments);
    }

    /// <summary>
    /// Gets the count of treatments.
    /// </summary>
    [HttpGet("Count")]
    public async Task<ActionResult<int>> GetCount() => 
        await _treatmentService.GetCount();

    /// <summary>
    /// Creates a new Treatment. Note: the new Treatment's must have a unique name or different type from the existing ones.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TreatmentViewModel>> Post(TreatmentPostViewModel treatmentPostViewModel)
    {
        var treatment = _mapper.Map<Treatment>(treatmentPostViewModel);
        treatment = await _treatmentService.Create(treatment);
        return _mapper.Map<TreatmentViewModel>(treatment);
    }

    /// <summary>
    /// Updates a Treatment.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<TreatmentViewModel>> Put(TreatmentViewModel treatmentViewModel)
    {
        var treatment = _mapper.Map<Treatment>(treatmentViewModel);
        treatment = await _treatmentService.Update(treatment);
        return _mapper.Map<TreatmentViewModel>(treatment);
    }

    /// <summary>
    /// Deletes a treatment by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<TreatmentViewModel>> Delete(int id)
    {
        var treatmentDeleted = await _treatmentService.Delete(id);
        return _mapper.Map<TreatmentViewModel>(treatmentDeleted);
    }
}
