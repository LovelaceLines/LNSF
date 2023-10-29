using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
namespace LNSF.Domain.Repositories;
public interface IPatientRepository : IBaseRepository<Patient>
{
    public Task<List<Patient>> Query(PatientFilter filter);
}