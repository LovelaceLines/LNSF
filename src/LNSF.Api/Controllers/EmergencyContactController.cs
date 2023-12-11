using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmergencyContactController : ControllerBase
{
    private readonly IEmergencyContactService _service;
    private readonly IMapper _mapper;

    public EmergencyContactController(IEmergencyContactService service, 
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of emergency contacts based on the provided filter.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<EmergencyContactViewModel>>> Get([FromQuery] EmergencyContactFilter filter)
    {
        var contacts = await _service.Query(filter);
        return _mapper.Map<List<EmergencyContactViewModel>>(contacts);
    }

    /// <summary>
    /// Gets the count of emergency contacts.
    /// </summary>
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        await _service.GetCount();

    /// <summary>
    /// Creates a new emergency contact to be associated with a person.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<EmergencyContactViewModel>> Post([FromBody]EmergencyContactPostViewModel emergencyContactPostViewModel)
    {
        var contact = _mapper.Map<EmergencyContact>(emergencyContactPostViewModel);
        contact = await _service.Create(contact);
        return _mapper.Map<EmergencyContactViewModel>(contact);
    }

    /// <summary>
    /// Updates an existing emergency contact.
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<EmergencyContactViewModel>> Put([FromBody]EmergencyContactViewModel emergencyContactViewModel)
    {
        var contact = _mapper.Map<EmergencyContact>(emergencyContactViewModel);
        contact = await _service.Update(contact);
        return _mapper.Map<EmergencyContactViewModel>(contact);
    }

    /// <summary>
    /// Deletes an emergency contact by ID.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<EmergencyContactViewModel>> Delete(int id)
    {
        var contact = await _service.Delete(id);
        return _mapper.Map<EmergencyContactViewModel>(contact);
    }
}
