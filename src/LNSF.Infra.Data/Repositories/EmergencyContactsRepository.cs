using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EmergencyContactsRepository : BaseRepository<EmergencyContact>, IEmergencyContactsRepository
{
    private readonly AppDbContext _context;

    public EmergencyContactsRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilter filter)
    {
        var query = _context.EmergencyContacts.AsNoTracking();
        
        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.Name != null) query = query.Where(x => x.Name.Contains(filter.Name));
        if (filter.Phone != null) query = query.Where(x => x.Phone.Contains(filter.Phone));
        if (filter.PeopleId != null) query = query.Where(x => x.PeopleId == filter.PeopleId);
        query = query.OrderBy(x => x.Name);

        var contacts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return contacts;
    }
}
