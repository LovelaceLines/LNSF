﻿using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;

namespace LNSF.Application;

public class PeopleService
{
    private readonly AppDbContext _context;
    private readonly IPeoplesRepository _peopleRepository;
    private readonly IRoomsRepository _roomRepository;
    private readonly PeopleFiltersValidator _peopleFiltersValidator;
    private readonly PeopleValidator _peopleValidator;

    public PeopleService(AppDbContext context,
        IPeoplesRepository peopleRepository,
        IRoomsRepository roomRepository,
        PeopleFiltersValidator peopleFiltersValidator,
        PeopleValidator peopleValidator)
    {
        _context = context;
        _peopleRepository = peopleRepository;
        _roomRepository = roomRepository;
        _peopleFiltersValidator = peopleFiltersValidator;
        _peopleValidator = peopleValidator;
    }

    public async Task<ResultDTO<List<People>>> Get(PeopleFilters filters)
    {
        var validationResult = _peopleFiltersValidator.Validate(filters);

        return validationResult.IsValid ?
            await _peopleRepository.Get(filters) :
            new ResultDTO<List<People>>(validationResult.ToString());
    }

    public async Task<ResultDTO<People>> Get(int id) => 
        await _peopleRepository.Get(id);
    
    public async Task<ResultDTO<int>> GetQuantity() =>
        await _peopleRepository.GetQuantity();

    public async Task<ResultDTO<People>> CreateNewPeople(People people)
    {
        var validationResult = _peopleValidator.Validate(people);
        if (!validationResult.IsValid) return new ResultDTO<People>(validationResult.ToString());

        var transaction = await _context.Database.BeginTransactionAsync();

        var resultPeople = await _peopleRepository.Post(people);
        if (resultPeople.Error == true)
        {
            await transaction.RollbackAsync();
            return new ResultDTO<People>("Erro ao criar pessoa.");
        }

        await transaction.CommitAsync();
        return resultPeople;
    }

    public async Task<ResultDTO<People>> EditBasicInformation(People people)
    {
        var validationResult = _peopleValidator.Validate(people);
        if (!validationResult.IsValid) return new ResultDTO<People>(validationResult.ToString());

        var resultPeople = await _peopleRepository.Get(people.Id);
        if (resultPeople.Error == true) return new ResultDTO<People>("Pessoa não encontrada.");

        people.RoomId = resultPeople.Data.RoomId; //Quarto não pode ser alterado.

        var transaction = await _context.Database.BeginTransactionAsync();

        resultPeople = await _peopleRepository.Put(people);
        if (resultPeople.Error == true)
        {
            await transaction.RollbackAsync();
            return new ResultDTO<People>("Erro ao editar pessoa.");
        }

        await transaction.CommitAsync();
        return resultPeople;	
    }

    public async Task<ResultDTO<People>> AddPeopleToRoom(int peopleId, int roomId)
    {
        var resultPeople = await _peopleRepository.Get(peopleId);
        if (resultPeople.Error == true) return new ResultDTO<People>("Pessoa não encontrada.");

        var resultRoom = await _roomRepository.Get(roomId);
        if (resultRoom.Error == true) return new ResultDTO<People>("Quarto não encontrado.");
        
        var people = resultPeople.Data;
        var room = resultRoom.Data;

        if (people.RoomId == roomId) return new ResultDTO<People>("Pessoa já está no quarto.");
        if (people.RoomId != 0 && people.RoomId != null) return new ResultDTO<People>("Pessoa já está em um quarto.");
        if (room.Beds - room.Occupation <= 0) return new ResultDTO<People>("Não há vagas.");
        if (!room.Available) return new ResultDTO<People>("Quarto indisponível.");

        // AddPeopleToRoom
        people.RoomId = roomId;
        room.Occupation++;

        people.Room = room;

        resultPeople = await _peopleRepository.Put(people);

        return resultPeople.Error == true ? 
            new ResultDTO<People>("Erro ao adicionar pessoa ao quarto.") : 
            resultPeople;

        /*
        var transaction = await _context.Database.BeginTransactionAsync();

        resultPeople = await _peopleRepository.Put(people);
        resultRoom = await _roomRepository.Put(room);

        if (resultPeople.Error == true || resultRoom.Error == true)
        {
            await transaction.RollbackAsync();
            return new ResultDTO<People>("Erro ao adicionar pessoa ao quarto.");
        }

        await transaction.CommitAsync();

        return resultPeople;
        */
    }

    public async Task<ResultDTO<People>> RemovePeopleFromRoom(int peopleId, int roomId)
    {
        var resultPeople = await _peopleRepository.Get(peopleId);
        if (resultPeople.Error == true) return new ResultDTO<People>("Pessoa não encontrada.");

        var resultRoom = await _roomRepository.Get(roomId);
        if (resultRoom.Error == true) return new ResultDTO<People>("Quarto não encontrado.");
        
        var people = resultPeople.Data;
        var room = resultRoom.Data;

        if (people.RoomId != roomId) return new ResultDTO<People>("Pessoa não está no quarto.");

        // RemovePeopleFromRoom
        people.RoomId = null;
        room.Occupation--;

        people.Room = room;

        resultPeople = await _peopleRepository.Put(people);
        return resultPeople.Error == true ? 
            new ResultDTO<People>("Erro ao remover pessoa do quarto.") : 
            resultPeople;

        /*
        var transaction = await _context.Database.BeginTransactionAsync();

        resultPeople = await _peopleRepository.Put(people);
        resultRoom = await _roomRepository.Put(room);

        if (resultPeople.Error == true || resultRoom.Error == true)
        {
            await transaction.RollbackAsync();
            return new ResultDTO<People>("Erro ao remover pessoa do quarto.");
        }
        
        await transaction.CommitAsync();

        return resultPeople;
        */
    }
}
