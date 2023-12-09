using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IPeopleService
{
    Task<List<People>> Query(PeopleFilter filter);
    Task<int> GetCount();
    Task<People> Create(People people);
    Task<People> Update(People people);
}
