using LNSF.Application.Interfaces;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostingController(IHostingRepository repository,
	IHostingService service,
	IHostingEscortService hostingEscortService) : ControllerBase

{
	[Authorize(Policy = "User")]
	[HttpGet]
	public async Task<ActionResult<QueryResult<HostingDTO>>> Get([FromQuery] HostingFilter filter) =>
		await repository.Query(filter);

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<Hosting>> Post(Hosting hosting) =>
		await service.Create(hosting);

	[Authorize(Policy = "User")]
	[HttpPost("add-escort-to-hosting")]
	public async Task<ActionResult<HostingEscort>> AddEscortToHosting([FromBody] HostingEscort hostingEscort) =>
		await hostingEscortService.Create(hostingEscort);

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<Hosting>> Put(Hosting hosting) =>
		await service.Update(hosting);

	[Authorize(Policy = "Base")]
	[HttpDelete("remove-escort-from-hosting")]
	public async Task<ActionResult<HostingEscort>> RemoveEscortFromHosting([FromBody] HostingEscort hostingEscort) =>
		await hostingEscortService.Delete(hostingEscort);
}
