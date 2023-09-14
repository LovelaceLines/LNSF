using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PeoplesRepository : IPeoplesRepository
{
    private readonly AppDbContext _context;

    public PeoplesRepository(AppDbContext context) =>
        _context = context;

    public async Task<ResultDTO<List<People>>> Get(PeopleFilters filters)
    {
        var query = _context.Peoples.AsNoTracking();
        var count = await query.CountAsync();

        if (!string.IsNullOrEmpty(filters.Name)) query = query.Where(x => x.Name.Contains(filters.Name));
        if (!string.IsNullOrEmpty(filters.RG)) query = query.Where(x => x.RG.Contains(filters.RG));
        if (!string.IsNullOrEmpty(filters.CPF)) query = query.Where(x => x.CPF.Contains(filters.CPF));
        if (!string.IsNullOrEmpty(filters.Phone)) query = query.Where(x => x.Phone.Contains(filters.Phone));
        if (!string.IsNullOrEmpty(filters.RoomNumber)) query = query.Where(x => x.Room.Number.Contains(filters.RoomNumber));

        var peoples = await query
            .Skip((filters.Page.Page - 1) * filters.Page.PageSize)
            .Take(filters.Page.PageSize)
            .ToListAsync();

        return new ResultDTO<List<People>>(peoples);
    }

    public async Task<ResultDTO<People>> Get(int id)
    {
        var people = await _context.Peoples.FindAsync(id);

        return (people == null) ?
            new ResultDTO<People>("Não encontrado") :
            new ResultDTO<People>(people);
    }

    public async Task<ResultDTO<int>> GetQuantity() =>
        new ResultDTO<int>(await _context.Peoples.CountAsync());

    public async Task<ResultDTO<People>> Post(People people)
    {
        _context.Peoples.Add(people);
        await _context.SaveChangesAsync();
        
        return new ResultDTO<People>(people);
    }

    public async Task<ResultDTO<People>> Put(People people)
    {
        var _people = await _context.Peoples.FindAsync(people.Id);

        if (_people == null) return new ResultDTO<People>("Não encontrado");

        _context.Entry(_people).CurrentValues.SetValues(people);

        _context.Peoples.Update(_people);
        await _context.SaveChangesAsync();

        return new ResultDTO<People>(_people);
    }
}
