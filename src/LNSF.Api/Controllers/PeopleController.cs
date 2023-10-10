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
    private readonly IPeopleService _service;

    public PeopleController(IPeopleService service,
        IMapper mapper)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<PeopleViewModel>>> Get([FromQuery]PeopleFilter filter)
    {
        var peoples = await _service.Query(filter);
        var peoplesViewModel = _mapper.Map<List<PeopleViewModel>>(peoples);
        
        return Ok(peoplesViewModel);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _service.GetCount());

    [HttpPost]
    public async Task<ActionResult<PeopleViewModel>> Post([FromBody]PeoplePostViewModel people)
    {
        var peopleMapped = _mapper.Map<People>(people);
        var peopleCreated = await _service.Create(peopleMapped);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleCreated);
        
        return Ok(peopleViewModel);
    }

    [HttpPut]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeoplePutViewModel people)
    {
        var peopleMapper = _mapper.Map<People>(people);
        var peopleUpdated = await _service.Update(peopleMapper);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

        return Ok(peopleViewModel);
    }

    [HttpPut("add-people-to-room")]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeopleAddPeopleToRoomViewModel Ids)
    {
        var peopleUpdated = await _service.AddPeopleToRoom(Ids.PeopleId, Ids.RoomId);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

        return Ok(peopleViewModel);
    }

    [HttpPut("remove-people-from-room")]
    public async Task<ActionResult<PeopleViewModel>> Put([FromBody]PeopleRemovePeopleFromRoom peopleId)
    {
        var peopleUpdated = await _service.RemovePeopleFromRoom(peopleId.PeopleId);
        var peopleViewModel = _mapper.Map<PeopleViewModel>(peopleUpdated);

        return Ok(peopleViewModel);
    }
}
