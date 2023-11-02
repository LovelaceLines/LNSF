using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface IEscortRepository : IBaseRepository<Escort>
{
    Task<List<Escort>> Query (EscortFilter filter);
}
