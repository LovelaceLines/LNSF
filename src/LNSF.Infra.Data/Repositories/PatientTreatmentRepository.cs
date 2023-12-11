using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class PatientTreatmentRepository : BaseRepository<PatientTreatment>, IPatientTreatmentRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<PatientTreatment> _patientsTreatments;

    public PatientTreatmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _patientsTreatments = _context.PatientsTreatments.AsNoTracking();
    }

    public async Task<List<PatientTreatment>> Query(PatientTreatmentFilter filter)
    {
        var query = _patientsTreatments;

        if (filter.PatientId.HasValue) query = query.Where(pt => pt.PatientId == filter.PatientId);
        if (filter.TreatmentId.HasValue) query = query.Where(pt => pt.TreatmentId == filter.TreatmentId);

        var patientsTreatments = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return patientsTreatments;
    }

    public Task<List<PatientTreatment>> RemoveByPatientId(int patientId)
    {
        var patientsTreatments = _patientsTreatments.Where(pt => pt.PatientId == patientId);
        
        _context.PatientsTreatments.RemoveRange(patientsTreatments);

        return patientsTreatments.ToListAsync();
    }

    public async Task<bool> ExistsByPatientIdAndTreatmentId(int patientId, int treatmentId) =>
        await _patientsTreatments.AnyAsync(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId);

    public async Task<List<PatientTreatment>> GetByPatientId(int patientId) => 
        await _patientsTreatments.Where(pt => pt.PatientId == patientId).ToListAsync(); 

    public async Task<PatientTreatment> GetByPatientIdAndTreatmentId(int patientId, int treatmentId) =>
        await _patientsTreatments.FirstOrDefaultAsync(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId) ??
            throw new AppException("Relacionamento Paciente e Tratamento não encontrado!", HttpStatusCode.NotFound);
}
