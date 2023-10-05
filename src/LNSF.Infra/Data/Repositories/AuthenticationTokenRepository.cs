using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class AuthenticationTokenRepository : BaseRepository<AuthenticationToken>, IAuthenticationTokenRepository
{
    private readonly AppDbContext _context;

    public AuthenticationTokenRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<AuthenticationToken> Get(string token, string refreshToken)
    {
        var authToken = await _context.AuthenticationTokens.AsNoTracking()
            .Where(x => x.Token == token && x.RefreshToken == refreshToken)
            .ToListAsync();
        
        if (authToken.Count != 1) throw new AppException("Token not found");
        return authToken.First();
    }
}
