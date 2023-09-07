using LNSF.Application.Services;
using LNSF.Domain.Entities;
using Microsoft.AspNetCore.Mvc; 

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly TourService _service;

    public TourController(TourService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Tour>>> Get()
    {
        List<Tour> result = await _service.Get();
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tour>> Get(int id)
    {
        var data = await _service.Get(id);

        if (data == null)
        {
            return NotFound();
        }

        return Ok(data);
    }
}
