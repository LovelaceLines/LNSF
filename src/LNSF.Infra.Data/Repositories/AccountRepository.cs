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

        if (filter.UserName != null) query = query.Where(x => x.UserName.ToLower().Contains(filter.UserName.ToLower()));
        if (filter.Role != null) query = query.Where(x => x.Role == filter.Role);
        if (filter.OrderBy == OrderBy.Descending) query = query.OrderByDescending(x => x.UserName);
        else query = query.OrderBy(x => x.UserName);

        var accounts = await query
            .Skip((filter.Page.Page - 1) * filter.Page.PageSize)
            .Take(filter.Page.PageSize)
            .ToListAsync();

        return accounts;
    }

    public async Task<Account> GetById(string id)
    {
        var account = await _context.Accounts.AsNoTracking()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (account == null) throw new AppException("Conta não encontrada!", HttpStatusCode.NotFound);
        return account;
    }

    public async Task<Account> GetByUserName(string userName)
    {
        var accounts = await _context.Accounts.AsNoTracking()
            .Where(x => x.UserName == userName)
            .ToListAsync();

        if (accounts.Count == 1) return accounts.First();
        throw new AppException("Usuário inválidos!", HttpStatusCode.Unauthorized);
    }

    public async Task<bool> ExistsId(string id)
    {
        var accounts = await _context.Accounts.AsNoTracking()
            .Where(x => x.Id == id)
            .ToListAsync();

        return accounts.Count == 1;
    }

    public async Task<bool> Exists(string userName, string password)
    {
        var accounts = await _context.Accounts.AsNoTracking()
            .Where(x => x.UserName == userName && VerifyPassword(password, x.Password))
            .ToListAsync();

        return accounts.Count == 1;
    }

    public async Task<bool> Exists(string userName)
    {
        var accounts = await _context.Accounts.AsNoTracking()
            .Where(x => x.UserName == userName)
            .ToListAsync();

        return accounts.Count == 1;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }

    public async Task<Account> Auth(string userName, string password)
    {
        var account = await _context.Accounts.AsNoTracking()
            .Where(x => x.UserName == userName)
            .FirstOrDefaultAsync() ?? throw new AppException("Usuário ou senha inválidos!", HttpStatusCode.Unauthorized);

        return !VerifyPassword(password, account.Password)
            ? throw new AppException("Usuário ou senha inválidos!", HttpStatusCode.Unauthorized)
            : account;
    }
}
