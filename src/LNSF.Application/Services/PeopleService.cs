using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using System.Net;

namespace LNSF.Application;

public class PeopleService
{
    private readonly IPeoplesRepository _peopleRepository;
    private readonly IRoomsRepository _roomRepository;
    private readonly PeopleFilterValidator _PeopleFilterValidator;
    private readonly PeopleValidator _peopleValidator;

    public PeopleService(IPeoplesRepository peopleRepository,
        IRoomsRepository roomRepository,
        PeopleFilterValidator PeopleFilterValidator,
        PeopleValidator peopleValidator)
    {
        _peopleRepository = peopleRepository;
        _roomRepository = roomRepository;
        _PeopleFilterValidator = PeopleFilterValidator;
        _peopleValidator = peopleValidator;
    }

    public async Task<List<People>> Query(PeopleFilter filter)
    {
        var validationResult = _PeopleFilterValidator.Validate(filter);

        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _peopleRepository.Query(filter);
    }

    public async Task<People> Get(int id) => 
        await _peopleRepository.Get(id);
    
    public async Task<int> GetQuantity() =>
        await _peopleRepository.GetQuantity();

    public async Task<People> CreateNewPeople(People people)
    {
        var validationResult = _peopleValidator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _peopleRepository.Post(people);
    }

    public async Task<People> EditBasicInformation(People people)
    {
        var validationResult = _peopleValidator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        var oldPeople = await _peopleRepository.Get(people.Id);

        people.RoomId = oldPeople.RoomId; //Quarto não pode ser alterado.
        people.Room = oldPeople.Room;

        return await _peopleRepository.Put(people);	
    }

    public async Task<People> AddPeopleToRoom(int peopleId, int roomId)
    {
        var people = await _peopleRepository.Get(peopleId);
        var room = await _roomRepository.Get(roomId);

        if (people.RoomId == roomId) throw new AppException("Pessoa já está no quarto.", HttpStatusCode.UnprocessableEntity);
        if (people.RoomId != null) throw new AppException("Pessoa já está em um quarto.", HttpStatusCode.UnprocessableEntity);
        if (room.Beds - room.Occupation <= 0) throw new AppException("Não há vagas.", HttpStatusCode.UnprocessableEntity);
        if (!room.Available) throw new AppException("Quarto indisponível.", HttpStatusCode.UnprocessableEntity);

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
            throw new AppException("Erro ao adicionar pessoa ao quarto.", HttpStatusCode.InternalServerError);
        }

        // await _peopleRepository.CommitTransaction();

        return people;
    }

    public async Task<People> RemovePeopleFromRoom(int peopleId)
    {
        var people = await _peopleRepository.Get(peopleId);

        if (people.RoomId == null) throw new AppException("Pessoa não está no quarto.", HttpStatusCode.UnprocessableEntity);

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
            throw new AppException("Erro ao remover pessoa do quarto.", HttpStatusCode.InternalServerError);
        }

        // await _peopleRepository.CommitTransaction();

        return people;
    }
}
