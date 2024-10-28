namespace LNSF.Domain.Entities;

public class EmergencyContact : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }

    public int PeopleId { get; set; }
    public People? People { get; set; }
}
