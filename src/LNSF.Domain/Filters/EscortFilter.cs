using LNSF.Domain.Enums;
namespace LNSF.Domain.Filters;
public class EscortFilter
{
    public int? Id { get; set; }
    public int? PeopleId { get; set; }
    public bool? Active { get; set; }
    public bool? IsVeteran { get; set; }
    
    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public EscortFilter() { }
    
    public EscortFilter(int? id = null, 
        int? peopleId = null, 
        bool? active = null, 
        bool? isVeteran = null, 
        Pagination? page = null, 
        OrderBy? orderBy = null)
    {
        Id = id;
        PeopleId = peopleId;
        Active = active;
        IsVeteran = isVeteran;
        Page = page ?? Page;
        OrderBy = orderBy;
    }
}