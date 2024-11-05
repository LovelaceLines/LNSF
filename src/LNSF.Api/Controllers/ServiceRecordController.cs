using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRecordController(IServiceRecordRepository repository, IServiceRecordService service) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<ServiceRecord>>> Query([FromQuery] BaseFilter filter) =>
		Ok(await repository.Query(filter));

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<ServiceRecord>> Post([FromBody] ServiceRecord serviceRecord) =>
		Ok(await service.Create(serviceRecord));

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<ServiceRecord>> Put([FromBody] ServiceRecord serviceRecord) =>
		Ok(await service.Update(serviceRecord));

	[Authorize(Policy = "Base")]
	[HttpDelete("{id}")]
	public async Task<ActionResult<ServiceRecord>> Delete(int id) =>
		Ok(await service.Delete(id));
}
