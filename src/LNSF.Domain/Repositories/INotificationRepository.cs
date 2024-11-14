using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface INotificationRepository : IBaseRepository<Notification>
{
	Task<List<Notification>> GetByUserId(int userId);
	Task<Notification> GetByIdAndUserId(int id, int userId);
	Task<bool> ExistsByIdAndUserId(int id, int userId);
	Task<int> CountUnreadByUserId(int userId);
}
