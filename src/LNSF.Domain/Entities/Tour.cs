namespace LNSF.Domain.Entities;

public class Tour
{
    public int? Id { get; set; }
    public DateTime Output { get; set; }
    public DateTime? Input { get; set; }
    public string? Note { get; set; }

    public int? PeopleId { get; set; }
    public People? People { get; set; }
}
