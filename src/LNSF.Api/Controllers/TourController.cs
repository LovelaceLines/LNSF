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
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<TourViewModel>>> Get([FromQuery] TourFilter filter)
    {
        var tours = await _service.Query(filter);
        return _mapper.Map<List<TourViewModel>>(tours);
    }

    /// <summary>
    /// Gets the count of tours.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _service.GetCount();

    /// <summary>
    /// Creates the output of a tour. Note: the input is automatically set to the current date.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TourViewModel>> PostOpenTour([FromBody] TourPostViewModel tourPostViewModel)
    {
        var tour = _mapper.Map<Tour>(tourPostViewModel);
        tour = await _service.CreateOpenTour(tour);
        return _mapper.Map<TourViewModel>(tour);
    }

    /// <summary>
    /// Updates the input and note of a tour. Note: the input is automatically set to the current date.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<TourViewModel>> PutCloseTour([FromBody] TourPutViewModel tourPutViewModel)
    {
        var tour = _mapper.Map<Tour>(tourPutViewModel);
        tour = await _service.UpdateOpenTourToClose(tour);
        return _mapper.Map<TourViewModel>(tour);
    }

    /// <summary>
    /// Updates all properties of a tour.
    /// </summary>
    [Authorize]
    [HttpPut("put-all")]
    public async Task<ActionResult<TourViewModel>> Put([FromBody] TourViewModel tourViewModel)
    {
        var tour = _mapper.Map<Tour>(tourViewModel);
        tour = await _service.Update(tour);
        return _mapper.Map<TourViewModel>(tour);
    }
}
