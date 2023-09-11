using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PeoplesRepository : IPeoplesRepository
{
    private readonly AppDbContext _context;

    public PeoplesRepository(AppDbContext context) =>
        _context = context;

    public async Task<ResultDTO<List<People>>> Get(Pagination pagination)
    {
        var query = _context.Peoples.AsNoTracking();
        var count = await query.CountAsync();

        if (count == 0) return new ResultDTO<List<People>>("Não encontrado");
        
        var peoples = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
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
