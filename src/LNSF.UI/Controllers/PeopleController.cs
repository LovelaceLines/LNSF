using AutoMapper;
using LNSF.Application;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.UI.ViewModels;
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
    public async Task<ActionResult<List<PeopleViewModel>>> Get([FromQuery]PeopleFilter filter)
    {
        var peoples = await _peopleService.Query(filter);
        var peoplesViewModel = _mapper.Map<List<PeopleViewModel>>(peoples);
        
        return Ok(peoplesViewModel);
    }

    [HttpGet("quantity")]
    public async Task<ActionResult<int>> GetQuantity() => 
        Ok(await _peopleService.GetQuantity());

    [HttpPost]
    public async Task<ActionResult<PeopleViewModel>> Post([FromBody]PeoplePostViewModel people)
    {
        var peopleMapped = _mapper.Map<People>(people);
        var peopleCreated = await _peopleService.Create(peopleMapped);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleCreated);
        
        return Ok(peopleViewModel);
    }

    [HttpPut]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeoplePutViewModel people)
    {
        var peopleMapper = _mapper.Map<People>(people);
        var peopleUpdated = await _peopleService.Update(peopleMapper);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

        return Ok(peopleViewModel);
    }

    [HttpPut("add-people-to-room")]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeopleAddPeopleToRoomViewModel Ids)
    {
        var peopleUpdated = await _peopleService.AddPeopleToRoom(Ids.PeopleId, Ids.RoomId);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

        return Ok(peopleViewModel);
    }

    [HttpPut("remove-people-from-room")]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeopleRemovePeopleFromRoom peopleId)
    {
        var peopleUpdated = await _peopleService.RemovePeopleFromRoom(peopleId.PeopleId);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

        return Ok(peopleViewModel);
    }
}
