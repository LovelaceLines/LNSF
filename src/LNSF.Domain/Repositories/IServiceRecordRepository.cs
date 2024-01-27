using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IServiceRecordRepository : IBaseRepository<ServiceRecord>
{
    Task<List<ServiceRecord>> Query(ServiceRecordFilter filter);
    Task<bool> ExistsByPatientId(int patientId);
    Task<bool> ExistsById(int id, int patientId);
}
