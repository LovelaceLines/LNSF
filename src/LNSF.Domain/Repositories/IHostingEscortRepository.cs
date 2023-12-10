using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHostingEscortRepository : IBaseRepository<HostingEscort>
{
    Task<List<HostingEscort>> Query(HostingEscortFilter filter);
    Task<List<HostingEscort>> GetByHostingId(int hostingId);
    Task<List<HostingEscort>> GetByEscortId(int escortId);
    Task<HostingEscort> GetByHostingIdAndEscortId(int hostingId, int escortId);
    Task<bool> ExistsByHostingIdAndEscortId(int hostingId, int escortId);
    Task<List<HostingEscort>> RemoveByHostingId(int hostingId);
    Task<List<HostingEscort>> RemoveByEscortId(int escortId);
}
