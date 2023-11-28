using System.Net;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HostingRepository : BaseRepository<Hosting>, IHostingRepository
{
    private readonly AppDbContext _context;
    private readonly IHostingEscortRepository _hostingEscortRepository;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Hosting> _hostings;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public HostingRepository(AppDbContext context, 
        IHostingEscortRepository hostingEscortRepository) : base(context)
    {
        _context = context;
        _patients = _context.Patients.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
        _hostingEscortRepository = hostingEscortRepository;
    }

    public async Task<List<Hosting>> Query(HostingFilter filter)
    {
        var query = _context.Hostings.AsNoTracking();
        var hostingsEscorts = _context.HostingsEscorts.AsNoTracking();

        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.PatientId != null) query = query.Where(x => x.PatientId == filter.PatientId);
        if (filter.EscortId != null) query = query.Where(x => 
            hostingsEscorts.Any(y => y.HostingId == x.Id && y.EscortId == filter.EscortId));
        if (filter.PatientCheckIn != null) query = query.Where(x => x.CheckIn >= filter.PatientCheckIn);
        if (filter.PatientCheckOut != null) query = query.Where(x => x.CheckOut <= filter.PatientCheckOut);
        if (filter.EscortCheckIn != null) query = query.Where(x => 
            hostingsEscorts.Any(y => y.HostingId == x.Id && y.CheckIn >= filter.EscortCheckIn));
        if (filter.EscortCheckOut != null) query = query.Where(x =>
            hostingsEscorts.Any(y => y.HostingId == x.Id && y.CheckOut <= filter.EscortCheckOut));

        if (filter.Active == true) query = query.Where(x =>
            x.CheckIn <= DateTime.Now && DateTime.Now <= x.CheckOut);
        else if (filter.Active == false) query = query.Where(x =>
            !(x.CheckIn <= DateTime.Now && DateTime.Now <= x.CheckOut));

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Id);
        else query = query.OrderByDescending(x => x.Id);

        var hostings = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        foreach (var hosting in hostings)
        {
            var escortInfos = hostingsEscorts.Where(x => x.HostingId == hosting.Id)
                .Select(x => new HostingEscortInfo{ Id = x.EscortId, CheckIn = x.CheckIn, CheckOut = x.CheckOut })
                .ToList();

            hosting.EscortInfos = escortInfos;
        }

        return hostings;
    }

    public new async Task<Hosting> Add(Hosting hosting)
    {
        await BeguinTransaction();

        try
        {
            await _context.Hostings.AddAsync(hosting);
            await _context.SaveChangesAsync();

            foreach (var escortInfo in hosting.EscortInfos)
                await _hostingEscortRepository.Add(new HostingEscort
                {
                    EscortId = escortInfo.Id,
                    HostingId = hosting.Id,
                    CheckIn = escortInfo.CheckIn,
                    CheckOut = escortInfo.CheckOut
                });
  
            await CommitTransaction();

            return hosting;
        }
        catch (Exception)
        {
            await RollbackTransaction();
            throw new AppException("Erro ao adicionar hospedagem", HttpStatusCode.BadRequest);
        }
    }

    public new async Task<Hosting> Update(Hosting hosting)
    {
        await BeguinTransaction();

        try
        {
            await _hostingEscortRepository.RemoveByHostingId(hosting.Id);

            _context.Hostings.Update(hosting);
            await _context.SaveChangesAsync();

            foreach (var escortInfo in hosting.EscortInfos)
                await _hostingEscortRepository.Add(new HostingEscort
                {
                    EscortId = escortInfo.Id,
                    HostingId = hosting.Id,
                    CheckIn = escortInfo.CheckIn,
                    CheckOut = escortInfo.CheckOut
                });

            await _context.SaveChangesAsync();
            await CommitTransaction();

            return hosting;
        }
        catch (Exception)
        {
            await RollbackTransaction();
            throw new AppException("Erro ao atualizar hospedagem", HttpStatusCode.BadRequest);
        }
    }

    public Task<bool> ExistsByIdAndPatientId(int id, int patientId) =>
        _context.Hostings.AsNoTracking()
            .AnyAsync(x => x.Id == id && x.PatientId == patientId);
    
    public Task<bool> ExistsByPeopleIdAndDate(int peopleId, DateTime date) =>
        _hostings.AnyAsync(h => h.CheckIn <= date && date <= h.CheckOut
            && (
                _patients.Any(p => p.PeopleId == peopleId && p.Id == h.PatientId)
                ||
                _hostingsEscorts.Any(he => 
                    _escorts.Any(e => e.PeopleId == peopleId && e.Id == he.EscortId) 
                    && h.Id == he.HostingId)
                )
            );
            
    public Task<List<CheckInAndCheckOut>> GetCheckInAndCheckOutByPeopleId(int peopleId)
    {
        var checks = new List<CheckInAndCheckOut>();
        
        checks = _hostings.Where(h => 
            _patients.Any(p => p.PeopleId == peopleId && p.Id == h.PatientId)
            ||
            _hostingsEscorts.Any(he => 
                _escorts.Any(e => e.PeopleId == peopleId && e.Id == he.EscortId) 
                && h.Id == he.HostingId)
            )
            .Select(h => new CheckInAndCheckOut { CheckIn = h.CheckIn, CheckOut = h.CheckOut })
            .ToList();
                
        return Task.FromResult(checks);
    }
}