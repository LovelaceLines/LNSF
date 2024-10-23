using LNSF.Application.Interfaces;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostingController(IHostingRepository repository,
	IHostingService service,
	IHostingEscortService hostingEscortService) : ControllerBase

{
	[HttpGet]
	public async Task<ActionResult<QueryResult<HostingDTO>>> Get([FromQuery] HostingFilter filter) =>
		await repository.Query(filter);

	[HttpPost]
	public async Task<ActionResult<Hosting>> Post(Hosting hosting) =>
		await service.Create(hosting);

	[HttpPost("add-escort-to-hosting")]
	public async Task<ActionResult<HostingEscort>> AddEscortToHosting([FromBody] HostingEscort hostingEscort) =>
		await hostingEscortService.Create(hostingEscort);

	[HttpPut]
	public async Task<ActionResult<Hosting>> Put(Hosting hosting) =>
		await service.Update(hosting);

	[HttpDelete("remove-escort-from-hosting")]
	public async Task<ActionResult<HostingEscort>> RemoveEscortFromHosting([FromBody] HostingEscort hostingEscort) =>
		await hostingEscortService.Delete(hostingEscort);
}
