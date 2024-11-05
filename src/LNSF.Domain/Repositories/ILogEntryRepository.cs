using LNSF.Domain.Filters;
using LNSF.Domain.Entities;

namespace LNSF.Domain.Repositories;

public interface ILogEntryRepository : IBaseRepository<Room>
{
	Task<QueryResult<LogEntry>> Query(LogEntryFilter filter);
}
