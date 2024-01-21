using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPatientService
{
    Task<List<PatientDTO>> Query(PatientFilter filter);
    Task<int> Count();
    Task<Patient> Create(Patient patient);
    Task<Patient> Update(Patient patient);
    Task<Patient> Delete(int Id);
}
