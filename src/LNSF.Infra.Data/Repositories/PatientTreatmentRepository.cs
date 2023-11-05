using System.Net;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PatientTreatmentRepository : BaseRepository<PatientTreatment>, IPatientTreatmentRepository
{
    private readonly AppDbContext _context;
    public PatientTreatmentRepository(AppDbContext context) : base(context) => 
        _context = context;

    public Task<List<PatientTreatment>> RemoveByPatientId(int patientId)
    {
        var patientsTreatments = _context.PatientsTreatments.AsNoTracking()
            .Where(pt => pt.PatientId == patientId);
        
        _context.PatientsTreatments.RemoveRange(patientsTreatments);

        return patientsTreatments.ToListAsync();
    }

    public Task<List<PatientTreatment>> RemoveByTreatmentId(int treatmentId)
    {
        var patientsTreatments = _context.PatientsTreatments.AsNoTracking()
            .Where(pt => pt.TreatmentId == treatmentId);
        
        _context.PatientsTreatments.RemoveRange(patientsTreatments);

        return patientsTreatments.ToListAsync(); 
    }

    public async Task<bool> ExistsByPatientIdAndTreatmentId(int patientId, int treatmentId)
    {
        var patientTreatment =  await _context.PatientsTreatments.AsNoTracking()
            .Where(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId)
            .FirstOrDefaultAsync();
        
        return patientTreatment != null;
    }

    public async Task<List<PatientTreatment>> GetByPatientId(int patientId)
    {
        var patientsTreatments = await _context.PatientsTreatments.AsNoTracking()
            .Where(pt => pt.PatientId == patientId)
            .ToListAsync();
        
        return patientsTreatments;
    }

    public async Task<PatientTreatment> GetByPatientIdAndTreatmentId(int patientId, int treatmentId)
    {
        var patientTreatment = await _context.PatientsTreatments.AsNoTracking()
            .Where(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId)
            .FirstOrDefaultAsync();
        
        return patientTreatment ?? throw new AppException("Relacionamento Paciente e Tratamento não encontrado!", HttpStatusCode.NotFound);
    }

    public Task<List<PatientTreatment>> GetByTreatmentId(int treatmentId)
    {
        var patientsTreatments = _context.PatientsTreatments.AsNoTracking()
            .Where(pt => pt.TreatmentId == treatmentId)
            .ToListAsync();

        return patientsTreatments;
    }
}
