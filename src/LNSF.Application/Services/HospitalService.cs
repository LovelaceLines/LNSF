using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository _repository;
    private readonly HospitalValidator _validator;

    public HospitalService(IHospitalRepository repository, 
        HospitalValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public Task<List<Hospital>> Query(HospitalFilter filter) => 
        _repository.Query(filter);

    public async Task<int> GetCount() => 
        await _repository.GetCount();

    public async Task<Hospital> Create(Hospital hospital)
    {
        var validationResult = await _validator.ValidateAsync(hospital);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (await _repository.ExistsByName(hospital.Name)) throw new AppException("Nome do hospital já cadastrado!", HttpStatusCode.Conflict);

        return await _repository.Add(hospital);
    }

    public async Task<Hospital> Update(Hospital newHospital)
    {
        var validationResult = await _validator.ValidateAsync(newHospital);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _repository.Exists(newHospital.Id)) throw new AppException("Hospital não encontrado!", HttpStatusCode.NotFound);
        var oldHospital = await _repository.Get(newHospital.Id);
        if (oldHospital.Name != newHospital.Name && await _repository.ExistsByName(newHospital.Name)) throw new AppException("Nome do hospital já cadastrado!", HttpStatusCode.Conflict);
        
        return await _repository.Update(newHospital);
    }

    public async Task<Hospital> Delete(int id)
    {
        if (!await _repository.Exists(id)) throw new AppException("Hospital não encontrado!", HttpStatusCode.NotFound);

        return await _repository.Remove(id);
    }
}
