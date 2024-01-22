using LNSF.Domain.Enums;
namespace LNSF.Domain.Filters;
public class EscortFilter
{
    public int? Id { get; set; }
    public int? PeopleId { get; set; }
    public bool? GetPeople { get; set; }
    public bool? Active { get; set; }
    public bool? IsVeteran { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public EscortFilter() { }

    public EscortFilter(int? id = null,
        int? peopleId = null,
        bool? active = null,
        bool? isVeteran = null,
        bool? getPeople = null,
        string? globalFilter = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        PeopleId = peopleId;
        Active = active;
        IsVeteran = isVeteran;
        GetPeople = getPeople;
        GlobalFilter = globalFilter;
        Page = page ?? Page;
        OrderBy = orderBy;
    }
}