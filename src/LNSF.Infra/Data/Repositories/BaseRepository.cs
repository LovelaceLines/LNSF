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

    public virtual async Task<T> Get(int id) => 
        await _context.Set<T>().FindAsync(id) ??
            throw new AppException("Entidade não encontrada");

    public virtual async Task<int> GetQuantity() => 
        await _context.Set<T>().CountAsync();

    public virtual async Task<T> Post(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<T> Put(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<T> Delete(T entity)
    {
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
}
