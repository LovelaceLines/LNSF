using LNSF.Application.Services;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmergencyContactController : ControllerBase
{
    private readonly EmergencyContactService _emergencyContactService;

    public EmergencyContactController(EmergencyContactService emergencyContactService) => 
        _emergencyContactService = emergencyContactService;
    
    [HttpGet]
    public async Task<ActionResult<ResultDTO<List<EmergencyContact>>>> Get([FromBody]Pagination pagination)
    {
        var result = await _emergencyContactService.Get(pagination);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResultDTO<EmergencyContact>>> Get(int id)
    {
        var result = await _emergencyContactService.Get(id);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpGet("quantity")]
    public async Task<ActionResult<ResultDTO<int>>> GetQuantity()
    {
        var result = await _emergencyContactService.GetQuantity();

        return result.Error ? StatusCode(500, result) : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ResultDTO<EmergencyContact>>> Post([FromBody]EmergencyContact contact)
    {
        var result = await _emergencyContactService.Post(contact);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ResultDTO<EmergencyContact>>> Put([FromBody]EmergencyContact contact)
    {
        var result = await _emergencyContactService.Put(contact);

        return result.Error ? BadRequest(result) : Ok(result);
    }
}
