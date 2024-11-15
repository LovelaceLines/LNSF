using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class NotificationService(INotificationRepository repository, NotificationValidator validator) : INotificationService
{
	public async Task<Notification> Create(Notification notification)
	{
		var validationResult = validator.Validate(notification);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		return await repository.Add(notification);
	}

	public async Task<Notification> Update(Notification newNotification)
	{
		var validationResult = validator.Validate(newNotification);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsById(newNotification.Id)) throw new AppException("Notificação não encontrada", HttpStatusCode.NotFound);

		var oldNotification = await repository.GetById(newNotification.Id);
		oldNotification.Title = newNotification.Title;
		oldNotification.Content = newNotification.Content;

		return await repository.Update(oldNotification);
	}

	public async Task<Notification> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Notificação não encontrada", HttpStatusCode.NotFound);
		var notification = await repository.GetById(id);
		return await repository.Remove(notification);
	}
}
