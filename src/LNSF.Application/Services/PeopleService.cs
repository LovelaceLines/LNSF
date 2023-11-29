using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using System.Net;
using LNSF.Application.Interfaces;

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

    public async Task<People> Get(int id) => 
        await _repository.Get(id);
    
    public async Task<int> GetCount() =>
        await _repository.GetCount();

    public async Task<People> Create(People people)
    {
        var validationResult = _validator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _repository.Add(people);
    }

    public async Task<People> Update(People newPeople)
    {
        var validationResult = _validator.Validate(newPeople);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        
        if (!await _repository.Exists(newPeople.Id)) throw new AppException("Pessoa não encontrada!", HttpStatusCode.UnprocessableEntity);

        var oldPeople = await _repository.Get(newPeople.Id);

        return await _repository.Update(newPeople);	
    }
}
