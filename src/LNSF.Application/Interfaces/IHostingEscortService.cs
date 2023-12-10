using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IHostingEscortService
{
    Task<List<HostingEscort>> Query(HostingEscortFilter filter);
    Task<int> GetCount();
}
