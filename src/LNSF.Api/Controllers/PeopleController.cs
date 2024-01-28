using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPeopleService _peopleService;
    private readonly IPeopleRoomHostingService _peopleRoomHostingService;

    public PeopleController(IPeopleService peopleService,
        IMapper mapper,
        IPeopleRoomHostingService peopleRoomHostingService)
    {
        _mapper = mapper;
        _peopleService = peopleService;
        _peopleRoomHostingService = peopleRoomHostingService;
    }

    /// <summary>
    /// Retrieves a list of people based on the provided filter.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<PeopleViewModel>>> Get([FromQuery] PeopleFilter filter)
    {
        var peoples = await _peopleService.Query(filter);
        return _mapper.Map<List<PeopleViewModel>>(peoples);
    }

    /// <summary>
    /// Gets the count of people.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _peopleService.GetCount();

    /// <summary>
    /// Creates a new people. Note: do not create a people with a room.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PeopleViewModel>> Post([FromBody] PeoplePostViewModel peoplePostViewModel)
    {
        var people = _mapper.Map<People>(peoplePostViewModel);
        people = await _peopleService.Create(people);
        return _mapper.Map<PeopleViewModel>(people);
    }

    /// <summary>
    /// Updates a people's information. Note: do not update the people's room.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody] PeoplePutViewModel peoplePutViewModel)
    {
        var people = _mapper.Map<People>(peoplePutViewModel);
        people = await _peopleService.Update(people);
        return _mapper.Map<PeopleViewModel>(people);
    }

    /// <summary>
    /// Adds people to a room.
    /// </summary>
    [Authorize]
    [HttpPost("add-people-to-room")]
    public async Task<ActionResult<PeopleRoomHostingViewModel>> Post([FromBody] PeopleRoomHostingViewModel peopleRoomHostingViewModel)
    {
        var peopleRoomHosting = _mapper.Map<PeopleRoomHosting>(peopleRoomHostingViewModel);
        peopleRoomHosting = await _peopleRoomHostingService.Create(peopleRoomHosting);
        return _mapper.Map<PeopleRoomHostingViewModel>(peopleRoomHosting);
    }

    /// <summary>
    /// Removes a people from a room.
    /// </summary>
    [Authorize]
    [HttpDelete("remove-people-from-room")]
    public async Task<ActionResult<PeopleRoomHostingViewModel>> Delete(PeopleRoomHostingViewModel peopleRoomHostingViewModel)
    {
        var peopleRoomHosting = _mapper.Map<PeopleRoomHosting>(peopleRoomHostingViewModel);
        peopleRoomHosting = await _peopleRoomHostingService.Delete(peopleRoomHosting);
        return _mapper.Map<PeopleRoomHostingViewModel>(peopleRoomHosting);
    }
}
