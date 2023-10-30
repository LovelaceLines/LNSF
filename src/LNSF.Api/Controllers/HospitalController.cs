using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HospitalController : ControllerBase
{
    private readonly IHospitalService _service;
    private readonly IMapper _mapper;

    public HospitalController(IHospitalService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of hospitals based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<HospitalViewModel>>> Get([FromQuery]HospitalFilter filter)
    {
        var hospitals = await _service.Query(filter);
        var hospitalsViewModel = _mapper.Map<List<HospitalViewModel>>(hospitals);

        return Ok(hospitalsViewModel);
    }

    /// <summary>
    /// Gets the count of hospitals.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<Hospital>> GetCount() => 
        Ok(await _service.GetCount());

    /// <summary>
    /// Creates a new hospital.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<HospitalViewModel>> Post([FromBody]HospitalPostViewModel hospital)
    {
        var hospitalMapped = _mapper.Map<Hospital>(hospital);
        var hospitalCreated = await _service.Create(hospitalMapped);
        var hospitalViewModel = _mapper.Map<HospitalViewModel>(hospitalCreated);

        return Ok(hospitalViewModel);
    }

    /// <summary>
    /// Updates a hospital.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<HospitalViewModel>> Put([FromBody]HospitalViewModel hospital)
    {
        var hospitalMapped = _mapper.Map<Hospital>(hospital);
        var hospitalUpdated = await _service.Update(hospitalMapped);
        var hospitalViewModel = _mapper.Map<HospitalViewModel>(hospitalUpdated);

        return Ok(hospitalViewModel);
    }
}
