namespace LNSF.Domain.Entities;

public class PeopleRoom
{
    public int HostingId { get; set; }
    public Hosting? Hosting { get; set; }
    public int PeopleId { get; set; }
    public People? People { get; set; }
    public int RoomId { get; set; }
    public Room? Room { get; set; }
}
