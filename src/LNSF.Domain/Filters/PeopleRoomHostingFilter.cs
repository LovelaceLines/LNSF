using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PeopleRoomHostingFilter
{
    public int? PeopleId { get; set; }
    public int? RoomId { get; set; }
    public int? HostingId { get; set; }
    public int? Vacancy { get; set; }
    public bool? HasVacancy { get; set; }
    public bool? Available { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public bool? Active { get; set; }
    public bool? GetPeople { get; set; }
    public bool? GetRoom { get; set; }
    public bool? GetHosting { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public PeopleRoomHostingFilter() { }

    public PeopleRoomHostingFilter(
        int? peopleId = null,
        int? roomId = null,
        int? hostingId = null,
        int? vacancy = null,
        bool? hasVacancy = null,
        bool? available = null,
        DateTime? checkIn = null,
        DateTime? checkOut = null,
        bool? active = null,
        bool? getPeople = null,
        bool? getRoom = null,
        bool? getHosting = null,
        string? globalFilter = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        PeopleId = peopleId;
        RoomId = roomId;
        HostingId = hostingId;
        Vacancy = vacancy;
        HasVacancy = hasVacancy;
        Available = available;
        CheckIn = checkIn;
        CheckOut = checkOut;
        Active = active;
        GetPeople = getPeople;
        GetRoom = getRoom;
        GetHosting = getHosting;
        GlobalFilter = globalFilter;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
