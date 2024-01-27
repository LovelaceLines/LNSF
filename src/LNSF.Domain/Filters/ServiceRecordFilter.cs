using LNSF.Domain.Enums;

namespace LNSF.Domain.Filters;

public class ServiceRecordFilter
{
    public int? Id { get; set; }
    public int? PatientId { get; set; }
    public string? GlobalFilter { get; set; }

    public bool? GetPatient { get; set; }

    public Pagination Page { get; set; } = new();
    public OrderBy? OrderBy { get; set; }

    public ServiceRecordFilter() { }

    public ServiceRecordFilter(
        int? id = null,
        int? patientId = null,
        string? globalFilter = null,
        bool? getPatient = null,
        Pagination? page = null,
        OrderBy? orderBy = null)
    {
        Id = id;
        PatientId = patientId;
        GlobalFilter = globalFilter;
        GetPatient = getPatient;
        Page = page ?? new();
        OrderBy = orderBy;
    }
}
