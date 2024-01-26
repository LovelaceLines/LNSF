using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace LNSF.Infra.Data.Repositories;

public class FamilyGroupProfileRepository : BaseRepository<FamilyGroupProfile>, IFamilyGroupProfileRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<FamilyGroupProfile> _familyGroupProfiles;
    private readonly IQueryable<Patient> _patients;
    private readonly IQueryable<People> _peoples;

    public FamilyGroupProfileRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _familyGroupProfiles = _context.FamilyGroupProfiles.AsNoTracking();
        _patients = _context.Patients.AsNoTracking();
        _peoples = _context.Peoples.AsNoTracking();
    }

    public async Task<List<FamilyGroupProfile>> Query(FamilyGroupProfileFilter filter)
    {
        var query = _familyGroupProfiles;

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!, _peoples);

        if (filter.Id.HasValue) query = QueryId(query, filter.Id.Value);
        if (!filter.Name.IsNullOrEmpty()) query = QueryName(query, filter.Name!);
        if (!filter.Kinship.IsNullOrEmpty()) query = QueryKinship(query, filter.Kinship!);
        if (filter.Age.HasValue) query = QueryAge(query, filter.Age.Value);
        if (!filter.Profession.IsNullOrEmpty()) query = QueryProfession(query, filter.Profession!);
        if (filter.Income.HasValue) query = QueryIncome(query, filter.Income.Value);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(f => f.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(f => f.Name);

        var familyGroupProfiles = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(Build(filter.GetPatient ?? false, _patients))
            .ToListAsync();

        return familyGroupProfiles;
    }

    private static Expression<Func<FamilyGroupProfile, FamilyGroupProfile>> Build(bool getPatient, IQueryable<Patient> patients) =>
        f => new FamilyGroupProfile
        {
            Id = f.Id,
            Name = f.Name,
            Kinship = f.Kinship,
            Age = f.Age,
            Profession = f.Profession,
            Income = f.Income,
            PatientId = f.PatientId,
            Patient = !getPatient ? null :
                patients.Include(p => p.People).First(p => p.Id == f.PatientId),
        };

    public static IQueryable<FamilyGroupProfile> QueryGlobalFilter(IQueryable<FamilyGroupProfile> query, string globalFilter, IQueryable<People> peoples) =>
        query.Where(f =>
            QueryName(query, globalFilter).Any(q => q.Id == f.Id) ||
            QueryKinship(query, globalFilter).Any(q => q.Id == f.Id) ||
            QueryProfession(query, globalFilter).Any(q => q.Id == f.Id) ||
            PeopleRepository.QueryGlobalFilter(peoples, globalFilter).Any(p => p.Id == f.Patient!.PeopleId));

    public static IQueryable<FamilyGroupProfile> QueryId(IQueryable<FamilyGroupProfile> query, int id) =>
        query.Where(f => f.Id == id);

    public static IQueryable<FamilyGroupProfile> QueryName(IQueryable<FamilyGroupProfile> query, string name) =>
        query.Where(f => f.Name.ToLower().Contains(name.ToLower()));

    public static IQueryable<FamilyGroupProfile> QueryKinship(IQueryable<FamilyGroupProfile> query, string kinship) =>
        query.Where(f => f.Kinship.ToLower().Contains(kinship.ToLower()));

    public static IQueryable<FamilyGroupProfile> QueryAge(IQueryable<FamilyGroupProfile> query, int age) =>
        query.Where(f => f.Age == age);

    public static IQueryable<FamilyGroupProfile> QueryProfession(IQueryable<FamilyGroupProfile> query, string profession) =>
        query.Where(f => f.Profession.ToLower().Contains(profession.ToLower()));

    public static IQueryable<FamilyGroupProfile> QueryIncome(IQueryable<FamilyGroupProfile> query, double income) =>
        query.Where(f => f.Income == income);
}
