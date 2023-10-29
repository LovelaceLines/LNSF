using AutoMapper;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LNSF.Application.Interfaces;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscortController : ControllerBase
{
    private readonly IEscortService _escortService;
    private readonly IMapper _mapper;

    public EscortController(IEscortService escortService, IMapper mapper)
    {
        _escortService = escortService;
        _mapper = mapper;
    }

    [HttpGet("count")] 
    public async Task<ActionResult<int>> GetCount()
    {
        var count = await _escortService.GetCount();
        return Ok(count);
    }

    [HttpPost]
    public async Task<ActionResult<EscortViewModel>> Post(EscortPostViewModel escortPostViewModel)
    {
        var escort = _mapper.Map<Escort>(escortPostViewModel);
        var escortCreated = await _escortService.Create(escort);
        var escortViewModel = _mapper.Map<EscortViewModel>(escortCreated);
        return Ok(escortViewModel);
    }

    [HttpPut]
    public async Task<ActionResult<EscortViewModel>> Put(EscortViewModel escortPutViewModel)
    {
        var escort = _mapper.Map<Escort>(escortPutViewModel);
        var escortUpdated = await _escortService.Update(escort);
        var escortViewModel = _mapper.Map<EscortViewModel>(escortUpdated);
        return Ok(escortViewModel);
    }

    [HttpGet]
    public async Task<ActionResult<List<EscortViewModel>>> Get([FromQuery] EscortFilter filter)
    {
        var escorts = await _escortService.Query(filter);
        var escortsViewModel = _mapper.Map<List<EscortViewModel>>(escorts);
        return Ok(escortsViewModel);
    }
}