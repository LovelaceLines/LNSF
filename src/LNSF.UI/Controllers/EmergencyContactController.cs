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
public class EmergencyContactController : ControllerBase
{
    private readonly EmergencyContactService _emergencyContactService;
    private readonly IMapper _mapper;

    public EmergencyContactController(EmergencyContactService emergencyContactService, 
        IMapper mapper)
    {
        _emergencyContactService = emergencyContactService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmergencyContactViewModel>>> Get([FromQuery] EmergencyContactFilter filter)
    {
        try
        {
            var contacts = await _emergencyContactService.Query(filter);
            var contactsViewModel = _mapper.Map<List<EmergencyContactViewModel>>(contacts);

            return Ok(contactsViewModel);
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
    public async Task<ActionResult<EmergencyContactViewModel>> Post([FromBody]EmergencyContactPostViewModel contact)
    {
        try
        {
            var contactMapped = _mapper.Map<EmergencyContact>(contact);
            var contactCreated = await _emergencyContactService.Create(contactMapped);
            var contactViewModel = _mapper.Map<EmergencyContactViewModel>(contactCreated);

            return Ok(contactViewModel);
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
    public async Task<ActionResult<EmergencyContactViewModel>> Put([FromBody]EmergencyContactViewModel contact)
    {
        try
        {
            var contactMapped = _mapper.Map<EmergencyContact>(contact);
            var contactEdited = await _emergencyContactService.Update(contactMapped);
            var contactViewModel = _mapper.Map<EmergencyContactViewModel>(contactEdited);

            return Ok(contactViewModel);
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

    [HttpDelete("{id}")]
    public async Task<ActionResult<EmergencyContactViewModel>> Delete(int id)
    {
        try
        {
            var contactDeleted = await _emergencyContactService.Delete(id);
            var contactsViewModel = _mapper.Map<EmergencyContactViewModel>(contactDeleted);

            return Ok(contactsViewModel);
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
