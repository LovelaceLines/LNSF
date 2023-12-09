using LNSF.Application.Interfaces;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application;

public class PeopleService : IPeopleService
{
    private readonly IPeopleRepository _repository;
    private readonly PeopleValidator _validator;

    public PeopleService(IPeopleRepository peopleRepository,
        PeopleValidator peopleValidator)
    {
        _repository = peopleRepository;
        _validator = peopleValidator;
    }

    public async Task<List<People>> Query(PeopleFilter filter) => 
        await _repository.Query(filter);
    
    public async Task<int> GetCount() =>
        await _repository.GetCount();

    public async Task<People> Create(People people)
    {
        var validationResult = _validator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _repository.Add(people);
    }

    public async Task<People> Update(People people)
    {
        var validationResult = _validator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _repository.ExistsById(people.Id)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.NotFound);

        return await _repository.Update(people);	
    }
}
