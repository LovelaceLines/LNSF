namespace LNSF.Domain.Repositories;

public interface IBaseRepository<T> where T : class
{
    public Task<List<T>> Get();
    public Task<T> Get(int id);
    public Task<bool> Exists(int id);
    public Task<int> GetQuantity();
    public Task<T> Post(T entity);
    public Task<T> Put(T entity);
    public Task<T> Delete(T entity);
    public Task<T> Delete(int id);
    
    public Task BeguinTransaction();
    public Task CommitTransaction();
    public Task RollbackTransaction();
}
