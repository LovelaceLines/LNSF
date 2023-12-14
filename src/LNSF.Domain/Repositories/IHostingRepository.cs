using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHostingRepository : IBaseRepository<Hosting>
{
    Task<List<Hosting>> Query(HostingFilter filter);
    Task<bool> ExistsByIdAndPatientId(int id, int patientId);
    Task<bool> ExistsByIdAndPeopleId(int id, int peopleId);
    Task<bool> ExistsWithDateConflict(Hosting hosting);
}
