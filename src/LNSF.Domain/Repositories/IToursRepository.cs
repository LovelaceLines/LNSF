using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface IToursRepository
{
    public Task<ResultDTO<List<Tour>>> Get(Pagination pagination);
    public Task<ResultDTO<Tour>> Get(int id);
    public Task<ResultDTO<int>> GetQuantity();
    public Task<ResultDTO<Tour>> Post(Tour tour);
    public Task<ResultDTO<Tour>> Put(Tour tour);
}
