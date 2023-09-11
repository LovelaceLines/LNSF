using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Application;

public class PeopleService
{
    private readonly IPeoplesRepository _peopleRepository;
    private readonly IRoomsRepository _roomRepository;
    private readonly PaginationValidator _paginationValidator;
    private readonly PeopleValidator _peopleValidator;

    public PeopleService(IPeoplesRepository peopleRepository,
        IRoomsRepository roomRepository,
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
        if (people.Id != 0) people.Id = 0;
        
        if (people.RoomId.HasValue && people.RoomId != 0)
        {
            var resultRoom = await _roomRepository.Get(id: people.RoomId.Value);

            if (resultRoom.Error == true) return new ResultDTO<People>("Quarto não encontrado.");

            var room = resultRoom.Data;
            if (room.Beds - room.Occupation <= 0) return new ResultDTO<People>("Não há vagas.");
        }

        var validationResult = _peopleValidator.Validate(people);

        return validationResult.IsValid ?
            await _peopleRepository.Post(people) :
            new ResultDTO<People>(validationResult.ToString());
    }

    public async Task<ResultDTO<People>> Put(People people)
    {
        if (people.Id == 0) return new ResultDTO<People>("Pessoa não encontrada");
        if (people.RoomId == 0) return new ResultDTO<People>("Quarto não encontrado");

        var validationResult = _peopleValidator.Validate(people);

        return validationResult.IsValid ?
            await _peopleRepository.Put(people) :
            new ResultDTO<People>(validationResult.ToString());
    }

    public async Task<ResultDTO<People>> AddPeopleToRoom(int peopleId, int roomId)
    {
        var resultPeople = await _peopleRepository.Get(peopleId);
        if (resultPeople.Error == true) return new ResultDTO<People>("Pessoa não encontrada.");
        var people = resultPeople.Data;

        var resultRoom = await _roomRepository.Get(roomId);
        if (resultRoom.Error == true) return new ResultDTO<People>("Quarto não encontrado.");
        var room = resultRoom.Data;

        if (room.Beds - room.Occupation <= 0) return new ResultDTO<People>("Não há vagas.");
        if (!room.Available) return new ResultDTO<People>("Quarto indisponível.");
        if (people.RoomId != 0) return new ResultDTO<People>("Pessoa já está em um quarto.");
        if (people.RoomId == roomId) return new ResultDTO<People>("Pessoa já está no quarto.");

        //AddPeopleToRoom
        people.RoomId = roomId;
        room.Occupation++;

        if ((await _roomRepository.Put(room)).Error == true)
            return new ResultDTO<People>("Erro ao atualizar quarto.");
        
        resultPeople = await _peopleRepository.Put(people);
        return resultPeople.Error ? 
            new ResultDTO<People>("Erro ao atualizar pessoa.") :
            new ResultDTO<People>(resultPeople.Data);
    }

    public async Task<ResultDTO<People>> RemovePeopleFromRoom(int peopleId, int roomId)
    {
        var resultPeople = await _peopleRepository.Get(peopleId);
        if (resultPeople.Error == true) return new ResultDTO<People>("Pessoa não encontrada.");
        var people = resultPeople.Data;

        var resultRoom = await _roomRepository.Get(roomId);
        if (resultRoom.Error == true) return new ResultDTO<People>("Quarto não encontrado.");
        var room = resultRoom.Data;

        if (people.RoomId != roomId) return new ResultDTO<People>("Pessoa não está no quarto.");

        //RemovePeopleFromRoom
        people.RoomId = null;
        room.Occupation--;

        if ((await _roomRepository.Put(room)).Error == true)
            return new ResultDTO<People>("Erro ao atualizar quarto.");
        
        resultPeople = await _peopleRepository.Put(people);
        return resultPeople.Error ? 
            new ResultDTO<People>("Erro ao atualizar pessoa.") :
            new ResultDTO<People>(resultPeople.Data);
    }
}
