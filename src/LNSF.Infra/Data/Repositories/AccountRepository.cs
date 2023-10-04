﻿using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context) : base(context) => 
        _context = context;

    public async Task<bool> Exist(Account account)
    {
        var accounts = await _context.Accounts.AsNoTracking()
            .Where(x => x.Role == account.Role && x.Password == account.Password)
            .ToListAsync();

        if (accounts.Count == 1) return true;
        return false;
    }
}
