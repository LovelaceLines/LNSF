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
        var escortsViewModel = _mapper.Map<List<EscortViewModel>>(escorts);

        return Ok(escortsViewModel);
    }

    /// <summary>
    /// Gets the count of escorts.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _escortService.GetCount());

    /// <summary>
    /// Creates a new escort.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EscortViewModel>> Post(EscortPostViewModel escort)
    {
        var escortMapped = _mapper.Map<Escort>(escort);
        var escortCreated = await _escortService.Create(escortMapped);
        var escortViewModel = _mapper.Map<EscortViewModel>(escortCreated);

        return Ok(escortViewModel);
    }

    /// <summary>
    /// Updates an escort.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<EscortViewModel>> Put(EscortViewModel escort)
    {
        var escortMapped = _mapper.Map<Escort>(escort);
        var escortUpdated = await _escortService.Update(escortMapped);
        var escortViewModel = _mapper.Map<EscortViewModel>(escortUpdated);
        
        return Ok(escortViewModel);
    }

}