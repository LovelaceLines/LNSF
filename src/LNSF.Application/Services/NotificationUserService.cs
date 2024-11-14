using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using System.Net;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class NotificationUserService(INotificationUserRepository repository,
	INotificationRepository notificationRepository,
	IUserRepository userRepository) : INotificationUserService
{
	public async Task<NotificationUser> Create(NotificationUser notificationUser)
	{
		if (!await notificationRepository.ExistsById(notificationUser.NotificationId)) throw new AppException("Notificação não encontrada", HttpStatusCode.NotFound);
		if (!await userRepository.ExistsById(notificationUser.UserId)) throw new AppException("Usuário não encontrado", HttpStatusCode.NotFound);
		if (await repository.ExistsByNotificationIdAndUserId(notificationUser.NotificationId, notificationUser.UserId)) throw new AppException("Notificação já lida", HttpStatusCode.BadRequest);

		return await repository.Add(notificationUser);
	}

	public async Task<NotificationUser> Delete(NotificationUser notificationUser)
	{
		if (!await repository.ExistsById(notificationUser.Id)) throw new AppException("Notificação não encontrada", HttpStatusCode.NotFound);
		return await repository.Remove(notificationUser);
	}
}
