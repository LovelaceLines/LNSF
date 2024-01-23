using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _service;
    private readonly IMapper _mapper;

    public RoomController(IRoomService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of rooms based on the provided filter.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<RoomViewModel>>> Get([FromQuery] RoomFilter filter)
    {
        var rooms = await _service.Query(filter);
        return _mapper.Map<List<RoomViewModel>>(rooms);
    }

    /// <summary>
    /// Gets the count of rooms.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _service.GetCount();

    /// <summary>
    /// Creates a new room.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RoomViewModel>> Post([FromBody] RoomPostViewModel roomPostViewModel)
    {
        var room = _mapper.Map<Room>(roomPostViewModel);
        room = await _service.Create(room);
        return _mapper.Map<RoomViewModel>(room);
    }

    /// <summary>
    /// Updates a room.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RoomViewModel>> Put([FromBody] RoomViewModel roomViewModel)
    {
        var room = _mapper.Map<Room>(roomViewModel);
        room = await _service.Update(room);
        return _mapper.Map<RoomViewModel>(room);
    }
}
