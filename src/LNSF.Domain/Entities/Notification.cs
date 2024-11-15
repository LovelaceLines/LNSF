namespace LNSF.Domain.Entities;

public class Notification : BaseEntity
{
	public required string Title { get; set; }
	public required string Content { get; set; }
	public DateTime ValidFrom { get; set; }
	public DateTime ExpiredAt { get; set; }
}
