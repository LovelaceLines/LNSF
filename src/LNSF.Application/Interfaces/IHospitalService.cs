using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IHospitalService
{
    public Task<List<Hospital>> Query(HospitalFilter filter);
    public Task<int> GetCount();
    public Task<Hospital> Create(Hospital hospital);
    public Task<Hospital> Update(Hospital hospital);
    public Task<Hospital> Delete(int id);
}
