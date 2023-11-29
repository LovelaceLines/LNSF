using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPeopleService
{
    public Task<List<People>> Query(PeopleFilter filter);
    public Task<int> GetCount();
    public Task<People> Create(People people);
    public Task<People> Update(People people);
}
