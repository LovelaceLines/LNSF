using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Test.DTOs;
using LNSF.Test.Fakers;
using LNSF.Test.Global;
using System.Net;

namespace LNSF.Test.API;

public class NotificationTestApi : GlobalClientRequest
{
	[Fact]
	public async Task Get_Notification_Ok()
	{
		var notifications = new List<Notification>();
		for (var i = 0; i < 5; i++) notifications.Add(await GetNotification());

		var result = await Get<List<Notification>>(_notificationClient);

		Assert.Contains(notifications, n => result.Any(r => r.Id == n.Id));
	}

	[Fact]
	public async Task Get_Notification_NoMarkAsRead_Ok()
	{
		var notification = await GetNotification();
		await PostFromBody<NotificationUser>(_notificationMarkAsReadClient, new NotificationUser { NotificationId = notification.Id });

		var result = await Get<List<Notification>>(_notificationClient);

		Assert.DoesNotContain(result, n => n.Id == notification.Id);
	}


	[Fact]
	public async Task Post_Notification_Ok()
	{
		var fake = new NotificationFake().Generate();

		var result = await PostFromBody<Notification>(_notificationClient, fake);

		Assert.Equal(fake.Title, result.Title);
		Assert.Equal(fake.Content, result.Content);
	}

	[Fact]
	public async Task Put_Notification_Ok()
	{
		var notification = await GetNotification();
		var fake = new NotificationFake(id: notification.Id).Generate();

		var result = await PutFromBody<Notification>(_notificationClient, fake);

		Assert.Equal(fake.Title, result.Title);
		Assert.Equal(fake.Content, result.Content);
	}

	[Fact]
	public async Task Delete_Notification_Ok()
	{
		var notification = await GetNotification();

		var result = await DeleteFromUri<Notification>(_notificationClient, notification.Id);

		Assert.Equal(notification.Id, result.Id);
	}
}
