namespace LNSF.Domain.Entities;

public class NotificationUser : BaseEntity
{
	public DateTime ReadAt { get; set; }
	public int NotificationId { get; set; }
	public Notification? Notification { get; set; }
	public int UserId { get; set; }
	public User? User { get; set; }
}
