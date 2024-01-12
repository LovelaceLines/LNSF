using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class TourFilter
{
    public int? Id { get; set; }
    public DateTime? Output { get; set; }
    public DateTime? Input { get; set; }
    public string? Note { get; set; }
    public bool? InOpen { get; set; }
    public int? PeopleId { get; set; }
    public bool? GetPeople { get; set; }
    public string? PeopleName { get; set; }
    public string? PeopleRG { get; set; }
    public string? PeopleCPF { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public TourFilter() { }
    
    public TourFilter(int? id = null,   
        DateTime? output = null, 
        DateTime? input = null, 
        string? note = null, 
        bool? inOpen = null, 
        int? peopleId = null,
        bool? getPeople = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        Output = output;
        Input = input;
        Note = note;
        InOpen = inOpen;
        PeopleId = peopleId;
        GetPeople = getPeople;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
