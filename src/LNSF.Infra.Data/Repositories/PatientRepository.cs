using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<PatientTreatment> _patientsTreatments;

    public PatientRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _patients = _context.Patients.AsNoTracking();
        _patientsTreatments = _context.PatientsTreatments.AsNoTracking();
    }

    public async Task<List<Patient>> Query(PatientFilter filter) 
    {
        var query = _patients;
        var hostings = _context.Hostings.AsNoTracking();
        var patientTreatment = _patientsTreatments;
        
        if (filter.Id.HasValue) query = query.Where(p => p.Id == filter.Id);
        if (filter.PatientId.HasValue) query = query.Where(p => p.PeopleId == filter.PatientId);
        if (filter.HospitalId.HasValue) query = query.Where(p => p.HospitalId == filter.HospitalId);
        if (filter.SocioEconomicRecord.HasValue) query = query.Where(p => p.SocioeconomicRecord == filter.SocioEconomicRecord);
        if (filter.Term.HasValue) query = query.Where(p => p.Term == filter.Term);
        if (filter.TreatmentId.HasValue) query = query.Where(p =>
           patientTreatment.Any(pt => pt.PatientId == p.Id && pt.TreatmentId == filter.TreatmentId));

        if (filter.Active == true) query = query.Where(p =>
            hostings.Any(h => h.PatientId == p.Id && 
                h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut));
        else if (filter.Active == false) query = query.Where(p =>
            !hostings.Any(h => h.PatientId == p.Id && 
                h.CheckIn <= DateTime.Now && DateTime.Now <= h.CheckOut));
        
        if (filter.IsVeteran == true) query = query.Where(p =>
            hostings.Count(h => h.PatientId == p.Id) > 1);
        else if (filter.IsVeteran == false) query = query.Where(p =>
            hostings.Count(h => h.PatientId == p.Id) == 1);

        if (filter.Order == OrderBy.Ascending) query = query.OrderBy(p => p.Id);
        else if (filter.Order == OrderBy.Descending) query = query.OrderByDescending(p => p.Id);

        var patients = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return patients;
    }

    public async Task<bool> ExistsByPeopleId(int peopleId) =>
        await _patients.AnyAsync(x => x.PeopleId == peopleId);

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _patients.AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
}
