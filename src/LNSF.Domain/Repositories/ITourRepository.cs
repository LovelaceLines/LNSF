using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface ITourRepository
{
    public Task<ResultDTO<List<Tour>>> Get(Pagination pagination);
    public Task<ResultDTO<Tour>> Get(int id);
    public Task<ResultDTO<int>> GetQuantity();
    public Task<ResultDTO<Tour>> PostOutput(Tour tour);
    public Task<ResultDTO<Tour>> PutInput(Tour tour);
}
