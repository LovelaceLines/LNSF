using LNSF.Application;
using LNSF.Domain;
using LNSF.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Room>>> Get()
    {
        return Ok(await _roomService.Get());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> Get(int id)
    {
        var room = await _roomService.Get(id);

        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpPost]
    public async Task<ActionResult<Room>> Post([FromBody]IRoomAdd room)
    {
        var _room = await _roomService.Add(room);

        if (_room == null)
        {
            return BadRequest();
        }

        return Ok(_room);
    }

    [HttpPut]
    public async Task<ActionResult<Room>> Put([FromBody]Room room)
    {
        var _room = await _roomService.Update(room);

        if (_room == null)
        {
            return NotFound();
        }

        return Ok(_room);
    }

    [HttpGet("available/{id}")]
    public async Task<ActionResult<bool>> GetAvailable(int id)
    {
        var _available = await _roomService.Available(id);

        if (!_available)
        {
            return NotFound();
        }

        return Ok(_available);
    }

    [HttpGet("occupation/{id}")]
    public async Task<ActionResult<int>> GetOccupation(int id)
    {
        var _occupation = await _roomService.GetOccupation(id);

        return Ok(_occupation);
    }
}
