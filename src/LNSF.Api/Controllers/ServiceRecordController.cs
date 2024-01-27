using AutoMapper;
using LNSF.Api.ViewModels;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRecordController : ControllerBase
{
    private readonly IServiceRecordService _service;
    private readonly IMapper _mapper;

    public ServiceRecordController(IServiceRecordService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of service records based on the provided filter.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ServiceRecordViewModel>>> Query([FromQuery] ServiceRecordFilter filter)
    {
        var serviceRecords = await _service.Query(filter);
        return _mapper.Map<List<ServiceRecordViewModel>>(serviceRecords);
    }

    /// <summary>
    /// Retrieves the count of service records.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _service.GetCount();

    /// <summary>
    /// Creates a new service record.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceRecordViewModel>> Post([FromBody] ServiceRecordPostViewModel serviceRecordPostViewModel)
    {
        var serviceRecord = _mapper.Map<ServiceRecord>(serviceRecordPostViewModel);
        serviceRecord = await _service.Create(serviceRecord);
        return _mapper.Map<ServiceRecordViewModel>(serviceRecord);
    }

    /// <summary>
    /// Updates a service record.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ServiceRecordViewModel>> Put([FromBody] ServiceRecordViewModel serviceRecordViewModel)
    {
        var serviceRecord = _mapper.Map<ServiceRecord>(serviceRecordViewModel);
        serviceRecord = await _service.Update(serviceRecord);
        return _mapper.Map<ServiceRecordViewModel>(serviceRecord);
    }

    /// <summary>
    /// Deletes a service record by its ID.
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceRecordViewModel>> Delete(int id)
    {
        var serviceRecord = await _service.Delete(id);
        return _mapper.Map<ServiceRecordViewModel>(serviceRecord);
    }
}
