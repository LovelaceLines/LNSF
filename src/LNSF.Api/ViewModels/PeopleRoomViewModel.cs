namespace LNSF.Api.ViewModels;

public class PeopleRoomHostingViewModel
{
    public int HostingId { get; set; }
    public HostingViewModel? Hosting { get; set; }
    public int PeopleId { get; set; }
    public PeopleViewModel? People { get; set; }
    public int RoomId { get; set; }
    public RoomViewModel? Room { get; set; }
}
