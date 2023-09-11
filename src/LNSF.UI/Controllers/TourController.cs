using LNSF.Application.Services;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
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
    public async Task<ActionResult<ResultDTO<List<Tour>>>> Get([FromBody]Pagination pagination)
    {
        var result = await _service.Get(pagination);

        return result.Error ? StatusCode(500, result) : Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultDTO<Tour>>> Get(int id)
    {
        var result = await _service.Get(id);

        return result.Error ? NotFound(result) : Ok(result);
    }

    [HttpGet("quantity")]
    public async Task<ActionResult<ResultDTO<Tour>>> GetQuantity()
    {
        var result = await _service.GetQuantity();

        return result.Error ? StatusCode(500, result) : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ResultDTO<Tour>>> Post([FromBody]Tour tour)
    {
        var result = await _service.Post(tour);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ResultDTO<Tour>>> Put([FromBody]Tour tour)
    {
        var result = await _service.Put(tour);

        return result.Error ? BadRequest(result) : Ok(result);
    }
}
