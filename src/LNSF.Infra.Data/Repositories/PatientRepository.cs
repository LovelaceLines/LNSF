using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Treatment> _treatments;
    private readonly IQueryable<Hospital> _hospitals;
    private readonly IQueryable<PatientTreatment> _patientsTreatments;
    private readonly IQueryable<Escort> _escorts;
    private readonly IQueryable<Hosting> _hostings;
    private readonly IQueryable<HostingEscort> _hostingsEscorts;

    public PatientRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _peoples = _context.Peoples.AsNoTracking();
        _patients = _context.Patients.AsNoTracking();
        _treatments = _context.Treatments.AsNoTracking();
        _hospitals = _context.Hospitals.AsNoTracking();
        _patientsTreatments = _context.PatientsTreatments.AsNoTracking();
        _escorts = _context.Escorts.AsNoTracking();
        _hostings = _context.Hostings.AsNoTracking();
        _hostingsEscorts = _context.HostingsEscorts.AsNoTracking();
    }

    public async Task<List<PatientDTO>> Query(PatientFilter filter)
    {
        var query = _patients;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!, _peoples, _treatments, _patientsTreatments, _hospitals);

        if (filter.Id.HasValue) query = QueryPatientId(query, filter.Id.Value);
        if (filter.PeopleId.HasValue) query = QueryPeopleId(query, filter.PeopleId.Value);
        if (filter.HospitalId.HasValue) query = QueryHospitalId(query, filter.HospitalId.Value);
        if (filter.SocioEconomicRecord.HasValue) query = QuerySocioEconomicRecord(query, filter.SocioEconomicRecord.Value);
        if (filter.Term.HasValue) query = QueryTerm(query, filter.Term.Value);
        if (filter.TreatmentId.HasValue) query = QueryTreatmentId(query, filter.TreatmentId.Value, _patientsTreatments);

        if (filter.Active.HasValue) query = QueryActive(query, _hostings, filter.Active.Value);
        if (filter.IsVeteran.HasValue) query = QueryVeteran(query, filter.IsVeteran.Value, _peoples, _hostings, _hostingsEscorts);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(p => p.Id);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(p => p.Id);

        var patients = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPeople ?? false, filter.GetHospital ?? false, filter.GetTreatments ?? false, _patientsTreatments))
            .ToListAsync();

        return patients;
    }

    public static IQueryable<Patient> QueryGlobalFilter(IQueryable<Patient> query, string globalFilter, IQueryable<People> peoples, IQueryable<Treatment> treatments, IQueryable<PatientTreatment> patientsTreatments, IQueryable<Hospital> hospitals) =>
        query.Where(pa =>
            PeopleRepository.QueryGlobalFilter(peoples, globalFilter).Any(pe => pe.Id == pa.PeopleId) ||
            HospitalRepository.QueryGlobalFilter(hospitals, globalFilter).Any(h => h.Id == pa.HospitalId) ||
            TreatmentRepository.QueryGlobalFilter(treatments, globalFilter).Any(t => patientsTreatments.Any(pt => pt.PatientId == pa.Id && pt.TreatmentId == t.Id)));

    public static IQueryable<Patient> QueryPatientId(IQueryable<Patient> query, int id) =>
        query.Where(p => p.Id == id);

    public static IQueryable<Patient> QueryPeopleId(IQueryable<Patient> query, int peopleId) =>
        query.Where(p => p.PeopleId == peopleId);

    public static IQueryable<Patient> QueryHospitalId(IQueryable<Patient> query, int hospitalId) =>
        query.Where(p => p.HospitalId == hospitalId);

    public static IQueryable<Patient> QuerySocioEconomicRecord(IQueryable<Patient> query, bool socioEconomicRecord) =>
        query.Where(p => p.SocioeconomicRecord == socioEconomicRecord);

    public static IQueryable<Patient> QueryTerm(IQueryable<Patient> query, bool term) =>
        query.Where(p => p.Term == term);

    public static IQueryable<Patient> QueryTreatmentId(IQueryable<Patient> query, int treatmentId, IQueryable<PatientTreatment> patientsTreatments) =>
        query.Where(p => patientsTreatments.Any(pt => pt.PatientId == p.Id && pt.TreatmentId == treatmentId));

    public static IQueryable<Patient> QueryActive(IQueryable<Patient> patients, IQueryable<Hosting> hostings, bool active) =>
        active ? patients.Where(p => HostingRepository.QueryActive(hostings, true).Any(h => h.PatientId == p.Id)) :
            patients.Where(p => !HostingRepository.QueryActive(hostings, true).Any(h => h.PatientId == p.Id));

    public static IQueryable<Patient> QueryVeteran(IQueryable<Patient> query, bool veteran, IQueryable<People> peoples, IQueryable<Hosting> hostings, IQueryable<HostingEscort> hostingsEscorts) =>
        query.Where(pa => PeopleRepository.QueryVeteran(peoples, veteran, hostings, hostingsEscorts).Any(p => p.Id == pa.PeopleId));

    public static Expression<Func<Patient, PatientDTO>> Build(bool getPeople, bool getHospital, bool getTreatments, IQueryable<PatientTreatment> patientsTreatments) =>
        p => new PatientDTO
        {
            Id = p.Id,
            PeopleId = p.PeopleId,
            HospitalId = p.HospitalId,
            SocioeconomicRecord = p.SocioeconomicRecord,
            Term = p.Term,
            People = getPeople ? p.People : null,
            Hospital = getHospital ? p.Hospital : null,
            Treatments = getTreatments ?
                patientsTreatments.Where(pt => pt.PatientId == p.Id).Select(pt => pt.Treatment!).ToList() :
                null
        };

    public async Task<bool> ExistsByPeopleId(int peopleId) =>
        await _patients.AnyAsync(x => x.PeopleId == peopleId);

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _patients.AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
}
