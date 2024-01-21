using LNSF.Domain.Entities;

namespace LNSF.Domain.DTOs;

public class PatientDTO : Patient
{
    public List<Treatment>? Treatments { get; set; }
}
