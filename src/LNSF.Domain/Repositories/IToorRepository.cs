using LNSF.Domain.Entities;
using LNSF.Domain.Views;

namespace LNSF.Domain.Repositories;

public interface IToorRepository
{
    public Task<List<Toor>> Get();
    public Task<Toor> Get(int id);
    public Task<Toor> AddOutput(IToorOutput output);
    public Task<Toor> AddInput(IToorInput input);
}
