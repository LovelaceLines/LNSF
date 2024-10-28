using LNSF.Domain.Enums;

namespace LNSF.Domain.Entities;

public class Treatment : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public TypeTreatment Type { get; set; }
}
