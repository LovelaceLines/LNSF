using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
namespace LNSF.Application.Interfaces;

public interface ITreatmentService
{
    Task<List<Treatment>> Query(TreatmentFilter filter);
    Task<Treatment> Create(Treatment Treatment);
    Task<Treatment> Update(Treatment treatment);
    Task<int> GetCount();
}
