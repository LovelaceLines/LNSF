using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HostingRepository : BaseRepository<Hosting>, IHostingRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Hosting> _hostings;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public HostingRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _hostings = _context.Hostings.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
    }

    public async Task<List<HostingDTO>> Query(HostingFilter filter)
    {
        var query = _hostings;
        var hostingsEscorts = _hostingsEscorts;

        if (filter.Id.HasValue) query = query.Where(h => h.Id == filter.Id);
        if (filter.PatientId.HasValue) query = query.Where(h => h.PatientId == filter.PatientId);
        if (filter.EscortId.HasValue) query = query.Where(h => 
            hostingsEscorts.Any(he => he.HostingId == h.Id && he.EscortId == filter.EscortId));
        if (filter.CheckIn.HasValue) query = query.Where(h => h.CheckIn >= filter.CheckIn);
        if (filter.CheckOut.HasValue) query = query.Where(h => h.CheckOut <= filter.CheckOut);

        if (filter.Active == true) query = query.Where(h =>
            h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut);
        else if (filter.Active == false) query = query.Where(h =>
            !(h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut));

        List<HostingDTO> hostings = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(h => new HostingDTO
            {
                Id = h.Id,
                CheckIn = h.CheckIn,
                CheckOut = h.CheckOut,
                PatientId = h.PatientId,
                Patient = filter.GetPatient.GetValueOrDefault(false) ? h.Patient : null,
            })
            .ToListAsync();
        
        if (filter.GetPatient == true && filter.GetPatientPeople == true)
            hostings.ForEach(h => h.Patient!.People = _context.Peoples.AsNoTracking()
                .FirstOrDefault(p => p.Id == h.Patient.PeopleId));

        if (filter.GetEscort == true)
        {
            hostings.ForEach(h => h.Escorts = _context.Escorts.AsNoTracking()
                .Where(e => hostingsEscorts.Any(he => he.HostingId == h.Id && he.EscortId == e.Id))
                .ToArray());
            
            if (filter.GetEscortPeople == true)
                hostings.ForEach(h => h.Escorts!.ToList().ForEach(e => e.People = _context.Peoples.AsNoTracking()
                    .FirstOrDefault(p => p.Id == e.PeopleId)));
        }

        return hostings;
    }

    public async Task<bool> ExistsByIdAndPatientId(int id, int patientId) =>
        await _hostings.AnyAsync(h => h.Id == id && h.PatientId == patientId);

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _hostings.AnyAsync(h => h.Id == id && (h.Patient!.PeopleId == peopleId ||
            _hostingsEscorts.Any(he => he.HostingId == id && 
                _escorts.Any(e => e.Id == he.EscortId && e.PeopleId == peopleId))));
    
    public async Task<bool> ExistsWithDateConflict(Hosting hosting) =>
        await _hostings.AnyAsync(h => h.PatientId == hosting.PatientId && hosting.Id != h.Id &&
            (hosting.CheckIn < h.CheckIn || hosting.CheckIn <= h.CheckOut || h.CheckOut == null));
}