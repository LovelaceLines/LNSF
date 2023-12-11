using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostingEscortController : ControllerBase
{
    private readonly IHostingEscortService _service;
    private readonly IMapper _mapper;

    public HostingEscortController(IHostingEscortService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<HostingEscortViewModel>>> Query([FromQuery] HostingEscortFilter filter)
    {
        var hostingsEscorts = await _service.Query(filter);
        return _mapper.Map<List<HostingEscortViewModel>>(hostingsEscorts);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCount() => 
        Ok(await _service.GetCount());
}
