using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHostingEscortRepository : IBaseRepository<HostingEscort>
{
    Task<List<HostingEscort>> Query(HostingEscortFilter filter);
    Task<HostingEscort> GetByHostingIdAndEscortId(int hostingId, int escortId);
    Task<bool> ExistsByHostingIdAndEscortId(int hostingId, int escortId);
    Task<bool> ExistsByEscortIdAndCheckInAndCheckOut(int hostingId, int escortId);
}
