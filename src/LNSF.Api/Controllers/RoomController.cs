using AutoMapper;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LNSF.Application.Interfaces;

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

    [HttpGet]
    public async Task<ActionResult<List<RoomViewModel>>> Get([FromQuery]RoomFilter filter)
    {
        var rooms = await _service.Query(filter);
        var roomsMapped = _mapper.Map<List<RoomViewModel>>(rooms);

        return Ok(roomsMapped);
    }


    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _service.GetCount());

    [HttpPost]
    public async Task<ActionResult<RoomViewModel>> Post([FromBody]RoomPostViewModel room)
    {
        var roomMapped = _mapper.Map<Room>(room);
        roomMapped = await _service.Create(roomMapped);
        var roomViewModel = _mapper.Map<RoomViewModel>(roomMapped);

        return Ok(roomViewModel);
    }

    [HttpPut]
    public async Task<ActionResult<RoomViewModel>> Put([FromBody]RoomViewModel room)
    {
        var roomMapped = _mapper.Map<Room>(room);
        roomMapped = await _service.Update(roomMapped);
        var roomViewModel = _mapper.Map<RoomViewModel>(roomMapped);

        return Ok(roomViewModel);
    }
}
