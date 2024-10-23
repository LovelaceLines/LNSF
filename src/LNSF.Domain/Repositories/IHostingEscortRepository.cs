using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHostingEscortRepository : IBaseRepository<HostingEscort>
{
    Task<QueryResult<HostingEscort>> Query(BaseFilter filter);
    Task<HostingEscort> GetByHostingIdAndEscortId(int hostingId, int escortId);
    Task<bool> ExistsByHostingIdAndEscortId(int hostingId, int escortId);
    Task<bool> ExistsWithDateConflict(int hostingId, int escortId);
}
