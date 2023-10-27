using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IPatientService
{
    Task<Patient> Create(Patient patient);
    Task<Patient> Update(Patient patient);
    Task<int> Count();
}
