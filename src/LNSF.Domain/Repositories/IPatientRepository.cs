using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;
public interface IPatientRepository : IBaseRepository<Patient>
{
    public Task<List<Patient>> Query(PatientFilter filter);
    public Task<bool> PeopleExists(int peopleId);
    public new Task<Patient> Add(Patient patient);
    public new Task<Patient> Update(Patient patient);
}