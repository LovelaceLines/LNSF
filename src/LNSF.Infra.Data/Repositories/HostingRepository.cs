using System.Net;
using LNSF.Domain.Entities;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HostingRepository : BaseRepository<Hosting>, IHostingRepository
{
    private readonly AppDbContext _context;
    private readonly IHostingEscortRepository _hostingEscortRepository;

    public HostingRepository(AppDbContext context, 
        IHostingEscortRepository hostingEscortRepository) : base(context)
    {
        _context = context;
        _hostingEscortRepository = hostingEscortRepository;
    }

    public async Task<List<Hosting>> Query(HostingFilter filter)
    {
        var query = _context.Hostings.AsNoTracking();

        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.PatientId != null) query = query.Where(x => x.PatientId == filter.PatientId);
        if (filter.CheckIn != null) query = query.Where(x => x.CheckIn >= filter.CheckIn);
        if (filter.CheckOut != null) query = query.Where(x => x.CheckOut <= filter.CheckOut);
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(x => x.Id);
        else query = query.OrderByDescending(x => x.Id);

        var hostings = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hostings;
    }

    public new async Task<Hosting> Add(Hosting hosting)
    {
        await BeguinTransaction();

        try
        {
            await _context.Hostings.AddAsync(hosting);
            await _context.SaveChangesAsync();

            foreach (var escortId in hosting.EscortsIds)
            {
                await _hostingEscortRepository.Add(new HostingEscort
                {
                    EscortId = escortId,
                    HostingId = hosting.Id
                });
            }
  
            await CommitTransaction();

            return hosting;
        }
        catch (Exception)
        {
            await RollbackTransaction();
            throw new AppException("Erro ao adicionar hospedagem", HttpStatusCode.BadRequest);
        }
    }

    public new async Task<Hosting> Update(Hosting hosting)
    {
        await BeguinTransaction();

        try
        {
            await _hostingEscortRepository.RemoveByHostingId(hosting.Id);

            _context.Hostings.Update(hosting);
            await _context.SaveChangesAsync();

            foreach (var escortId in hosting.EscortsIds)
            {
                await _hostingEscortRepository.Add(new HostingEscort
                {
                    EscortId = escortId,
                    HostingId = hosting.Id
                });
            }

            await _context.SaveChangesAsync();
            await CommitTransaction();

            return hosting;
        }
        catch (Exception)
        {
            await RollbackTransaction();
            throw new AppException("Erro ao atualizar hospedagem", HttpStatusCode.BadRequest);
        }
    }
}