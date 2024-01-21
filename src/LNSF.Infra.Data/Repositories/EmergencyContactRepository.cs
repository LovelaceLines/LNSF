using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class EmergencyContactRepository : BaseRepository<EmergencyContact>, IEmergencyContactRepository
{
    private readonly AppDbContext _context;
    private readonly IQueryable<EmergencyContact> _emergencyContacts;
    private readonly IQueryable<People> _peoples;

    public EmergencyContactRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _emergencyContacts = _context.EmergencyContacts.AsNoTracking();
        _peoples = _context.Peoples.AsNoTracking();
    }

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilter filter)
    {
        var query = _emergencyContacts;

        if (filter.Id.HasValue) query = QueryEmergencyContactId(query, filter.Id.Value);
        if (!filter.Name.IsNullOrEmpty()) query = QueryName(query, filter.Name!);
        if (!filter.Phone.IsNullOrEmpty()) query = QueryPhone(query, filter.Phone!);
        if (filter.PeopleId.HasValue) query = QueryPeopleId(query, filter.PeopleId.Value);

        if (!filter.GlobalFilter.IsNullOrEmpty()) query = QueryGlobalFilter(query, filter.GlobalFilter!, _peoples);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.Name);

        var contacts = await query
            .Skip(filter.Page.Page * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .Select(c => Convert(c, filter.GetPeople ?? false))
            .ToListAsync();

        return contacts;
    }

    private static EmergencyContact Convert(EmergencyContact contact, bool getPeople) =>
        new()
        {
            Id = contact.Id,
            Name = contact.Name,
            Phone = contact.Phone,
            PeopleId = contact.PeopleId,
            People = getPeople ? contact.People : null
        };

    public static IQueryable<EmergencyContact> QueryGlobalFilter(IQueryable<EmergencyContact> query, string globalFilter, IQueryable<People> peoples) =>
        query.Where(c =>
            QueryName(query, globalFilter).Any(n => n.Id == c.Id) ||
            QueryPhone(query, globalFilter).Any(p => p.Id == c.Id) ||
            PeopleRepository.QueryGlobalFilter(peoples, globalFilter).Any(p => p.Id == c.PeopleId));

    public static IQueryable<EmergencyContact> QueryEmergencyContactId(IQueryable<EmergencyContact> query, int id) =>
        query.Where(c => c.Id == id);

    public static IQueryable<EmergencyContact> QueryPeopleId(IQueryable<EmergencyContact> query, int peopleId) =>
        query.Where(c => c.PeopleId == peopleId);

    public static IQueryable<EmergencyContact> QueryName(IQueryable<EmergencyContact> query, string name) =>
        query.Where(c => c.Name.ToLower().Contains(name.ToLower()));

    public static IQueryable<EmergencyContact> QueryPhone(IQueryable<EmergencyContact> query, string phone) =>
        query.Where(c => c.Phone.Contains(phone));

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) =>
        await _emergencyContacts.AnyAsync(c => c.Id == id && c.PeopleId == peopleId);
}
