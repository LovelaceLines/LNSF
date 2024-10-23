using System.Net;

using LNSF.Application.Interfaces;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class UserRoleService(IUserRoleRepository repository, IUserRepository userRepository, IRoleRepository roleRepository) : IUserRoleService
{
	public async Task<User> AddToRole(int userId, int roleId)
	{
		if (!await userRepository.ExistsById(userId)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
		if (!await roleRepository.ExistsById(roleId)) throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

		var user = await userRepository.GetById(userId);
		if (await repository.ExistsById(userId, roleId)) throw new AppException("Usuário já possui este perfil!", HttpStatusCode.Conflict);

		await repository.Add(userId, roleId);

		return user;
	}

	public async Task<bool> RemoveFromRole(int userId, int roleId)
	{
		if (!await repository.Exists(userId, roleId)) throw new AppException("Usuário não está nesta função!", HttpStatusCode.BadRequest);

		var userRole = await repository.Get(userId, roleId);

		await repository.Remove(userRole);

		return true;
	}
}
