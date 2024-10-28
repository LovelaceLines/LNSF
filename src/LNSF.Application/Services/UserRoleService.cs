using LNSF.Application.Interfaces;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class UserRoleService(IUserRoleRepository repository, IUserRepository userRepository, IRoleRepository roleRepository) : IUserRoleService
{
	public async Task<UserRole> AddToRole(int userId, int roleId)
	{
		if (!await userRepository.ExistsById(userId)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
		if (!await roleRepository.ExistsById(roleId)) throw new AppException("Permissão não encontrada!", HttpStatusCode.NotFound);

		if (await repository.ExistsById(userId, roleId)) throw new AppException("Usuário já possui esta permissão!", HttpStatusCode.Conflict);

		return await repository.Add(userId, roleId);
	}

	public async Task<UserRole> RemoveFromRole(int userId, int roleId)
	{
		if (!await repository.Exists(userId, roleId)) throw new AppException("Usuário não está nesta função!", HttpStatusCode.BadRequest);

		return await repository.Remove(new UserRole { UserId = userId, RoleId = roleId });
	}
}
