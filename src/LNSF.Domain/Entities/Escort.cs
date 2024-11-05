namespace LNSF.Domain.Entities;

public class Escort : BaseEntity
{
	public int PeopleId { get; set; }
	public People? People { get; set; }
}
