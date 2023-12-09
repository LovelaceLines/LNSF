using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IHospitalService
{
    Task<List<Hospital>> Query(HospitalFilter filter);
    Task<int> GetCount();
    Task<Hospital> Create(Hospital hospital);
    Task<Hospital> Update(Hospital hospital);
    Task<Hospital> Delete(int id);
}
