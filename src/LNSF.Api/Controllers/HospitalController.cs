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
        return _mapper.Map<List<HospitalViewModel>>(hospitals);
    }

    /// <summary>
    /// Gets the count of hospitals.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        await _service.GetCount();

    /// <summary>
    /// Creates a new hospital.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<HospitalViewModel>> Post([FromBody]HospitalPostViewModel hospitalPostViewModel)
    {
        var hospital = _mapper.Map<Hospital>(hospitalPostViewModel);
        hospital = await _service.Create(hospital);
        return _mapper.Map<HospitalViewModel>(hospital);
    }

    /// <summary>
    /// Updates a hospital.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<HospitalViewModel>> Put([FromBody]HospitalViewModel hospitalViewModel)
    {
        var hospital = _mapper.Map<Hospital>(hospitalViewModel);
        hospital = await _service.Update(hospital);
        return _mapper.Map<HospitalViewModel>(hospital);
    }

    /// <summary>
    /// Deletes a hospital by id. Note: this action results in a cascade delete (Patient).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<HospitalViewModel>> Delete(int id)
    {
        var hospital = await _service.Delete(id);
        return _mapper.Map<HospitalViewModel>(hospital);
    }
}
