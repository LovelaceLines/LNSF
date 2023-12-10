using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPatientTreatmentService
{
    Task<List<PatientTreatment>> Query(PatientTreatmentFilter filter);
    Task<int> GetCount();
}
