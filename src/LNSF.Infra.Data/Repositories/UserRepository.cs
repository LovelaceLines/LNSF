using System.Net;
using LNSF.Domain.Enums;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LNSF.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IdentityUserRole<string> _userRoleManager;

    public UserRepository(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IdentityUserRole<string> userRoleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userRoleManager = userRoleManager;
    }

    public async Task<List<IdentityUser>> Query(UserFilter filter)
    {
        var query = _userManager.Users.AsNoTracking();

        if (!filter.Id.IsNullOrEmpty()) query = query.Where(u => u.Id == filter.Id);
        if (!filter.UserName.IsNullOrEmpty()) query = query.Where(u => u.UserName != null && u.UserName.ToLower().Contains(filter.UserName!.ToLower()));
        if (!filter.Email.IsNullOrEmpty()) query = query.Where(u => u.Email != null && u.Email.ToLower().Contains(filter.Email!.ToLower()));
        if (filter.Role.HasValue) query = query.Where(u =>
            _roleManager.Roles.Any(r => r.Name == filter.Role.ToString() && 
                _userRoleManager.RoleId == r.Id && _userRoleManager.UserId == u.Id));
        
        if (filter.OrderBy == OrderBy.Ascending) query = query.OrderBy(u => u.UserName);
        else query = query.OrderByDescending(u => u.UserName);

        var users = await query
            .Skip((filter.Page?.Page -1 ) * filter.Page?.PageSize ?? 0)
            .Take(filter.Page?.PageSize ?? 0)
            .ToListAsync();

        return users;
    }

    public async Task<IdentityUser> Auth(string userName, string password)
    {
        if (!await ExistsByUserName(userName)) throw new AppException("Erro ao autenticar!", HttpStatusCode.Unauthorized);
        var user = await GetByUserName(userName);

        if (!await CheckPassword(user, password)) throw new AppException("Erro ao autenticar!", HttpStatusCode.Unauthorized); 

        return user;
    }

    public async Task<int> GetCount() => 
        await _userManager.Users.AsNoTracking().CountAsync();

    public async Task<bool> ExistsById(string id) => 
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.Id == id);
    
    public async Task<bool> ExistsByUserName(string userName) =>
        await _userManager.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);
    
    public Task<bool> ExistsByEmail(string email) => 
        _userManager.Users.AsNoTracking().AnyAsync(u => u.Email == email);

    public Task<bool> ExistsByPhoneNumber(string phoneNumber) => 
        _userManager.Users.AsNoTracking().AnyAsync(u => u.PhoneNumber == phoneNumber);
    
    public async Task<IdentityUser> GetById(string id) => 
        await _userManager.FindByIdAsync(id) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
    
    public async Task<IdentityUser> GetByUserName(string userName) =>
        await _userManager.FindByNameAsync(userName) ?? throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
    
    public async Task<List<string>> GetRoles(IdentityUser user) =>
        (List<string>)await _userManager.GetRolesAsync(user);

    public async Task<bool> CheckPassword(string id, string password) => 
        await _userManager.CheckPasswordAsync(await GetById(id), password);
    
    public async Task<bool> CheckPassword(IdentityUser user, string password) =>
        await _userManager.CheckPasswordAsync(user, password);

    public async Task<IdentityUser> Add(IdentityUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return user;
    }

    public async Task<IdentityUser> Update(IdentityUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        
        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return user;
    }

    public async Task<IdentityUser> UpdatePassword(string id, string oldPassword, string newPassword)
    {
        var user = await GetById(id);
        var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

        return user;
    }

    public async Task<IdentityUser> Remove(dynamic id)
    {
        var user = await GetById(id);
        var result = await _userManager.DeleteAsync(user) as IdentityResult;

        if (!result!.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);
        
        return user;
    }
}
