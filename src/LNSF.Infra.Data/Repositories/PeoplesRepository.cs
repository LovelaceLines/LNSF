using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using LNSF.Domain.Enums;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRepository : BaseRepository<People>, IPeopleRepository
{
    private readonly AppDbContext _context;

    public PeopleRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<List<People>> Query(PeopleFilter filter)
    {
        var query = _context.Peoples.AsNoTracking();

        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (!string.IsNullOrEmpty(filter.Name)) query = query.Where(x => x.Name.Contains(filter.Name));
        if (!string.IsNullOrEmpty(filter.RG)) query = query.Where(x => x.RG.Contains(filter.RG));
        if (!string.IsNullOrEmpty(filter.CPF)) query = query.Where(x => x.CPF.Contains(filter.CPF));
        if (!string.IsNullOrEmpty(filter.Phone)) query = query.Where(x => x.Phone.Contains(filter.Phone));
        if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.Id);
        else query = query.OrderBy(x => x.Id);

        var peoples = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return peoples;
    }
}
