namespace LNSF.Domain.Entities;

public class Tour : BaseEntity
{
	public DateTime Output { get; set; }
	public DateTime? Input { get; set; }
	public required string Note { get; set; }

	public int PeopleId { get; set; }
	public People? People { get; set; }
}
