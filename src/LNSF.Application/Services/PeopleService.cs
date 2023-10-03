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

    public async Task<List<People>> Query(PeopleFilters filters)
    {
        var validationResult = _peopleFiltersValidator.Validate(filters);

        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        return await _peopleRepository.Query(filters);
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
        room.Occupation++;
        if (room.Beds == room.Occupation) room.Available = false;
        
        // await _peopleRepository.BeguinTransaction();

        try
        {
            people = await _peopleRepository.Put(people);
            await _roomRepository.Put(room);
        }
        catch (Exception)
        {
            // await _peopleRepository.RollbackTransaction();
            throw new AppException("Erro ao adicionar pessoa ao quarto.");
        }

        // await _peopleRepository.CommitTransaction();

        return people;
    }

    public async Task<People> RemovePeopleFromRoom(int peopleId)
    {
        var people = await _peopleRepository.Get(peopleId);

        if (people.RoomId == null) throw new AppException("Pessoa não está no quarto.");

        var room = await _roomRepository.Get(people.RoomId.Value);

        // RemovePeopleFromRoom
        people.RoomId = null;
        people.Room = null;
        room.Occupation--;
        room.Available = true;

        // await _peopleRepository.BeguinTransaction();

        try
        {
            people = await _peopleRepository.Put(people);
            await _roomRepository.Put(room);
        }
        catch (Exception)
        {
            // await _peopleRepository.RollbackTransaction();
            throw new AppException("Erro ao remover pessoa do quarto.");
        }

        // await _peopleRepository.CommitTransaction();

        return people;
    }
}
