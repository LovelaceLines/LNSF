using LNSF.Domain.Entities;
using LNSF.Domain.Filters;

namespace LNSF.Application.Interfaces;

public interface IFamilyGroupProfileService
{
    Task<List<FamilyGroupProfile>> Query(FamilyGroupProfileFilter filter);
    Task<int> GetCount();
    Task<FamilyGroupProfile> Create(FamilyGroupProfile familyGroupProfile);
    Task<FamilyGroupProfile> Update(FamilyGroupProfile familyGroupProfile);
    Task<FamilyGroupProfile> Delete(int id);
}
