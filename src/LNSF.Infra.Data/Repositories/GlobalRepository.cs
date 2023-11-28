using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;

namespace LNSF.Infra.Data;

public class GlobalRepository : IGlobalRepository
{
    private readonly AppDbContext _context;

    public GlobalRepository(AppDbContext context) => 
        _context = context;
}
