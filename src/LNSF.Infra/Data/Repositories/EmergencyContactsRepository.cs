using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EmergencyContactsRepository : BaseRepository<EmergencyContact>, IEmergencyContactsRepository
{
    private readonly AppDbContext _context;

    public EmergencyContactsRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilters filters)
    {
        var query = _context.EmergencyContacts.AsNoTracking();
        
        if (filters.Id != null) query = query.Where(x => x.Id == filters.Id);
        if (filters.Name != null) query = query.Where(x => x.Name.Contains(filters.Name));
        if (filters.Phone != null) query = query.Where(x => x.Phone.Contains(filters.Phone));
        if (filters.PeopleId != null) query = query.Where(x => x.PeopleId == filters.PeopleId);
        query = query.OrderBy(x => x.Name);

        var contacts = await query
            .Skip((filters.Page.Page - 1) * filters.Page.PageSize)
            .Take(filters.Page.PageSize)
            .ToListAsync();

        return contacts;
    }
}
