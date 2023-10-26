using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository _repository;

    public HospitalService(IHospitalRepository repository) => 
        _repository = repository;

    public Task<List<Hospital>> Query(HospitalFilter filter) => 
        _repository.Query(filter);

    public async Task<int> GetCount() => 
        await _repository.GetCount();

    public async Task<Hospital> Create(Hospital hospital) => 
        await _repository.Add(hospital);

    public async Task<Hospital> Update(Hospital hospital) => 
        await _repository.Update(hospital);
}
