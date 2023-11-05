using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostingController : ControllerBase

{
    private readonly IHostingService _hostingService;
    private readonly IMapper _mapper;

    public HostingController(IHostingService hostingService, 
        IMapper mapper)
    {
        _hostingService = hostingService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of hostings based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<HostingViewModel>>> Get([FromQuery] HostingFilter filter)
    {
        var hostings = await _hostingService.Query(filter);
        var hostingsViewModel = _mapper.Map<List<HostingViewModel>>(hostings);

        return Ok(hostingsViewModel);
    }

    /// <summary>
    /// Gets the count of hosting.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _hostingService.GetCount());

    /// <summary>
    /// Creates a new hosting.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<HostingViewModel>> Post(HostingPostViewModel hosting)
    {
        var hostingMapped = _mapper.Map<Hosting>(hosting);
        var hostingCreated = await _hostingService.Create(hostingMapped);
        var hostingViewModel = _mapper.Map<HostingViewModel>(hostingCreated);

        return Ok(hostingViewModel);
    }

    /// <summary>
    /// Updates a hosting. Note: the patient cannot be changed.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<HostingViewModel>> Put(HostingViewModel hosting)
    {
        var hostingMapped = _mapper.Map<Hosting>(hosting);
        var hostingUpdated = await _hostingService.Update(hostingMapped);
        var hostingViewModel = _mapper.Map<HostingViewModel>(hostingUpdated);
        
        return Ok(hostingViewModel);
    }

}