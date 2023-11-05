using System.Net;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class PatientRepository : BaseRepository<Patient>, IPatientRepository
{
    private readonly AppDbContext _context;
    private readonly IPatientTreatmentRepository _patientTreatmentRepository;

    public PatientRepository(AppDbContext context,
        IPatientTreatmentRepository patientTreatmentRepository) : base(context)
    {
        _context = context;
        _patientTreatmentRepository = patientTreatmentRepository;
    }

    public async Task<List<Patient>> Query(PatientFilter filter) 
    {
        var query = _context.Patients.AsNoTracking();
        
        if (filter.Id.HasValue) query = query.Where(x => x.Id == filter.Id);
        if (filter.PatientId.HasValue) query = query.Where(x => x.PeopleId == filter.PatientId);
        if (filter.HospitalId.HasValue) query = query.Where(x => x.HospitalId == filter.HospitalId);
        if (filter.SocioEconomicRecord.HasValue) query = query.Where(x => x.SocioeconomicRecord == filter.SocioEconomicRecord);
        if (filter.Term.HasValue) query = query.Where(x => x.Term == filter.Term);
        if (filter.TreatmentId.HasValue) query = query.Where(p =>
           _context.PatientsTreatments.Any(pt => pt.PatientId == p.Id && pt.TreatmentId == filter.TreatmentId));
        if (filter.Order == OrderBy.Ascending) query = query.OrderBy(x => x.Id);
        else query = query.OrderByDescending(x => x.Id);

        var patients = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return patients;
    }

    public async Task<bool> PeopleExists(int peopleId)
    {
        var people = await _context.Patients.AsNoTracking()
            .Where(x => x.PeopleId == peopleId)
            .FirstOrDefaultAsync();
        
        return people != null;
    }

    public new async Task<Patient> Add(Patient patient)
    {
        await BeguinTransaction();

        try
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            foreach (var treatmentId in patient.TreatmentIds)
            {
                await _patientTreatmentRepository.Add(new PatientTreatment
                {
                    PatientId = patient.Id,
                    TreatmentId = treatmentId
                });
            }

            await CommitTransaction();
            return patient;
        }
        catch (Exception)
        {
            await RollbackTransaction();
            throw new AppException("Erro ao adicionar paciente!", HttpStatusCode.BadRequest);
        }
    }

    public new async Task<Patient> Update(Patient patient)
    {
        await BeguinTransaction();

        try
        {
            await _patientTreatmentRepository.RemoveByPatientId(patient.Id);

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            foreach (var treatmentId in patient.TreatmentIds)
            {
                await _patientTreatmentRepository.Add(new PatientTreatment
                {
                    PatientId = patient.Id,
                    TreatmentId = treatmentId
                });
            }

            await CommitTransaction();
            return patient;
        }
        catch (Exception)
        {
            await RollbackTransaction();
            throw new AppException("Erro ao atualizar paciente!", HttpStatusCode.BadRequest);
        }
    }
}
