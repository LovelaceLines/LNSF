﻿using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

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
        if (await _repository.Exists(hospital.Name)) throw new AppException("Nome do hospital já cadastrado!", HttpStatusCode.UnprocessableEntity);

        return await _repository.Add(hospital);
    }

    public async Task<Hospital> Update(Hospital hospital)
    {
        var validationResult = await _validator.ValidateAsync(hospital);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        if (!await _repository.Exists(hospital.Id)) throw new AppException("Hospital não encontrado!", HttpStatusCode.NotFound);
        var oldHospital = await _repository.Get(hospital.Id);
        if (oldHospital.Name != hospital.Name && await _repository.Exists(hospital.Name)) throw new AppException("Nome do hospital já cadastrado!", HttpStatusCode.UnprocessableEntity);
        
        return await _repository.Update(hospital);
    }
}
