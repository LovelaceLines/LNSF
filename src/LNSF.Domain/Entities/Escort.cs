namespace LNSF.Domain.Entities;
public class Escort 
{
    public int Id { get; set; }
    public int PeopleId { get; set; }
    public People? People { get; set; }
}
