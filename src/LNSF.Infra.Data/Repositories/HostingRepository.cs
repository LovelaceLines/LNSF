using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class HostingRepository : BaseRepository<Hosting>, IHostingRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Treatment> _treatments;
    private readonly IQueryable<PatientTreatment> _patientsTreatments;
    private readonly IQueryable<Hospital> _hospitals;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<Hosting> _hostings;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public HostingRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoples = _context.Peoples.AsNoTracking();
        _patients = _context.Patients.AsNoTracking();
        _treatments = _context.Treatments.AsNoTracking();
        _patientsTreatments = _context.PatientsTreatments.AsNoTracking();
        _hospitals = _context.Hospitals.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
    }

    public async Task<List<HostingDTO>> Query(HostingFilter filter)
    {
        var query = _hostings;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!, _patients, _treatments, _patientsTreatments, _escorts, _hostingsEscorts, _peoples, _hospitals);

        if (filter.Id.HasValue) query = QueryHostingId(query, filter.Id.Value);
        if (filter.PatientId.HasValue) query = QueryPatientId(query, filter.PatientId.Value);
        if (filter.EscortId.HasValue) query = QueryEscortId(query, filter.EscortId.Value, _hostingsEscorts);
        if (filter.CheckIn.HasValue) query = QueryCheckIn(query, filter.CheckIn.Value);
        if (filter.CheckOut.HasValue) query = QueryCheckOut(query, filter.CheckOut.Value);

        if (filter.Active.HasValue) query = QueryActive(query, filter.Active.Value);

        List<HostingDTO> hostings = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPatient ?? false, filter.GetEscort ?? false, _patients, _escorts, _hostingsEscorts))
            .ToListAsync();

        return hostings;
    }

    public static IQueryable<Hosting> QueryGlobalFilter(IQueryable<Hosting> query, string globalFilter, IQueryable<Patient> patients, IQueryable<Treatment> treatments, IQueryable<PatientTreatment> patientsTreatments, IQueryable<Escort> escorts, IQueryable<HostingEscort> hostingsEscorts, IQueryable<People> peoples, IQueryable<Hospital> hospitals) =>
        query.Where(h =>
            PatientRepository.QueryGlobalFilter(patients, globalFilter, peoples, treatments, patientsTreatments, hospitals).Any(p => p.Id == h.PatientId) ||
            HostingEscortRepository.QueryGlobalFilter(hostingsEscorts, globalFilter, escorts, peoples).Any(he => he.HostingId == h.Id));

    public static IQueryable<Hosting> QueryHostingId(IQueryable<Hosting> query, int id) =>
        query.Where(h => h.Id == id);

    public static IQueryable<Hosting> QueryPatientId(IQueryable<Hosting> query, int patientId) =>
        query.Where(h => h.PatientId == patientId);

    public static IQueryable<Hosting> QueryEscortId(IQueryable<Hosting> query, int escortId, IQueryable<HostingEscort> hostingsEscorts) =>
        query.Where(h => hostingsEscorts.Any(he => he.HostingId == h.Id && he.EscortId == escortId));

    public static IQueryable<Hosting> QueryCheckIn(IQueryable<Hosting> query, DateTime checkIn) =>
        query.Where(h => h.CheckIn >= checkIn);

    public static IQueryable<Hosting> QueryCheckOut(IQueryable<Hosting> query, DateTime checkOut) =>
        query.Where(h => h.CheckOut <= checkOut);

    public static IQueryable<Hosting> QueryActive(IQueryable<Hosting> query, bool active) =>
        active ? query.Where(h => h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut) :
            query.Where(h => !(h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut));

    public static Expression<Func<Hosting, HostingDTO>> Build(bool getPatient, bool getEscort, IQueryable<Patient> patients, IQueryable<Escort> escorts, IQueryable<HostingEscort> hostingsEscorts) =>
        h => new HostingDTO()
        {
            Id = h.Id,
            CheckIn = h.CheckIn,
            CheckOut = h.CheckOut,
            PatientId = h.PatientId,
            Patient = getPatient != true ? null :
                patients.Include(p => p.People).First(p => p.Id == h.PatientId),
            Escorts = getEscort != true ? null :
                hostingsEscorts.Where(he => he.HostingId == h.Id)
                    .Join(escorts.Include(e => e.People), he => he.EscortId, e => e.Id, (he, e) => e)
                    .ToList()
        };

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