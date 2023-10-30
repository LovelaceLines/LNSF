using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPatientService
{
    Task<List<Patient>> Query(PatientFilter filter);
    Task<int> Count();
    Task<Patient> Create(Patient patient);
    Task<Patient> Update(Patient patient);
}
