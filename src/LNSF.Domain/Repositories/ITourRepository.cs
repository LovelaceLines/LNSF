using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface ITourRepository
{
    public Task<List<Tour>> Get();
    public Task<Tour> Get(int id);
    public Task<Tour> AddOutput(ITourOutput output);
    public Task<Tour> AddInput(ITourInput input);
}
