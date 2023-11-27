using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;
public interface IPatientRepository : IBaseRepository<Patient>
{
    Task<List<Patient>> Query(PatientFilter filter);
    Task<bool> ExistsByPeopleId(int peopleId);
    Task<bool> ExistsByIdAndPeopleId(int id, int peopleId);
    new Task<Patient> Add(Patient patient);
    new Task<Patient> Update(Patient patient);
}