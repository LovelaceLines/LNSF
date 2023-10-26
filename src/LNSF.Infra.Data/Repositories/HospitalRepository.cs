﻿using LNSF.Domain.Entities;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class HospitalRepository : BaseRepository<Hospital>, IHospitalRepository
{
    private readonly AppDbContext _context;
    public HospitalRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<List<Hospital>> Query(HospitalFilter filter)
    {
        var query = _context.Hospitals.AsNoTracking();
        
        if (filter.Id != null) query = query.Where(x => x.Id == filter.Id);
        if (filter.Name != null) query = query.Where(x => x.Name.Contains(filter.Name));
        if (filter.Acronym != null) query = query.Where(x => x.Acronym != null && x.Acronym.Contains(filter.Acronym));
        query = query.OrderBy(x => x.Name);

        var hospitals = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return hospitals;
    }
}
