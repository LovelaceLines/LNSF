using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using LNSF.Domain.Exceptions;

namespace LNSF.Infra.Data.Repositories;

public class EmergencyContactsRepository : BaseRepository<EmergencyContact>, IEmergencyContactsRepository
{
    private readonly AppDbContext _context;

    public EmergencyContactsRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<EmergencyContact>> Get(EmergencyContactFilters filters)
    {
        var query = _context.EmergencyContacts.AsNoTracking();

        if (filters.PeopleId != 0) query = query.Where(x => x.PeopleId == filters.PeopleId);
        query = query.OrderBy(x => x.Name);

        var contacts = await query.ToListAsync();
        
        return contacts;
    }

    public async Task<EmergencyContact> Get(int peopleId, string phone)
    {
        var contact = await _context.EmergencyContacts
            .Where(c => c.Phone == phone)
            .Where(c => c.PeopleId == peopleId)
            .FirstOrDefaultAsync() ?? 
                throw new AppException("Contato de emergência não encontrado");

        return contact;
    }
}
