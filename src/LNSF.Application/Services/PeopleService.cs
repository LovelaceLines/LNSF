﻿using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using System.Net;

namespace LNSF.Application;

public class PeopleService
{
    private readonly IPeoplesRepository _peopleRepository;
    private readonly IRoomsRepository _roomRepository;
    private readonly PeopleValidator _peopleValidator;

    public PeopleService(IPeoplesRepository peopleRepository,
        IRoomsRepository roomRepository,
        PeopleValidator peopleValidator)
    {
        _peopleRepository = peopleRepository;
        _roomRepository = roomRepository;
        _peopleValidator = peopleValidator;
    }

    public async Task<List<People>> Query(PeopleFilter filter) => 
        await _peopleRepository.Query(filter);

    public async Task<People> Get(int id) => 
        await _peopleRepository.Get(id);
    
    public async Task<int> GetCount() =>
        await _peopleRepository.GetCount();

    public async Task<People> Create(People people)
    {
        var validationResult = _peopleValidator.Validate(people);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        return await _peopleRepository.Add(people);
    }

    public async Task<People> Update(People newPeople)
    {
        var validationResult = _peopleValidator.Validate(newPeople);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        if (!await _peopleRepository.Exists(newPeople.Id)) throw new AppException("Pessoa não encontrada.", HttpStatusCode.UnprocessableEntity);

        var oldPeople = await _peopleRepository.Get(newPeople.Id);
        newPeople.RoomId = oldPeople.RoomId; //Quarto não pode ser alterado.

        return await _peopleRepository.Update(newPeople);	
    }

    public async Task<People> AddPeopleToRoom(int peopleId, int roomId)
    {
        if (!await _peopleRepository.Exists(peopleId)) throw new AppException("Pessoa não encontrada.", HttpStatusCode.UnprocessableEntity);
        if (!await _roomRepository.Exists(roomId)) throw new AppException("Quarto não encontrado.", HttpStatusCode.UnprocessableEntity);
        var people = await _peopleRepository.Get(peopleId);
        var room = await _roomRepository.Get(roomId);
        if (people.RoomId == roomId) throw new AppException("Pessoa já está no quarto.", HttpStatusCode.UnprocessableEntity);
        if (people.RoomId != null) throw new AppException("Pessoa já está em um quarto.", HttpStatusCode.UnprocessableEntity);
        if (room.Beds <= room.Occupation) throw new AppException("Não há vagas.", HttpStatusCode.UnprocessableEntity);
        if (!room.Available) throw new AppException("Quarto indisponível.", HttpStatusCode.UnprocessableEntity);

        people.RoomId = roomId;
        room.Occupation++;
        if (room.Beds == room.Occupation) room.Available = false;
        
        // await _peopleRepository.BeguinTransaction();
        try
        {
            people = await _peopleRepository.Update(people);
            await _roomRepository.Update(room);
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
        if (!await _peopleRepository.Exists(peopleId)) throw new AppException("Pessoa não encontrada.", HttpStatusCode.UnprocessableEntity);
        var people = await _peopleRepository.Get(peopleId);
        if (people.RoomId == null) throw new AppException("Pessoa não está no quarto.", HttpStatusCode.UnprocessableEntity);
        if (!await _roomRepository.Exists(people.RoomId.Value)) throw new AppException("Quarto não encontrado.", HttpStatusCode.UnprocessableEntity);

        var room = await _roomRepository.Get(people.RoomId.Value);
        people.RoomId = null;
        room.Occupation--;

        // await _peopleRepository.BeguinTransaction();
        try
        {
            people = await _peopleRepository.Update(people);
            await _roomRepository.Update(room);
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
