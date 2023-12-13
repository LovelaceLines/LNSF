using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PeopleFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? Phone { get; set; }
    public bool? Patient { get; set; }
    public bool? Escort { get; set; }
    public bool? Active { get; set; }
    public string? GlobalFilter { get; set; }
    
    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public PeopleFilter() { }

    public PeopleFilter(int? id = null,
        string? name = null,
        string? rg = null,
        string? cpf = null,
        string? phone = null,
        bool? patient = null,
        bool? escort = null,
        bool? active = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        Name = name;
        RG = rg;
        CPF = cpf;
        Phone = phone;
        Patient = patient;
        Escort = escort;
        Active = active;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
