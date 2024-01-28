using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleRoomHostingController : ControllerBase
{
    private readonly IPeopleRoomHostingService _service;
    private readonly IMapper _mapper;

    public PeopleRoomHostingController(IPeopleRoomHostingService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of objects based on the provided filter.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PeopleRoomHostingViewModel>>> Get([FromQuery] PeopleRoomHostingFilter filter)
    {
        var peopleRoomHostings = await _service.Query(filter);
        return _mapper.Map<List<PeopleRoomHostingViewModel>>(peopleRoomHostings);
    }

    /// <summary>
    /// Gets the count of PeoplesRooms.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _service.GetCount();
}
