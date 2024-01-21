﻿using LNSF.Domain.Entities;
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
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<Treatment> _treatments;
    private readonly IQueryable<PatientTreatment> _patientsTreatments;

    public PatientTreatmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _patients = _context.Patients.AsNoTracking();
        _treatments = _context.Treatments.AsNoTracking();
        _patientsTreatments = _context.PatientsTreatments.AsNoTracking();
    }

    public async Task<List<PatientTreatment>> Query(PatientTreatmentFilter filter)
    {
        var query = _patientsTreatments;

        if (filter.PatientId.HasValue) query = QueryPatientId(query, filter.PatientId.Value);
        if (filter.TreatmentId.HasValue) query = QueryTreatmentId(query, filter.TreatmentId.Value);

        var patientsTreatments = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return patientsTreatments;
    }

    public static IQueryable<PatientTreatment> QueryPatientId(IQueryable<PatientTreatment> query, int patientId) =>
        query.Where(pt => pt.PatientId == patientId);

    public static IQueryable<PatientTreatment> QueryTreatmentId(IQueryable<PatientTreatment> query, int treatmentId) =>
        query.Where(pt => pt.TreatmentId == treatmentId);

    public async Task<bool> ExistsByPatientIdAndTreatmentId(int patientId, int treatmentId) =>
        await _patientsTreatments.AnyAsync(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId);

    public async Task<PatientTreatment> GetByPatientIdAndTreatmentId(int patientId, int treatmentId) =>
        await _patientsTreatments.FirstOrDefaultAsync(pt => pt.PatientId == patientId && pt.TreatmentId == treatmentId) ??
            throw new AppException("Relacionamento Paciente e Tratamento não encontrado!", HttpStatusCode.NotFound);
}
