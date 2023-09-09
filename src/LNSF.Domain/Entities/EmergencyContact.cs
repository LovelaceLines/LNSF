namespace LNSF.Domain.Entities;

public class EmergencyContact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    
    public int PeopleId { get; set; }
    public People? People { get; set; }
}
