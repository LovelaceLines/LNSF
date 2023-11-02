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
    public TreatmentController(ITreatmentService treatmentService, IMapper mapper)
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
    public async Task<ActionResult<int>> GetCount()
    {
        var count = await _treatmentService.GetCount();
        return Ok(count);
    }

    [HttpPost]
    public async Task<ActionResult<TreatmentViewModel>> Post(TreatmentPostViewModel treatmentPostViewModel)
    {
        var treatmentMapped = _mapper.Map<Treatment>(treatmentPostViewModel);
        var treatment = await _treatmentService.Create(treatmentMapped);
        var treatmentViewModel = _mapper.Map<TreatmentViewModel>(treatment);
        return Ok(treatmentViewModel);

    }

    [HttpPut]
    public async Task<ActionResult<TreatmentViewModel>> Put(TreatmentViewModel treatmentViewModel)
    {
        var treatmentMapped = _mapper.Map<Treatment>(treatmentViewModel);
        var treatment = await _treatmentService.Update(treatmentMapped);
        var treatmentViewModelMapped = _mapper.Map<TreatmentViewModel>(treatment);
        return Ok(treatmentViewModelMapped);
    }

}
