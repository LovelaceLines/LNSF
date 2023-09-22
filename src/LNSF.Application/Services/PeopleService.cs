using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;

namespace LNSF.Application;

public class PeopleService
{
    private readonly IPeoplesRepository _peopleRepository;
    private readonly IRoomsRepository _roomRepository;
    private readonly PeopleFiltersValidator _peopleFiltersValidator;
    private readonly PeopleValidator _peopleValidator;

    public PeopleService(IPeoplesRepository peopleRepository,
        IRoomsRepository roomRepository,
        PeopleFiltersValidator peopleFiltersValidator,
        PeopleValidator peopleValidator)
    {
        _peopleRepository = peopleRepository;
        _roomRepository = roomRepository;
        _peopleFiltersValidator = peopleFiltersValidator;
        _peopleValidator = peopleValidator;
    }

    public async Task<List<People>> Get(PeopleFilters filters)
    {
        var validationResult = _peopleFiltersValidator.Validate(filters);

        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _peopleRepository.Get(filters);
    }

    public async Task<People> Get(int id) => 
        await _peopleRepository.Get(id);
    
    public async Task<int> GetQuantity() =>
        await _peopleRepository.GetQuantity();

    public async Task<People> CreateNewPeople(People people)
    {
        var validationResult = _peopleValidator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _peopleRepository.Post(people);
    }

    public async Task<People> EditBasicInformation(People people)
    {
        var validationResult = _peopleValidator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        var oldPeople = await _peopleRepository.Get(people.Id);

        people.RoomId = oldPeople.RoomId; //Quarto não pode ser alterado.
        people.Room = oldPeople.Room;

        return await _peopleRepository.Put(people);	
    }

    public async Task<People> AddPeopleToRoom(int peopleId, int roomId)
    {
        var people = await _peopleRepository.Get(peopleId);
        var room = await _roomRepository.Get(roomId);

        if (people.RoomId == roomId) throw new AppException("Pessoa já está no quarto.");
        if (people.RoomId != null) throw new AppException("Pessoa já está em um quarto.");
        if (room.Beds - room.Occupation <= 0) throw new AppException("Não há vagas.");
        if (!room.Available) throw new AppException("Quarto indisponível.");

        // AddPeopleToRoom
        people.RoomId = roomId;
        people.Room = room;
        room.Occupation++;

        return await _peopleRepository.Put(people);
    }

    public async Task<People> RemovePeopleFromRoom(int peopleId)
    {
        var people = await _peopleRepository.Get(peopleId);

        if (people.RoomId == null) throw new AppException("Pessoa não está no quarto.");

        var room = await _roomRepository.Get(people.RoomId.Value);

        // RemovePeopleFromRoom
        people.RoomId = null;
        room.Occupation--;

        // TEST

        people.Room = room;

        return await _peopleRepository.Put(people);
    }
}
