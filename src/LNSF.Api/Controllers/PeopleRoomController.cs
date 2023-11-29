using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleRoomController : ControllerBase
{
    private readonly IPeopleRoomService _service;
    private readonly IMapper _mapper;

    public PeopleRoomController(IPeopleRoomService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of objects based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PeopleRoomViewModel>>> Get([FromQuery]PeopleRoomFilter filter)
    {
        var peopleRooms = await _service.Query(filter);
        var peopleRoomsViewModel = _mapper.Map<List<PeopleRoomViewModel>>(peopleRooms);
        
        return Ok(peopleRoomsViewModel);
    }

    /// <summary>
    /// Gets the count of PeoplesRooms.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _service.GetCount());
}
