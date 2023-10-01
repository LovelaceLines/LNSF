﻿using AutoMapper;
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
    public async Task<ActionResult<List<RoomViewModel>>> Get([FromQuery]RoomFilters filters)
    {
        try
        {
            var rooms = await _roomService.Query(filters);
            var roomsMapped = _mapper.Map<List<RoomViewModel>>(rooms);

            return Ok(roomsMapped);
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
    public async Task<ActionResult<RoomViewModel>> Post([FromBody]RoomPostViewModel room)
    {
        try
        {
            var roomMapped = _mapper.Map<Room>(room);
            roomMapped = await _roomService.Create(roomMapped);
            var roomViewModel = _mapper.Map<RoomViewModel>(roomMapped);

            return Ok(roomViewModel);
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
    public async Task<ActionResult<RoomViewModel>> Put([FromBody]RoomViewModel room)
    {
        try
        {
            var roomMapped = _mapper.Map<Room>(room);
            roomMapped = await _roomService.Update(roomMapped);
            var roomViewModel = _mapper.Map<RoomViewModel>(roomMapped);

            return Ok(roomViewModel);
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
