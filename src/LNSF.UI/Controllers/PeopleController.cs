using AutoMapper;
using LNSF.Application;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

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
    [Authorize]
    public async Task<ActionResult<List<PeopleViewModel>>> Get([FromQuery]PeopleFilters filters)
    {
        try
        {
            var peoples = await _peopleService.Query(filters);
            var peoplesViewModel = _mapper.Map<List<PeopleViewModel>>(peoples);
            
            return Ok(peoplesViewModel);
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
    [Authorize]
    public async Task<ActionResult<int>> GetQuantity()
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
    public async Task<ActionResult<PeopleViewModel>> Post([FromBody]PeoplePostViewModel people)
    {
        try
        {
            var peopleMapped = _mapper.Map<People>(people);
            var peopleCreated = await _peopleService.CreateNewPeople(peopleMapped);
            var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleCreated);
            
            return Ok(peopleViewModel);
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
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeoplePutViewModel people)
    {
        try
        {
            var peopleMapper = _mapper.Map<People>(people);
            var peopleUpdated = await _peopleService.EditBasicInformation(peopleMapper);
            var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

            return Ok(peopleViewModel);
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

    [HttpPut("add-people-to-room")]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeopleAddPeopleToRoomViewModel Ids)
    {
        try
        {
            var peopleUpdated = await _peopleService.AddPeopleToRoom(Ids.PeopleId, Ids.RoomId);
            var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

            return Ok(peopleViewModel);
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

    [HttpPut("remove-people-from-room")]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeopleRemovePeopleFromRoom peopleId)
    {
        try
        {
            var peopleUpdated = await _peopleService.RemovePeopleFromRoom(peopleId.PeopleId);
            var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

            return Ok(peopleViewModel);
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
