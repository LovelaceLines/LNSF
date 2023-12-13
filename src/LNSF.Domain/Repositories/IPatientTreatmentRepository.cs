using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IPatientTreatmentRepository : IBaseRepository<PatientTreatment>
{
    Task<List<PatientTreatment>> Query(PatientTreatmentFilter filter);
    Task<PatientTreatment> GetByPatientIdAndTreatmentId(int patientId, int treatmentId);
    Task<bool> ExistsByPatientIdAndTreatmentId(int patientId, int treatmentId);
}
