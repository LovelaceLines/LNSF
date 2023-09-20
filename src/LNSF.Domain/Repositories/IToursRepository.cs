using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface IToursRepository : IBaseRepository<Tour>
{
    public Task<List<Tour>> Get(Pagination pagination);
}
