using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPatientTreatmentService
{
    Task<List<PatientTreatment>> Query(PatientTreatmentFilter filter);
    Task<int> GetCount();
    Task<PatientTreatment> Create(PatientTreatment patientTreatment);
    Task<PatientTreatment> Delete(int patientId, int treatmentId);
}
