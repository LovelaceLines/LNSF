using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IPatientTreatmentRepository : IBaseRepository<PatientTreatment>
{
    Task<List<PatientTreatment>> GetByPatientId(int patientId);
    Task<List<PatientTreatment>> GetByTreatmentId(int treatmentId);
    Task<PatientTreatment> GetByPatientIdAndTreatmentId(int patientId, int treatmentId);
    Task<bool> ExistsByPatientIdAndTreatmentId(int patientId, int treatmentId);
    Task<List<PatientTreatment>> RemoveByPatientId(int patientId);
    Task<List<PatientTreatment>> RemoveByTreatmentId(int treatmentId);
}
