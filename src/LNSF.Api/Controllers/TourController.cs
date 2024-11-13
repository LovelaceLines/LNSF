using LNSF.API.ServiceFilters;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController(ITourRepository repository,
	ITourService service,
	IHttpContextAccessor httpContextAccessor) : ControllerBase
{
	[Authorize(Policy = "User")]
	[HttpGet]
	[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
	public async Task<ActionResult<QueryResult<Tour>>> Query([FromQuery] TourFilter filter)
	{
		var tours = await repository.Query(filter);
		tours = new JsonSerializer<Tour>().Serialize(
			new ContractResolver(
				[
					new()
					{
						Role = "Voluntário",
						PropsSkipSerialization = ["RG", "CPF", "Phone"]
					},
				],
				httpContextAccessor
			),
			tours
		);
		return Ok(tours);
	}

	[Authorize(Policy = "Base")]
	[HttpPost]
	public async Task<ActionResult<Tour>> PostOpenTour([FromBody] Tour tour) =>
		Ok(await service.CreateOpenTour(tour));

	[Authorize(Policy = "Base")]
	[HttpPut]
	public async Task<ActionResult<Tour>> PutCloseTour([FromBody] Tour tour) =>
		Ok(await service.UpdateOpenTourToClose(tour));

	[Authorize(Policy = "Base")]
	[HttpPut("put-all")]
	public async Task<ActionResult<Tour>> Put([FromBody] Tour tour) =>
		Ok(await service.Update(tour));
}
