using LNSF.Application;
using LNSF.Domain;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<ActionResult<ResultDTO<List<Room>>>> Get([FromBody]Pagination pagination)
    {
        var result = await _roomService.Get(pagination);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultDTO<Room>>> Get(int id)
    {
        var result = await _roomService.Get(id);

        return result.Error ? BadRequest(result) : Ok(result);
    }


    [HttpGet("quantity")]
    public async Task<ActionResult<ResultDTO<int>>> GetQuantity()
    {
        var result = await _roomService.GetQuantity();

        return result.Error ? StatusCode(500, result) : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ResultDTO<Room>>> Post([FromBody]Room room)
    {
        var result = await _roomService.Post(room);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ResultDTO<Room>>> Put([FromBody]Room room)
    {
        var result = await _roomService.Put(room);

        return result.Error ? BadRequest(result) : Ok(result);
    }
}
