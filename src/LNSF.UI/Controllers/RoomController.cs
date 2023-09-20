using LNSF.Application.Services;
using LNSF.Domain;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Views;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomController(RoomService roomService) => 
        _roomService = roomService;

    [HttpGet]
    public async Task<ActionResult<List<Room>>> Get([FromBody]RoomFilters filters)
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
    public async Task<ActionResult<Room>> Post([FromBody]Room room)
    {
        try
        {
            return Ok(await _roomService.Post(room));
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
