using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRepository : BaseRepository<People>, IPeopleRepository
{
    private readonly AppDbContext _context;

    public PeopleRepository(AppDbContext context) : base(context) =>
        _context = context;

    public async Task<List<People>> Query(PeopleFilter filter)
    {
        var query = _context.Peoples.AsNoTracking();

        var patients = _context.Patients.AsNoTracking();
        var hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        var escorts = _context.Escorts.AsNoTracking();
        var hostings = _context.Hostings.AsNoTracking();

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = query.Where(p =>
            p.Name.ToLower().Contains(filter.GlobalFilter!.ToLower()) ||
            p.RG.Contains(filter.GlobalFilter!) ||
            p.CPF.Contains(filter.GlobalFilter!) ||
            p.Phone.Contains(filter.GlobalFilter!) ||
            p.City.ToLower().Contains(filter.GlobalFilter!.ToLower()));

        if (filter.Id.HasValue) query = query.Where(p => p.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(p => p.Name.ToLower().Contains(filter.Name!.ToLower()));
        if (!filter.RG.IsNullOrEmpty()) query = query.Where(p => p.RG.Contains(filter.RG!));
        if (!filter.CPF.IsNullOrEmpty()) query = query.Where(p => p.CPF.Contains(filter.CPF!));
        if (!filter.Phone.IsNullOrEmpty()) query = query.Where(p => p.Phone.Contains(filter.Phone!));

        if (filter.Patient == true) query = query.Where(p => 
            patients.Any(pt => pt.PeopleId == p.Id));
        else if (filter.Patient == false) query = query.Where(p =>
            !patients.Any(pt => pt.PeopleId == p.Id));

        if (filter.Escort == true) query = query.Where(p =>
            escorts.Any(e => e.PeopleId == p.Id));
        else if (filter.Escort == false) query = query.Where(p =>
            !escorts.Any(e => e.PeopleId == p.Id));
        
        if (filter.Active == true) query = query.Where(p =>
            hostings.Any(h => h.Patient!.PeopleId == p.Id &&
                h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)
            ||
            hostingsEscorts.Any(he => he.Escort!.PeopleId == p.Id &&
                he.Hosting!.CheckIn <= DateTime.Now && DateTime.Now <= he.Hosting.CheckOut));
        else if (filter.Active == false) query = query.Where(p =>
            !hostings.Any(h => h.Patient!.PeopleId == p.Id &&
                h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)
            &&
            !hostingsEscorts.Any(he => he.Escort!.PeopleId == p.Id &&
                he.Hosting!.CheckIn <= DateTime.Now && DateTime.Now <= he.Hosting.CheckOut));

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(p => p.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(p => p.Name);

        var peoples = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return peoples;
    }
}
