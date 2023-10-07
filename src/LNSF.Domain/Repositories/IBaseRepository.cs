namespace LNSF.Domain.Repositories;

public interface IBaseRepository<T> where T : class
{
    public Task<List<T>> Get();
    public Task<T> Get(int id);
    public Task<bool> Exists(int id);
    public Task<int> GetCount();
    public Task<T> Add(T entity);
    public Task<T> Update(T entity);
    public Task<T> Remove(T entity);
    public Task<T> Remove(int id);
    
    public Task BeguinTransaction();
    public Task CommitTransaction();
    public Task RollbackTransaction();
}
