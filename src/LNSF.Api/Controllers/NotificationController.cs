using LNSF.API.ServiceFilters;
using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LNSF.API.Controllers;

[ApiController]
[Authorize(Policy = "User")]
[Route("api/[controller]")]
public class NotificationController(INotificationRepository repository,
	INotificationService service,
	INotificationUserService notificationUserService) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<QueryResult<Notification>>> Get([FromQuery] NotificationFilter filter) =>
		Ok(await repository.Query(filter));

	[HttpGet("by-user")]
	[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
	public async Task<ActionResult<List<Notification>>> GetByUser()
	{
		var userId = (int)HttpContext.Items["CurrentUserId"]!;
		return Ok(await repository.GetByUserId(userId));
	}

	[HttpGet("unread-count")]
	[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
	public async Task<ActionResult<int>> GetUnreadCount()
	{
		var userId = (int)HttpContext.Items["CurrentUserId"]!;
		return Ok(await repository.CountUnreadByUserId(userId));
	}

	[Authorize(Policy = "Administrador")]
	[HttpPost]
	public async Task<ActionResult<Notification>> Post([FromBody] Notification notification) =>
		Ok(await service.Create(notification));

	[HttpPost("mark-as-read")]
	[ServiceFilter(typeof(AuthAndUserExtractionFilter))]
	public async Task<ActionResult<NotificationUser>> MarkAsRead([FromBody] NotificationUser notificationUser)
	{
		var userId = (int)HttpContext.Items["CurrentUserId"]!;
		notificationUser.UserId = userId;
		return Ok(await notificationUserService.Create(notificationUser));
	}

	[Authorize(Policy = "Administrador")]
	[HttpPut]
	public async Task<ActionResult<Notification>> Put([FromBody] Notification notification) =>
		Ok(await service.Update(notification));

	[Authorize(Policy = "Administrador")]
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id) =>
		Ok(await service.Delete(id));
}
