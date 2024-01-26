using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class FamilyGroupProfileFilter
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Kinship { get; set; }
    public int? Age { get; set; }
    public string? Profession { get; set; }
    public double? Income { get; set; }
    public string? GlobalFilter { get; set; }
    public bool? GetPatient { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public FamilyGroupProfileFilter() { }

    public FamilyGroupProfileFilter(
        int? id = null,
        string? name = null,
        string? kinship = null,
        int? age = null,
        string? profession = null,
        double? income = null,
        string? globalFilter = null,
        bool? getPatient = null,
        Pagination? page = null,
        OrderBy? orderBy = null
    )
    {
        Id = id;
        Name = name;
        Kinship = kinship;
        Age = age;
        Profession = profession;
        Income = income;
        GlobalFilter = globalFilter;
        GetPatient = getPatient;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
