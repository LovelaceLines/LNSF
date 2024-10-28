using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface IChainRepository
{
	Task<int> QueryCountPeopleHosted(ChainCountPeopleHostedFilter filter);
	Task<QueryResult<People>> QueryPeopleWillHosted(ChainDayFilter filter);
	Task<List<PeopleRoomHosting>> QueryPeopleWillBirthday(ChainDayFilter filter);
	Task<List<TreatmentDTO>> QueryCountTypeTreatment(ChainIntervalCheckFilter filter);
}
