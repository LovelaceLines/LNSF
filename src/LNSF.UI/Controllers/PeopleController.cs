using LNSF.Application;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly PeopleService _peopleService;

    public PeopleController(PeopleService peopleService) => 
        _peopleService = peopleService;

    [HttpGet]
    public async Task<ActionResult<ResultDTO<List<People>>>> Get([FromBody]Pagination pagination)
    {
        var result = await _peopleService.Get(pagination);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultDTO<People>>> Get(int id)
    {
        var result = await _peopleService.Get(id);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpGet("quantity")]
    public async Task<ActionResult<ResultDTO<int>>> GetQuantity()
    {
        var result = await _peopleService.GetQuantity();

        return result.Error ? StatusCode(500, result) : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ResultDTO<People>>> Post([FromBody]People people)
    {
        var result = await _peopleService.Post(people);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ResultDTO<People>>> Put([FromBody]People people)
    {
        var result = await _peopleService.Put(people);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpPost("AddPeopleToRoom")]
    public async Task<ActionResult<ResultDTO<People>>> AddPeopleToRoom([FromBody]PeopleRoomIds ids)
    {
        var result = await _peopleService.AddPeopleToRoom(ids.PeopleId, ids.RoomId);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpPost("RemovePeopleFromRoom")]
    public async Task<ActionResult<ResultDTO<People>>> RemovePeopleFromRoom([FromBody]PeopleRoomIds ids)
    {
        var result = await _peopleService.RemovePeopleFromRoom(ids.PeopleId, ids.RoomId);

        return result.Error ? BadRequest(result) : Ok(result);
    }
}
