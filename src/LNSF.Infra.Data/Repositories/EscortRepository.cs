using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EscortRepository : BaseRepository<Escort>, IEscortRepository
{
    private readonly AppDbContext _context;
    
    public EscortRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<Escort>> Query (EscortFilter filter)
    {
        var query = _context.Escorts.AsNoTracking();

        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.PeopleId != null) query = query.Where(x => x.PeopleId == filter.PeopleId);
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Id);
        else query = query.OrderByDescending(x => x.Id);

        var escorts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return escorts;
    }

    public async Task<bool> PeopleExists(int peopleId)
    {
        var escort = await _context.Escorts.AsNoTracking()
            .Where(x => x.PeopleId == peopleId)
            .FirstOrDefaultAsync();

        return escort != null;
    }
}
