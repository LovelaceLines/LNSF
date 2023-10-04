using AutoMapper;
using LNSF.Application.Services;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc; 

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly TourService _service;
    private readonly IMapper _mapper;

    public TourController(TourService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<TourViewModel>>> Get([FromQuery]TourFilter filter)
    {
        try
        {
            var tours = await _service.Query(filter);
            var tourViewModels = _mapper.Map<List<TourViewModel>>(tours);

            return Ok(tourViewModels);
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
            return Ok(await _service.GetQuantity());
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
    public async Task<ActionResult<TourViewModel>> Post([FromBody]TourPostViewModel tour)
    {
        try
        {
            var tourMapped = _mapper.Map<Tour>(tour);
            var tourCreated = await _service.Create(tourMapped);
            var tourViewModel = _mapper.Map<TourViewModel>(tourCreated);

            return Ok(tourViewModel);
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
    public async Task<ActionResult<TourViewModel>> Put([FromBody]TourPutViewModel tour)
    {
        try
        {
            var tourMapped = _mapper.Map<Tour>(tour);
            var tourUpdated = await _service.Update(tourMapped);
            var tourViewModel = _mapper.Map<TourViewModel>(tourUpdated);

            return Ok(tourViewModel);
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

    [HttpPut("put-all")]
    public async Task<ActionResult<TourViewModel>> Put([FromBody]TourViewModel tour)
    {
        try
        {
            var tourMapped = _mapper.Map<Tour>(tour);
            var tourUpdated = await _service.UpdateAll(tourMapped);
            var tourViewModel = _mapper.Map<TourViewModel>(tourUpdated);

            return Ok(tourViewModel);
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
