using AutoFilterer.Extensions;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class NotificationRepository(AppDbContext context) : BaseRepository<Notification>(context), INotificationRepository
{
	public async Task<QueryResult<Notification>> Query(NotificationFilter filter)
	{
		var query = context.Notifications.ApplyFilterWithoutPagination(filter);
		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();
		return new QueryResult<Notification>(items, totalCount);
	}

	public async Task<int> CountUnreadByUserId(int userId) =>
		await context.Notifications.CountAsync(IsValidNotification(userId));

	public async Task<bool> ExistsByIdAndUserId(int id, int userId) =>
		await context.NotificationsUsers.AnyAsync(nu => nu.NotificationId == id && nu.UserId == userId);

	public async Task<Notification> GetByIdAndUserId(int id, int userId) =>
		await context.Notifications.FirstAsync(n => n.Id == id &&
			context.NotificationsUsers.Any(nu => nu.NotificationId == n.Id && nu.UserId == userId));

	public async Task<List<Notification>> GetByUserId(int userId) =>
		await context.Notifications
			.Where(IsValidNotification(userId))
			.ToListAsync();

	private Expression<Func<Notification, bool>> IsValidNotification(int userId) =>
		n => n.ValidFrom <= DateTime.Now && DateTime.Now < n.ExpiredAt &&
			!context.NotificationsUsers.Any(nu => nu.NotificationId == n.Id && nu.UserId == userId);
}
