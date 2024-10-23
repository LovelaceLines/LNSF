using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRecordController(IServiceRecordRepository repository, IServiceRecordService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<ServiceRecord>>> Query([FromQuery] BaseFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<ServiceRecord>> Post([FromBody] ServiceRecord serviceRecord) =>
		Ok(await service.Create(serviceRecord));

	[HttpPut]
	public async Task<ActionResult<ServiceRecord>> Put([FromBody] ServiceRecord serviceRecord) =>
		Ok(await service.Update(serviceRecord));

	[HttpDelete("{id}")]
	public async Task<ActionResult<ServiceRecord>> Delete(int id) =>
		Ok(await service.Delete(id));
}
