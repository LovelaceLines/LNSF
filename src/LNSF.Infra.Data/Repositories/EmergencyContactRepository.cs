using LNSF.Domain.Repositories;
using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EmergencyContactRepository : BaseRepository<EmergencyContact>, IEmergencyContactRepository
{
    private readonly AppDbContext _context;

    public EmergencyContactRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilter filter)
    {
        var query = _context.EmergencyContacts.AsNoTracking();
        
        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.Name != null) query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
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
