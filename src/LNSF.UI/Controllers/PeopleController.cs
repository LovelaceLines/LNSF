﻿using AutoMapper;
using LNSF.Application;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly PeopleService _peopleService;

    public PeopleController(PeopleService peopleService,
        IMapper mapper)
    {
        _mapper = mapper;
        _peopleService = peopleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<People>>> Get([FromQuery]PeopleFilters filters)
    {
        try
        {
            return Ok(await _peopleService.Get(filters));
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
    public async Task<ActionResult<People>> Get(int id)
    {
        try
        {
            return Ok(await _peopleService.Get(id));
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
    public async Task<ActionResult<ResultDTO<int>>> GetQuantity()
    {
        try
        {
            return Ok(await _peopleService.GetQuantity());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<PeopleReturnViewModel>> Post([FromBody]PeoplePostViewModel people)
    {
        try
        {
            var peopleEntity = _mapper.Map<People>(people);
            peopleEntity = await _peopleService.CreateNewPeople(peopleEntity);
            var peopleReturn = _mapper.Map<PeopleReturnViewModel>(peopleEntity);
            
            return Ok(peopleReturn);
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
    public async Task<ActionResult<PeopleReturnViewModel>> Put([FromBody]PeoplePutViewModel people)
    {
        try
        {
            var peopleEntity = _mapper.Map<People>(people);
            peopleEntity = await _peopleService.EditBasicInformation(peopleEntity);
            var peopleReturn = _mapper.Map<PeopleReturnViewModel>(peopleEntity);

            return Ok(peopleReturn);
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

    [HttpPut("AddPeopleToRoom")]
    public async Task<ActionResult<PeopleReturnViewModel>> AddPeopleToRoom(PeopleAddPeopleToRoomViewModel Ids)
    {
        try
        {
            var peopleEntity = await _peopleService.AddPeopleToRoom(Ids.PeopleId, Ids.RoomId);
            var peopleReturn = _mapper.Map<PeopleReturnViewModel>(peopleEntity);

            return Ok(peopleReturn);
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

    [HttpPut("RemovePeopleFromRoom")]
    public async Task<ActionResult<PeopleReturnViewModel>> RemovePeopleFromRoom(PeopleRemovePeopleFromRoom peopleId)
    {
        try
        {
            var peopleEntity = await _peopleService.RemovePeopleFromRoom(peopleId.PeopleId);
            var peopleReturn = _mapper.Map<PeopleReturnViewModel>(peopleEntity);

            return Ok(peopleReturn);
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
