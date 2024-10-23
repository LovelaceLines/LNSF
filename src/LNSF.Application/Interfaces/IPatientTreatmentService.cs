using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IPatientTreatmentService : IBaseService<PatientTreatment>
{
	Task<PatientTreatment> Delete(PatientTreatment patientTreatment);
}
