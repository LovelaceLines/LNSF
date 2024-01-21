using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class PatientFilter
{
    public int? Id { get; set; }
    public int? PeopleId { get; set; }
    public int? HospitalId { get; set; }
    public bool? SocioEconomicRecord { get; set; }
    public bool? Term { get; set; }
    public int? TreatmentId { get; set; }
    public bool? Active { get; set; }
    public bool? IsVeteran { get; set; }
    public bool? GetPeople { get; set; }
    public bool? GetHospital { get; set; }
    public bool? GetTreatments { get; set; }
    public string? GlobalFilter { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public PatientFilter() { }

    public PatientFilter(int? id = null,
        int? peopleId = null,
        int? hospitalId = null,
        bool? socioEconomicRecord = null,
        bool? term = null,
        int? treatmentId = null,
        bool? active = null,
        bool? isVeteran = null,
        bool? getPeople = null,
        bool? getHospital = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        PeopleId = peopleId;
        HospitalId = hospitalId;
        SocioEconomicRecord = socioEconomicRecord;
        Term = term;
        TreatmentId = treatmentId;
        Active = active;
        IsVeteran = isVeteran;
        GetPeople = getPeople;
        GetHospital = getHospital;
        Page = page ?? Page;
        OrderBy = orderBy;
    }
}