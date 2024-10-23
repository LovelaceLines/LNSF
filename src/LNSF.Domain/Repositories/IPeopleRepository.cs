using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IPeopleRepository : IBaseRepository<People>
{
	Task<QueryResult<PeopleDTO>> Query(PeopleFilter filter);
	Task<bool> ExistsByCpf(string cpf);
	Task<bool> ExistsByRg(string rg);
	Task<bool> ExistsByPhone(string phone);
	Task<bool> ExistsByEmail(string email);
}
