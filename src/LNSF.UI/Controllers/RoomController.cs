using AutoMapper;
using LNSF.Application.Services;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;
    private readonly IMapper _mapper;

    public RoomController(RoomService roomService,
        IMapper mapper)
    {
        _roomService = roomService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Room>>> Get([FromQuery]RoomFilters filters)
    {
        try
        {
            return Ok(await _roomService.Get(filters));
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> Get(int id)
    {
        try
        {
            return Ok(await _roomService.Get(id));
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


    [HttpGet("quantity")]
    public async Task<ActionResult<int>> GetQuantity()
    {
        try
        {
            return Ok(await _roomService.GetQuantity());
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Room>> Post([FromBody]RoomPostViewModel room)
    {
        try
        {
            var roomEntity = _mapper.Map<Room>(room);
            roomEntity = await _roomService.Post(roomEntity);

            return Ok(roomEntity);
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<Room>> Put([FromBody]Room room)
    {
        try
        {
            return Ok(await _roomService.Put(room));
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
