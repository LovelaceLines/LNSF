using LNSF.Domain;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Application;

public class PeopleService
{
    private readonly IPeopleRepository _peopleRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly PaginationValidator _paginationValidator;
    private readonly PeopleValidator _peopleValidator;

    public PeopleService(IPeopleRepository peopleRepository,
        IRoomRepository roomRepository,
        PaginationValidator paginationValidator,
        PeopleValidator peopleValidator)
    {
        _peopleRepository = peopleRepository;
        _roomRepository = roomRepository;
        _paginationValidator = paginationValidator;
        _peopleValidator = peopleValidator;
    }

    public async Task<ResultDTO<List<People>>> Get(Pagination pagination)
    {
        var validationResult = _paginationValidator.Validate(pagination);

        return validationResult.IsValid ?
            await _peopleRepository.Get(pagination) :
            new ResultDTO<List<People>>(validationResult.ToString());
    }

    public async Task<ResultDTO<People>> Get(int id) => 
        await _peopleRepository.Get(id);
    
    public async Task<ResultDTO<int>> GetQuantity() =>
        await _peopleRepository.GetQuantity();

    public async Task<ResultDTO<People>> Post(People people)
    {
        /*
        // TODO verificar se existe RoomId
        if (people.RoomId == 0) people.RoomId = null;
        
        if (people.RoomId != null)
        {
            var room = await _roomRepository.Get(people.RoomId);

            if (room == null) return new ResultDTO<People>("Quarto não encontrado");
        }
        */

        var validationResult = _peopleValidator.Validate(people);

        return validationResult.IsValid ?
            await _peopleRepository.Post(people) :
            new ResultDTO<People>(validationResult.ToString());
    }

    public async Task<ResultDTO<People>> Put(People people)
    {
        var validationResult = _peopleValidator.Validate(people);

        return validationResult.IsValid ?
            await _peopleRepository.Put(people) :
            new ResultDTO<People>(validationResult.ToString());
    }

    public async Task<ResultDTO<People>> Put(int peopleId, int roomId) =>
        await _peopleRepository.Put(peopleId, roomId);
}
