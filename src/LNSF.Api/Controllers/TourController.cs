using AutoMapper;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LNSF.Application.Interfaces;

namespace LNSF.Api.Controllers;

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

    /// <summary>
    /// Retrieves a list of tours based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<TourViewModel>>> Get([FromQuery]TourFilter filter)
    {
        var tours = await _service.Query(filter);
        var tourViewModels = _mapper.Map<List<TourViewModel>>(tours);

        return Ok(tourViewModels);
    }

    /// <summary>
    /// Gets the count of tours.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _service.GetCount());

    /// <summary>
    /// Creates the output of a tour.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TourViewModel>> Post([FromBody]TourPostViewModel tour)
    {
        var tourMapped = _mapper.Map<Tour>(tour);
        var tourCreated = await _service.Create(tourMapped);
        var tourViewModel = _mapper.Map<TourViewModel>(tourCreated);

        return Ok(tourViewModel);
    }

    /// <summary>
    /// Updates the input and note of a tour.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<TourViewModel>> Put([FromBody]TourPutViewModel tour)
    {
        var tourMapped = _mapper.Map<Tour>(tour);
        var tourUpdated = await _service.Update(tourMapped);
        var tourViewModel = _mapper.Map<TourViewModel>(tourUpdated);

        return Ok(tourViewModel);
    }

    /// <summary>
    /// Updates all properties of a tour.
    /// </summary>
    [HttpPut("put-all")]
    public async Task<ActionResult<TourViewModel>> Put([FromBody]TourViewModel tour)
    {
        var tourMapped = _mapper.Map<Tour>(tour);
        var tourUpdated = await _service.UpdateAll(tourMapped);
        var tourViewModel = _mapper.Map<TourViewModel>(tourUpdated);

        return Ok(tourViewModel);
    }
}
