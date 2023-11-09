using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface ITreatmentService
{
    Task<List<Treatment>> Query(TreatmentFilter filter);
    Task<int> GetCount();
    Task<Treatment> Create(Treatment Treatment);
    Task<Treatment> Update(Treatment treatment);
    Task<Treatment> Delete(int id);
}
