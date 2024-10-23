using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IServiceRecordRepository : IBaseRepository<ServiceRecord>
{
    Task<QueryResult<ServiceRecord>> Query(BaseFilter filter);
    Task<bool> ExistsByPatientId(int patientId);
    Task<bool> ExistsById(int id, int patientId);
}
