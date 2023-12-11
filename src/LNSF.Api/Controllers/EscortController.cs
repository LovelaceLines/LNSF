using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscortController : ControllerBase
{
    private readonly IEscortService _escortService;
    private readonly IMapper _mapper;

    public EscortController(IEscortService escortService, 
        IMapper mapper)
    {
        _escortService = escortService;
        _mapper = mapper;
    }

    /// <summary>
    /// Represents a collection of escorts returned from the query.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<EscortViewModel>>> Get([FromQuery] EscortFilter filter)
    {
        
        var escorts = await _escortService.Query(filter);
        return _mapper.Map<List<EscortViewModel>>(escorts);
    }

    /// <summary>
    /// Gets the count of escorts.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        await _escortService.GetCount();

    /// <summary>
    /// Creates a new escort.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EscortViewModel>> Post(EscortPostViewModel escortPostViewModel)
    {
        var escort = _mapper.Map<Escort>(escortPostViewModel);
        escort = await _escortService.Create(escort);
        return _mapper.Map<EscortViewModel>(escort);
    }

    /// <summary>
    /// Updates an escort.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<EscortViewModel>> Put(EscortViewModel escortViewModel)
    {
        var escort = _mapper.Map<Escort>(escortViewModel);
        escort = await _escortService.Update(escort);
        return _mapper.Map<EscortViewModel>(escort);
    }

    /// <summary>
    /// Deletes an escort by its ID.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<EscortViewModel>> Delete(int id)
    {
        var escort = await _escortService.Delete(id);
        return _mapper.Map<EscortViewModel>(escort);
    }
}