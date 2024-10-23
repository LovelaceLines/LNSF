using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;

namespace LNSF.Domain.Repositories;

public interface ITreatmentRepository : IBaseRepository<Treatment>
{
	Task<QueryResult<Treatment>> Query(TreatmentFilter filter);
	Task<bool> ExistsByName(string name);
	Task<bool> ExistsByNameAndType(string name, TypeTreatment type);
	Task<List<Treatment>> GetByPatientId(int patientId);
}

