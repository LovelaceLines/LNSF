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
public class FamilyGroupProfileController : ControllerBase
{
    private readonly IFamilyGroupProfileService _service;
    private readonly IMapper _mapper;

    public FamilyGroupProfileController(IFamilyGroupProfileService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of family group profiles based on the provided filter.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<FamilyGroupProfileViewModel>>> Get([FromQuery] FamilyGroupProfileFilter filter)
    {
        var familyGroupProfiles = await _service.Query(filter);
        return _mapper.Map<List<FamilyGroupProfileViewModel>>(familyGroupProfiles);
    }

    /// <summary>
    /// Gets the count of family group profiles.
    /// </summary>
    [Authorize]
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount() =>
        await _service.GetCount();

    /// <summary>
    /// Creates a family group profile.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<FamilyGroupProfileViewModel>> Post([FromBody] FamilyGroupProfilePostViewModel familyGroupProfilePostViewModel)
    {
        var familyGroupProfile = _mapper.Map<FamilyGroupProfile>(familyGroupProfilePostViewModel);
        familyGroupProfile = await _service.Create(familyGroupProfile);
        return _mapper.Map<FamilyGroupProfileViewModel>(familyGroupProfile);
    }

    /// <summary>
    /// Updates a family group profile.
    /// </summary>
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<FamilyGroupProfileViewModel>> Put([FromBody] FamilyGroupProfileViewModel familyGroupProfileViewModel)
    {
        var familyGroupProfile = _mapper.Map<FamilyGroupProfile>(familyGroupProfileViewModel);
        familyGroupProfile = await _service.Update(familyGroupProfile);
        return _mapper.Map<FamilyGroupProfileViewModel>(familyGroupProfile);
    }

    /// <summary>
    /// Deletes a family group profile.
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<FamilyGroupProfileViewModel>> Delete(int id)
    {
        var familyGroupProfile = await _service.Delete(id);
        return _mapper.Map<FamilyGroupProfileViewModel>(familyGroupProfile);
    }

}
