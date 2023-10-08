using AutoMapper;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using LNSF.Application.Interfaces;

namespace LNSF.UI.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<List<EmergencyContactViewModel>>> Get([FromQuery] EmergencyContactFilter filter)
    {
        var contacts = await _service.Query(filter);
        var contactsViewModel = _mapper.Map<List<EmergencyContactViewModel>>(contacts);

        return Ok(contactsViewModel);
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() => 
        Ok(await _service.GetCount());

    [HttpPost]
    public async Task<ActionResult<EmergencyContactViewModel>> Post([FromBody]EmergencyContactPostViewModel contact)
    {
        var contactMapped = _mapper.Map<EmergencyContact>(contact);
        var contactCreated = await _service.Create(contactMapped);
        var contactViewModel = _mapper.Map<EmergencyContactViewModel>(contactCreated);

        return Ok(contactViewModel);
    }

    [HttpPut]
    public async Task<ActionResult<EmergencyContactViewModel>> Put([FromBody]EmergencyContactViewModel contact)
    {
        var contactMapped = _mapper.Map<EmergencyContact>(contact);
        var contactEdited = await _service.Update(contactMapped);
        var contactViewModel = _mapper.Map<EmergencyContactViewModel>(contactEdited);

        return Ok(contactViewModel);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<EmergencyContactViewModel>> Delete(int id)
    {
        var contactDeleted = await _service.Delete(id);
        var contactsViewModel = _mapper.Map<EmergencyContactViewModel>(contactDeleted);

        return Ok(contactsViewModel);
    }
}
