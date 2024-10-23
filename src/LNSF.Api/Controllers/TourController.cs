using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController(ITourRepository repository, ITourService service) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<Tour>>> Query([FromQuery] TourFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpPost]
	public async Task<ActionResult<Tour>> PostOpenTour([FromBody] Tour tour) =>
		Ok(await service.CreateOpenTour(tour));

	[HttpPut]
	public async Task<ActionResult<Tour>> PutCloseTour([FromBody] Tour tour) =>
		Ok(await service.UpdateOpenTourToClose(tour));

	[HttpPut("put-all")]
	public async Task<ActionResult<Tour>> Put([FromBody] Tour tour) =>
		Ok(await service.Update(tour));
}
