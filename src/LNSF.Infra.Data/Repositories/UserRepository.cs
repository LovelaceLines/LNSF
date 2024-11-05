using AutoFilterer.Extensions;
using AutoMapper;
using LNSF.Domain.DTOs;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using LNSF.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LNSF.Infra.Data.Repositories;

public class UserRepository(AppDbContext context,
	IMapper mapper,
	UserManager<User> userManager) : BaseRepository<User>(context), IUserRepository
{
	public async Task<QueryResult<UserDTO>> Query(UserFilter filter)
	{
		var query = context.Users.ApplyFilterWithoutPagination(filter);

		if (filter.Roles != null)
			query = query
				.Join(context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
				.Join(context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur.u, r })
				.Where(ur => ur.r.Name.ToLower().Contains(filter.Roles.ToLower()))
				.Select(ur => ur.u);

		var items = await query.ToPaged(filter.Page, filter.PerPage).ToListAsync();
		var totalCount = await query.CountAsync();

		var userDTOs = mapper.Map<List<UserDTO>>(items);
		userDTOs.ForEach(async u => u.Roles = await GetRoles(u));

		return new QueryResult<UserDTO>(items: userDTOs, totalCount: totalCount);
	}

	public async Task<User> Auth(string userName, string password)
	{
		if (!await ExistsByUserName(userName)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);

		var user = await GetByUserName(userName);

		if (!await CheckPassword(user, password)) throw new AppException("Erro ao fazer login!", HttpStatusCode.Unauthorized);

		return user;
	}

	public async Task<bool> CheckPassword(User user, string password) =>
		await userManager.CheckPasswordAsync(user, password);

	public async Task<bool> ExistsById(int id, int companyId) =>
		await userManager.Users.AsNoTracking().AnyAsync(u => u.Id == id);

	public async Task<bool> ExistsByUserName(string userName) =>
		await userManager.Users.AsNoTracking().AnyAsync(u => u.UserName == userName);

	public async Task<bool> ExistsByEmail(string email) =>
		await userManager.Users.AsNoTracking().AnyAsync(u => u.Email == email);

	public async Task<bool> ExistsByPhoneNumber(string phoneNumber) =>
		await userManager.Users.AsNoTracking().AnyAsync(u => u.PhoneNumber == phoneNumber);

	public async Task<User> GetById(int id) =>
		await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id) ??
			throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

	public async Task<User> GetByUserName(string userName) =>
		await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName) ??
			throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

	public async Task<List<Role>> GetRoles(User user) =>
		await context.UserRoles
			.Where(ur => ur.UserId == user.Id)
			.Join(context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
			.ToListAsync();

	public async Task<User> Add(User user, string password)
	{
		var result = await userManager.CreateAsync(user, password);

		if (!result.Succeeded) CatchError(new Exception(result.Errors.First().Description));

		return user;
	}

	public new async Task<User> Update(User user)
	{
		var result = await userManager.UpdateAsync(user);

		if (!result.Succeeded) CatchError(new Exception(result.Errors.First().Description));

		return user;
	}

	public async Task<bool> UpdatePassword(User user, string oldPassword, string newPassword)
	{
		var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

		if (!result.Succeeded) throw new AppException(result.Errors.First().Description, HttpStatusCode.BadRequest);

		return true;
	}
}
