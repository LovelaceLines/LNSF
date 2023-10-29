using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using LNSF.Domain.Filters;
using LNSF.Domain.Enums;
using Microsoft.EntityFrameworkCore;
namespace LNSF.Infra.Data.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    private readonly AppDbContext _context;
    public PatientRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Patient>> Query(PatientFilter filter) 
    {
        var query = _context.Patients.AsNoTracking();
        
        if (filter.Id.HasValue)query = query.Where(x => x.Id == filter.Id);
        if (filter.PatientId.HasValue)query = query.Where(x => x.PeopleId == filter.PatientId);
        if (filter.HospitalId.HasValue)query = query.Where(x => x.HospitalId == filter.HospitalId);
        if (filter.SocioEconomicRecord.HasValue)query = query.Where(x => x.SocioeconomicRecord == filter.SocioEconomicRecord);
        if (filter.Term.HasValue)query = query.Where(x => x.Term == filter.Term);
        if (filter.Order == OrderBy.Descending) query = query.OrderByDescending(x => x.Id);
        else query = query.OrderBy(x => x.Id);

        var patients = await query
        .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
        .Take(filter.Page.PageSize)
        .ToListAsync();

        return patients ;
    }
}
