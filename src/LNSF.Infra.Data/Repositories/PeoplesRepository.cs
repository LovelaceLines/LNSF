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

        var patients = _context.Patients.AsNoTracking();
        var hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        var escorts = _context.Escorts.AsNoTracking();
        var hostings = _context.Hostings.AsNoTracking();

        if (filter.Id != null) query = query.Where(p => p.Id == filter.Id);
        if (!string.IsNullOrEmpty(filter.Name)) query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
        if (!string.IsNullOrEmpty(filter.RG)) query = query.Where(p => p.RG.Contains(filter.RG));
        if (!string.IsNullOrEmpty(filter.CPF)) query = query.Where(p => p.CPF.Contains(filter.CPF));
        if (!string.IsNullOrEmpty(filter.Phone)) query = query.Where(p => p.Phone.Contains(filter.Phone));

        if (filter.Patient == true) query = query.Where(p => 
            patients.Any(pt => pt.PeopleId == p.Id));
        else if (filter.Patient == false) query = query.Where(p =>
            !patients.Any(pt => pt.PeopleId == p.Id));

        if (filter.Escort == true) query = query.Where(p =>
            escorts.Any(e => e.PeopleId == p.Id));
        else if (filter.Escort == false) query = query.Where(p =>
            !escorts.Any(e => e.PeopleId == p.Id));
        
        if (filter.Active == true) query = query.Where(p =>
            patients.Any(pt => pt.PeopleId == p.Id &&
                hostings.Any(h =>h.PatientId == pt.Id && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut))
            ||
            hostingsEscorts.Any(he => 
                escorts.Any(e => e.Id == he.EscortId && e.PeopleId == p.Id) 
                &&
                hostings.Any(h => h.Id == he.HostingId && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)
            ));
        else if (filter.Active == false) query = query.Where(p =>
            !patients.Any(pt => pt.PeopleId == p.Id && 
                hostings.Any(h =>h.PatientId == pt.Id && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut))
            &&
            !hostingsEscorts.Any(he =>
                escorts.Any(e => e.Id == he.EscortId && e.PeopleId == p.Id) 
                &&
                hostings.Any(h => h.Id == he.HostingId && 
                    h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut)
            ));

        if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(p => p.Name);
        else query = query.OrderBy(p => p.Name);

        var peoples = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return peoples;
    }
}
