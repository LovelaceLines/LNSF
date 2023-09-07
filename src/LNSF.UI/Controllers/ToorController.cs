using LNSF.Application.Services;
using LNSF.Domain.Entities;
using Microsoft.AspNetCore.Mvc; 

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToorController : ControllerBase
{
    private readonly ToorService _service;

    public ToorController(ToorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Toor>>> Get()
    {
        List<Toor> result = await _service.Get();
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Toor>> Get(int id)
    {
        var data = await _service.Get(id);

        if (data == null)
        {
            return NotFound();
        }

        return Ok(data);
    }
}
