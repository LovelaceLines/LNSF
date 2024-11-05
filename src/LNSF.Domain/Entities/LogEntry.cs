namespace LNSF.Domain.Entities;

public class LogEntry : BaseEntity
{
	public int UserId { get; set; }
	public User? User { get; set; }
	public string EntityName { get; set; } = string.Empty;
	public int EntityId { get; set; }
	public string Action { get; set; } = string.Empty;
	public string ValuesChanges { get; set; } = string.Empty;
	public DateTime LogDateTime { get; set; }
}
