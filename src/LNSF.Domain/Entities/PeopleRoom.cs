namespace LNSF.Domain.Entities;

public class PeopleRoom
{
    public int Id { get; set; }
    public int Occupation { get; set; }
    public int PeopleId { get; set; }
    public People? People { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
