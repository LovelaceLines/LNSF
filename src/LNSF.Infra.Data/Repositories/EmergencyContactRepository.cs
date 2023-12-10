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

    public EmergencyContactRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _emergencyContacts = _context.EmergencyContacts.AsNoTracking();
    }

    public async Task<List<EmergencyContact>> Query(EmergencyContactFilter filter)
    {
        var query = _emergencyContacts;
        
        if (filter.Id.HasValue) query = query.Where(x => x.Id == filter.Id);
        if (!filter.Name.IsNullOrEmpty()) query = query.Where(x => x.Name.ToLower().Contains(filter.Name!.ToLower()));
        if (!filter.Phone.IsNullOrEmpty()) query = query.Where(x => x.Phone.Contains(filter.Phone!));
        if (filter.PeopleId.HasValue) query = query.Where(x => x.PeopleId == filter.PeopleId);

        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Name);
        else if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.Name);
        
        var contacts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return contacts;
    }

    public async Task<bool> ExistsByIdAndPeopleId(int id, int peopleId) => 
        await _emergencyContacts.AnyAsync(x => x.Id == id && x.PeopleId == peopleId);
}
