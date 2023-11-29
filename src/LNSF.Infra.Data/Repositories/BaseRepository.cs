﻿using System.Net;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LNSF.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context) => 
        _context = context;

    public virtual async Task<List<T>> Get() => 
        await _context.Set<T>().AsNoTracking().ToListAsync();

    public virtual async Task<T> Get(dynamic id) => 
        await _context.Set<T>().FindAsync(id) ??
            throw new AppException("Entidade não encontrada!", HttpStatusCode.NotFound);

    public virtual async Task<bool> Exists(params object?[]? keyValues) => 
        await _context.Set<T>().FindAsync(keyValues) != null;

    public virtual async Task<int> GetCount() => 
        await _context.Set<T>().CountAsync();

    public virtual async Task<T> Add(T entity)
    {
        try
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        
            return entity;
        }
        catch (Exception ex) { throw CatchError(ex); }
    }

    public virtual async Task<T> Update(T entity)
    {
        try 
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        
            return entity;
        }
        catch (Exception ex) { throw CatchError(ex); } 
    }

    public virtual async Task<T> Remove(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<T> Remove(dynamic id)
    {
        T entity = await Get(id);
        _context.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task BeguinTransaction() => 
        await _context.Database.BeginTransactionAsync();

    public async Task CommitTransaction() => 
        await _context.Database.CommitTransactionAsync();

    public async Task RollbackTransaction() => 
        await _context.Database.RollbackTransactionAsync();
    
    private Exception CatchError(Exception ex)
    {
        var message = ex.InnerException?.Message ?? ex.Message;

        if (message.Contains("UNIQUE constraint")) throw new AppException("Valor já existe!", HttpStatusCode.Conflict);
        throw new Exception();
    }
}
