using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface IPeoplesRepository
{
    public Task<ResultDTO<List<People>>> Get(Pagination pagination);
    public Task<ResultDTO<People>> Get(int id);
    public Task<ResultDTO<int>> GetQuantity();
    public Task<ResultDTO<People>> Post(People people);
    public Task<ResultDTO<People>> Put(People people);
}
