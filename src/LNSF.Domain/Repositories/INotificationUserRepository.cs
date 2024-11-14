using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface INotificationUserRepository : IBaseRepository<NotificationUser>
{
	Task<bool> ExistsByNotificationIdAndUserId(int notificationId, int userId);
	Task<NotificationUser> GetByNotificationIdAndUserId(int notificationId, int userId);
}
