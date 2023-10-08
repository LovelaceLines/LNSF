using AutoMapper;
using LNSF.Application.Services;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LNSF.Application.Interfaces;

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly ITourService _service;
    private readonly IMapper _mapper;

    public TourController(ITourService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TourViewModel>>> Get([FromQuery]TourFilter filter)
    {
        var tours = await _service.Query(filter);
        var tourViewModels = _mapper.Map<List<TourViewModel>>(tours);

        return Ok(tourViewModels);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _service.GetCount());

    [HttpPost]
    public async Task<ActionResult<TourViewModel>> Post([FromBody]TourPostViewModel tour)
    {
        var tourMapped = _mapper.Map<Tour>(tour);
        var tourCreated = await _service.Create(tourMapped);
        var tourViewModel = _mapper.Map<TourViewModel>(tourCreated);

        return Ok(tourViewModel);
    }

    [HttpPut]
    public async Task<ActionResult<TourViewModel>> Put([FromBody]TourPutViewModel tour)
    {
        var tourMapped = _mapper.Map<Tour>(tour);
        var tourUpdated = await _service.Update(tourMapped);
        var tourViewModel = _mapper.Map<TourViewModel>(tourUpdated);

        return Ok(tourViewModel);
    }

    [HttpPut("put-all")]
    public async Task<ActionResult<TourViewModel>> Put([FromBody]TourViewModel tour)
    {
        var tourMapped = _mapper.Map<Tour>(tour);
        var tourUpdated = await _service.UpdateAll(tourMapped);
        var tourViewModel = _mapper.Map<TourViewModel>(tourUpdated);

        return Ok(tourViewModel);
    }
}
