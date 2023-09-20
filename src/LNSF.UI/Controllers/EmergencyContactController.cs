using LNSF.Application.Services;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
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
    public async Task<ActionResult<List<EmergencyContact>>> Get([FromBody] EmergencyContactFilters filters)
    {
        try
        {
            return Ok(await _emergencyContactService.Get(filters));
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
    public async Task<ActionResult<EmergencyContact>> Get(int id)
    {
        try
        {
            return Ok(await _emergencyContactService.Get(id));
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
            return Ok(await _emergencyContactService.GetQuantity());
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
    public async Task<ActionResult<EmergencyContact>> Post([FromBody]EmergencyContact contact)
    {
        try
        {
            return Ok(await _emergencyContactService.CreateNewContact(contact));
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
    public async Task<ActionResult<EmergencyContact>> Put([FromBody]EmergencyContact contact)
    {
        try
        {
            return Ok(await _emergencyContactService.EditContact(contact));
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
