using LNSF.Domain.Filters;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Domain.Repositories;

public interface IUserRepository
{
    Task<List<IdentityUser>> Query(UserFilter filter);
    Task<int> GetCount();
    Task<bool> ExistsById(string id);
    Task<bool> ExistsByUserName(string userName);
    Task<bool> ExistsByEmail(string email);
    Task<bool> ExistsByPhoneNumber(string phoneNumber);
    Task<IdentityUser> GetById(string id);
    Task<bool> CheckPassword(string id, string password);
    Task<IdentityUser> Add(IdentityUser user, string password);
    Task<IdentityUser> Update(IdentityUser user);
    Task<IdentityUser> UpdatePassword(string id, string oldPassword, string newPassword);
    Task<IdentityUser> Remove(dynamic id);
}
