using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class NotificationUserRepository(AppDbContext context) : BaseRepository<NotificationUser>(context), INotificationUserRepository
{
	public async Task<bool> ExistsByNotificationIdAndUserId(int notificationId, int userId) =>
		await context.NotificationsUsers.AnyAsync(nu => nu.NotificationId == notificationId && nu.UserId == userId);

	public async Task<NotificationUser> GetByNotificationIdAndUserId(int notificationId, int userId) =>
		await context.NotificationsUsers.FirstAsync(nu => nu.NotificationId == notificationId && nu.UserId == userId);
}
