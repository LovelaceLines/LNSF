using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class ToursRepository : BaseRepository<Tour>, IToursRepository
{
    private readonly AppDbContext _context;

    public ToursRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<List<Tour>> Query(TourFilters filters)
    {
        var query = _context.Tours.AsNoTracking();
        var count = await query.CountAsync();

        if (filters.Id != null) query = query.Where(x => x.Id == filters.Id);
        if (filters.Output != null) query = query.Where(x => x.Output >= filters.Output);
        if (filters.Input == null) query = query.Where(x => x.Input == null);
        else query = query.Where(x => x.Input <= filters.Input);
        if (filters.Note != null) query = query.Where(x => x.Note.Contains(filters.Note));
        if (filters.PeopleId != null) query = query.Where(x => x.PeopleId == filters.PeopleId);
        if (filters.Order == OrderBy.Ascending) query = query.OrderBy(x => x.Output);
        else query = query.OrderByDescending(x => x.Output);

        var tours = await query
            .Skip((filters.Page.Page - 1) * filters.Page.PageSize)
            .Take(filters.Page.PageSize)
            .ToListAsync();

        return tours;
    }
}
