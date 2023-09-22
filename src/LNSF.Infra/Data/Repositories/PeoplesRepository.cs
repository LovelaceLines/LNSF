using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PeoplesRepository : BaseRepository<People>, IPeoplesRepository
{
    private readonly AppDbContext _context;

    public PeoplesRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<List<People>> Get(PeopleFilters filters)
    {
        var query = _context.Peoples.AsNoTracking();
        var count = await query.CountAsync();

        if (filters.Id != null) query = query.Where(x => x.Id == filters.Id);
        if (!string.IsNullOrEmpty(filters.Name)) query = query.Where(x => x.Name.Contains(filters.Name));
        if (!string.IsNullOrEmpty(filters.RG)) query = query.Where(x => x.RG.Contains(filters.RG));
        if (!string.IsNullOrEmpty(filters.CPF)) query = query.Where(x => x.CPF.Contains(filters.CPF));
        if (!string.IsNullOrEmpty(filters.Phone)) query = query.Where(x => x.Phone.Contains(filters.Phone));
        if (!string.IsNullOrEmpty(filters.RoomNumber)) query = query.Where(x => x.Room.Number.Contains(filters.RoomNumber));

        var peoples = await query
            .Skip((filters.Page.Page - 1) * filters.Page.PageSize)
            .Take(filters.Page.PageSize)
            .ToListAsync();

        return peoples;
    }
}
