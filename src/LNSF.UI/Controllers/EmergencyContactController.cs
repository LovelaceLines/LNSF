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
    public async Task<ActionResult<ResultDTO<List<EmergencyContact>>>> Get([FromBody] EmergencyContactFilters filters)
    {
        var result = await _emergencyContactService.Get(filters);

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
        var result = await _emergencyContactService.CreateNewContact(contact);

        return result.Error ? BadRequest(result) : Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<ResultDTO<EmergencyContact>>> Put([FromBody]EmergencyContact contact)
    {
        var result = await _emergencyContactService.EditContact(contact);

        return result.Error ? BadRequest(result) : Ok(result);
    }
}
