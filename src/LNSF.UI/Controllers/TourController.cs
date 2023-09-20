using LNSF.Application.Services;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Views;
using Microsoft.AspNetCore.Mvc; 

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly TourService _service;

    public TourController(TourService service) =>
        _service = service;

    [HttpGet]
    public async Task<ActionResult<List<Tour>>> Get([FromBody]Pagination pagination)
    {
        try
        {
            return Ok(await _service.Get(pagination));
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
    public async Task<ActionResult<Tour>> Get(int id)
    {
        try
        {
            return Ok(await _service.Get(id));
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
    public async Task<ActionResult<Tour>> GetQuantity()
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
    public async Task<ActionResult<Tour>> Post([FromBody]Tour tour)
    {
        try
        {
            return await _service.Post(tour);
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
    public async Task<ActionResult<Tour>> Put([FromBody]Tour tour)
    {
        try
        {
            return await _service.Put(tour);
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
