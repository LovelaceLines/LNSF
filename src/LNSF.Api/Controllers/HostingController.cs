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
    private readonly IHostingEscortService _hostingEscortService;
    private readonly IMapper _mapper;

    public HostingController(IHostingService hostingService,
        IHostingEscortService hostingEscortService,
        IMapper mapper)
    {
        _hostingService = hostingService;
        _hostingEscortService = hostingEscortService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of hostings based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<HostingViewModel>>> Get([FromQuery] HostingFilter filter)
    {
        var hostings = await _hostingService.Query(filter);
        return _mapper.Map<List<HostingViewModel>>(hostings);
    }

    /// <summary>
    /// Gets the count of hosting.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        await _hostingService.GetCount();

    /// <summary>
    /// Creates a new hosting.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<HostingViewModel>> Post(HostingPostViewModel hostingPostViewModel)
    {
        var hosting = _mapper.Map<Hosting>(hostingPostViewModel);
        hosting = await _hostingService.Create(hosting);
        return _mapper.Map<HostingViewModel>(hosting);
    }

    /// <summary>
    /// Adds an escort to a hosting.
    /// </summary>
    [HttpPost("add-escort-to-hosting")]
    public async Task<ActionResult<HostingEscortViewModel>> AddEscortToHosting(HostingEscortViewModel hostingEscortViewModel)
    {
        var hostingEscort = _mapper.Map<HostingEscort>(hostingEscortViewModel);
        hostingEscort = await _hostingEscortService.Create(hostingEscort);
        return _mapper.Map<HostingEscortViewModel>(hostingEscort);
    }


    /// <summary>
    /// Updates a hosting. Note: the patient cannot be changed.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<HostingViewModel>> Put(HostingViewModel hostingViewModel)
    {
        var hosting = _mapper.Map<Hosting>(hostingViewModel);
        hosting = await _hostingService.Update(hosting);
        return _mapper.Map<HostingViewModel>(hosting);
    }

    /// <summary>
    /// Removes an escort from a hosting.
    /// </summary>
    [HttpDelete("remove-escort-from-hosting")]
    public async Task<ActionResult<HostingEscortViewModel>> RemoveEscortFromHosting(HostingEscortViewModel hostingEscortViewModel)
    {
        var hostingEscort = await _hostingEscortService.Delete(hostingEscortViewModel.HostingId, hostingEscortViewModel.EscortId);
        return _mapper.Map<HostingEscortViewModel>(hostingEscort);
    }
}