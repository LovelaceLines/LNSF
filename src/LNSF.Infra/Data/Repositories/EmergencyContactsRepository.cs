using LNSF.Domain.Repositories;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class EmergencyContactsRepository : IEmergencyContactsRepository
{
    private readonly AppDbContext _context;

    public EmergencyContactsRepository(AppDbContext context) => 
        _context = context;

    public async Task<ResultDTO<List<EmergencyContact>>> Get(Pagination pagination)
    {
        var query = _context.EmergencyContacts.AsNoTracking();
        var count = await query.CountAsync();

        if (count == 0) return new ResultDTO<List<EmergencyContact>>("Não encontrado");
        
        var contacts = await query
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new ResultDTO<List<EmergencyContact>>(contacts);
    }

    public async Task<ResultDTO<EmergencyContact>> Get(int id)
    {
        var contact = await _context.EmergencyContacts.FindAsync(id);

        return (contact == null) ?
            new ResultDTO<EmergencyContact>("Não encontrado.") :
            new ResultDTO<EmergencyContact>(contact);
    }

    public async Task<ResultDTO<int>> GetQuantity() => 
        new ResultDTO<int>(await _context.EmergencyContacts.CountAsync());

    public async Task<ResultDTO<EmergencyContact>> Post(EmergencyContact emergencyContact)
    {
        _context.EmergencyContacts.Add(emergencyContact);
        await _context.SaveChangesAsync();
        
        return new ResultDTO<EmergencyContact>(emergencyContact);
    }

    public async Task<ResultDTO<EmergencyContact>> Put(EmergencyContact emergencyContact)
    {
        var _emergencyContact = await _context.EmergencyContacts.FindAsync(emergencyContact.Id);

        if (_emergencyContact == null) return new ResultDTO<EmergencyContact>("Não encontrado.");

        _context.Entry(_emergencyContact).CurrentValues.SetValues(emergencyContact);

        _context.EmergencyContacts.Update(_emergencyContact);
        await _context.SaveChangesAsync();

        return new ResultDTO<EmergencyContact>(_emergencyContact);
    }
}
