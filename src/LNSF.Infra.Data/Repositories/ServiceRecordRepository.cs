using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class ServiceRecordRepository : BaseRepository<ServiceRecord>, IServiceRecordRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<People> _peoples;
    private readonly IQueryable<Treatment> _treatments;
    private readonly IQueryable<PatientTreatment> _patientsTreatments;
    private readonly IQueryable<Hospital> _hospitals;


    public ServiceRecordRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _patients = _context.Patients.AsNoTracking();
        _peoples = _context.Peoples.AsNoTracking();
        _treatments = _context.Treatments.AsNoTracking();
        _patientsTreatments = _context.PatientsTreatments.AsNoTracking();
        _hospitals = _context.Hospitals.AsNoTracking();
    }

    public Task<List<ServiceRecord>> Query(ServiceRecordFilter filter)
    {
        var query = _context.ServiceRecords.AsNoTracking();

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryServiceRecordGlobal(query, filter.GlobalFilter!, _patients, _peoples, _treatments, _patientsTreatments, _hospitals);

        if (filter.Id.HasValue) query = QueryServiceRecordById(query, filter.Id.Value);
        if (filter.PatientId.HasValue) query = QueryServiceRecordPatientId(query, filter.PatientId.Value);

        var serviceRecords = query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPatient ?? false, _patients))
            .ToListAsync();

        return serviceRecords;
    }

    public static Expression<Func<ServiceRecord, ServiceRecord>> Build(bool getPatient, IQueryable<Patient> patients) =>
        s => new ServiceRecord
        {
            Id = s.Id,
            PatientId = s.PatientId,
            Patient = !getPatient ? null :
                patients.Include(p => p.People).First(p => p.Id == s.PatientId),

            SocialProgram = s.SocialProgram,
            SocialProgramNote = s.SocialProgramNote,

            DomicileType = s.DomicileType,
            MaterialExternalWallsDomicile = s.MaterialExternalWallsDomicile,
            AccessElectricalEnergy = s.AccessElectricalEnergy,
            HasWaterSupply = s.HasWaterSupply,
            WayWaterSupply = s.WayWaterSupply,
            SanitaryDrainage = s.SanitaryDrainage,
            GarbageCollection = s.GarbageCollection,
            NumberRooms = s.NumberRooms,
            NumberBedrooms = s.NumberBedrooms,
            NumberPeoplePerBedroom = s.NumberPeoplePerBedroom,
            DomicileHasAccessibility = s.DomicileHasAccessibility,
            IsLocatedInRiskArea = s.IsLocatedInRiskArea,
            IsLocatedInDifficultAccessArea = s.IsLocatedInDifficultAccessArea,
            IsLocatedInConflictViolenceArea = s.IsLocatedInConflictViolenceArea,

            AccessToUnitNote = s.AccessToUnitNote,
            AccessToUnit = s.AccessToUnit,
            FirstAttendanceReason = s.FirstAttendanceReason,

            DemandPresented = s.DemandPresented,
            Referrals = s.Referrals,
            Observations = s.Observations,
        };

    public static IQueryable<ServiceRecord> QueryServiceRecordGlobal(IQueryable<ServiceRecord> query, string globalFilter, IQueryable<Patient> patients, IQueryable<People> peoples, IQueryable<Treatment> treatments, IQueryable<PatientTreatment> patientsTreatments, IQueryable<Hospital> hospitals) =>
        query.Where(s => PatientRepository.QueryGlobalFilter(patients, globalFilter, peoples, treatments, patientsTreatments, hospitals).Any(p => p.Id == s.PatientId));

    public static IQueryable<ServiceRecord> QueryServiceRecordById(IQueryable<ServiceRecord> query, int id) =>
        query.Where(s => s.Id == id);

    public static IQueryable<ServiceRecord> QueryServiceRecordPatientId(IQueryable<ServiceRecord> query, int patientId) =>
        query.Where(s => s.PatientId == patientId);

    public async Task<bool> ExistsById(int id, int patientId) =>
        await _context.ServiceRecords.AnyAsync(s => s.Id == id && s.PatientId == patientId);

    public async Task<bool> ExistsByPatientId(int patientId) =>
        await _context.ServiceRecords.AnyAsync(s => s.PatientId == patientId);
}
