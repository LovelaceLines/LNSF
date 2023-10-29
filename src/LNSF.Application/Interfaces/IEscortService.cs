using LNSF.Domain.Filters;
using LNSF.Domain.Entities;
namespace LNSF.Application.Interfaces;

public interface IEscortService
 {
    Task<List<Escort>> Query(EscortFilter filter);
    Task<int> GetCount();
    Task<Escort> Create(Escort escort);
    Task<Escort> Update(Escort escort);
}
