using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IBaseService<T>
{
	virtual Task<QueryResult<T>> Query(BaseFilter filter)
	{
		throw new NotImplementedException();
	}

	virtual Task<T> Create(T entity)
	{
		throw new NotImplementedException();
	}

	virtual Task<T> Update(T entity)
	{
		throw new NotImplementedException();
	}

	virtual Task<T> Delete(int id)
	{
		throw new NotImplementedException();
	}
}
