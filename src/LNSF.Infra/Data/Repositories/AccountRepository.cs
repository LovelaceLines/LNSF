using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context) : base(context) => 
        _context = context;
    
    public async Task<List<Account>> Query(AccountFilter filter)
    {
        var query = _context.Accounts.AsNoTracking();
        var count = await query.CountAsync();

        if (filter.UserName != null) query = query.Where(x => x.UserName.Contains(filter.UserName));
        if (filter.Role != null) query = query.Where(x => x.Role == filter.Role);
        if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.UserName);
        else query = query.OrderBy(x => x.UserName);

        var accounts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return accounts;
    }

    public async Task<Account> Get(string userName, string Password)
    {
        var accounts = await _context.Accounts.AsNoTracking()
            .Where(x => x.UserName == userName && x.Password == Password)
            .ToListAsync();

        if (accounts.Count == 1) return accounts.First();
        throw new AppException("Usuário ou senha inválidos", HttpStatusCode.Unauthorized);
    }

    public async Task<bool> Exists(string userName, string password)
    {
        var accounts = await _context.Accounts.AsNoTracking()
            .Where(x => x.UserName == userName && x.Password == password)
            .ToListAsync();

        return accounts.Count == 1;
    }
}
