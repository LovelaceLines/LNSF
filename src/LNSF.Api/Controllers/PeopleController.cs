using AutoMapper;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LNSF.Application.Interfaces;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPeopleService _peopleService;
    private readonly IPeopleRoomService _peopleRoomService;

    public PeopleController(IPeopleService peopleService,
        IMapper mapper,
        IPeopleRoomService peopleRoomService)
    {
        _mapper = mapper;
        _peopleService = peopleService;
        _peopleRoomService = peopleRoomService;
    }

    /// <summary>
    /// Retrieves a list of people based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<PeopleViewModel>>> Get([FromQuery]PeopleFilter filter)
    {
        var peoples = await _peopleService.Query(filter);
        var peoplesViewModel = _mapper.Map<List<PeopleViewModel>>(peoples);
        
        return Ok(peoplesViewModel);
    }

    /// <summary>
    /// Gets the count of people.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _peopleService.GetCount());

    /// <summary>
    /// Creates a new person. Note: do not create a person with a room.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PeopleViewModel>> Post([FromBody]PeoplePostViewModel people)
    {
        var peopleMapped = _mapper.Map<People>(people);
        var peopleCreated = await _peopleService.Create(peopleMapped);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleCreated);
        
        return Ok(peopleViewModel);
    }

    /// <summary>
    /// Updates a person's information. Note: do not update the person's room.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeoplePutViewModel people)
    {
        var peopleMapper = _mapper.Map<People>(people);
        var peopleUpdated = await _peopleService.Update(peopleMapper);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

        return Ok(peopleViewModel);
    }

    /// <summary>
    /// Adds people to a room.
    /// </summary>
    [HttpPost("add-people-to-room")]
    public async Task<ActionResult<PeopleRoomViewModel>> Post([FromBody]PeopleRoomViewModel peopleRoomPostViewModel)
    {
        var peopleRoom = _mapper.Map<PeopleRoom>(peopleRoomPostViewModel);
        var peopleRoomUpdated = await _peopleRoomService.Create(peopleRoom);
        var peopleRoomViewModel = _mapper.Map<PeopleRoomViewModel>(peopleRoomUpdated);

        return Ok(peopleRoomViewModel);
    }

    /// <summary>
    /// Removes a person from a room.
    /// </summary>
    [HttpDelete("remove-people-from-room")]
    public async Task<ActionResult<PeopleRoomViewModel>> Delete([FromQuery]int peopleRoomId)
    {
        var peopleDeleted = await _peopleRoomService.Delete(peopleRoomId);
        var peopleRoomViewModel = _mapper.Map<PeopleRoomViewModel>(peopleDeleted);

        return Ok(peopleRoomViewModel);
    }
}
