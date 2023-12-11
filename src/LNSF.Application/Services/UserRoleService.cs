using LNSF.Application.Interfaces;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository) => 
        _userRoleRepository = userRoleRepository;

    public Task<List<IdentityUserRole<string>>> Query(UserRoleFilter filter) =>
        _userRoleRepository.Query(filter);
        
    public async Task<int> GetCount() =>
        await _userRoleRepository.GetCount();
}
