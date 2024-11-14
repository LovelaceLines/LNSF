using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class NotificationRepository(AppDbContext context) : BaseRepository<Notification>(context), INotificationRepository
{
	public async Task<int> CountUnreadByUserId(int userId)
	{
		var notificationsCountTask = context.Notifications.CountAsync();
		var readNotificationsCountTask = context.NotificationsUsers.CountAsync(nu => nu.UserId == userId);

		await Task.WhenAll(notificationsCountTask, readNotificationsCountTask);

		return notificationsCountTask.Result - readNotificationsCountTask.Result;
	}

	public async Task<bool> ExistsByIdAndUserId(int id, int userId) =>
		await context.NotificationsUsers.AnyAsync(nu => nu.NotificationId == id && nu.UserId == userId);

	public async Task<Notification> GetByIdAndUserId(int id, int userId) =>
		await context.Notifications.FirstAsync(n => n.Id == id &&
			context.NotificationsUsers.Any(nu => nu.NotificationId == n.Id && nu.UserId == userId));

	public async Task<List<Notification>> GetByUserId(int userId) =>
		await context.Notifications
			.Where(n => !context.NotificationsUsers.Any(nu => nu.NotificationId == n.Id && nu.UserId == userId))
			.ToListAsync();
}
