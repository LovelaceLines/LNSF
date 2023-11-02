using LNSF.Domain.Repositories;
using LNSF.Domain.Entities;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using LNSF.Domain.Filters;
namespace LNSF.Infra.Data.Repositories;

    public class TreatmentRepository : BaseRepository<Treatment>, ITreatmentRepository
    {
        private readonly AppDbContext _context;
        public TreatmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Treatment>> Query(TreatmentFilter filter)
        {
            var query = _context.Treatments.AsQueryable();
            if (filter.Id != null)
            {
                query = query.Where(x => x.Id == filter.Id);
            }
            if (filter.Name != null)
            {
                query = query.Where(x => x.Name == filter.Name);
            }
            if (filter.Type != null) 
            {
                query = query.Where(x => x.Type == filter.Type);
            }
            var treatments = await query
            .Skip((filter.Page.Page -1 ) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();
            return treatments;
            
        }
    }
