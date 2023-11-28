using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;

namespace LNSF.Infra.Data.Repositories;

public class PeopleRoomRepository : BaseRepository<PeopleRoom>, IPeopleRoomRepository
{
    private readonly AppDbContext _context;

    public PeopleRoomRepository(AppDbContext context) : base(context) => 
        _context = context;
}
