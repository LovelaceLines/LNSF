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

    public async Task<List<Tour>> Get(Pagination pagination)
    {
        var query = _context.Tours.AsNoTracking();

        var tours = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return tours;
    }
}
