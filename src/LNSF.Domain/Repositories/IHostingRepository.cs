using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IHostingRepository : IBaseRepository<Hosting>
{
    Task<List<Hosting>> Query(HostingFilter filter);
    new Task<Hosting> Add(Hosting hosting);
    new Task<Hosting> Update(Hosting hosting);
}
