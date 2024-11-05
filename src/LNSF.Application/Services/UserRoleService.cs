using LNSF.Application.Interfaces;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class UserRoleService(IUserRoleRepository repository,
	IUserRepository userRepository,
	IRoleRepository roleRepository) : IUserRoleService
{
	public async Task<UserRole> AddToRole(UserRole userRole)
	{
		if (!await userRepository.ExistsById(userRole.UserId)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
		if (!await roleRepository.ExistsById(userRole.RoleId)) throw new AppException("Permissão não encontrada!", HttpStatusCode.NotFound);

		if (await repository.ExistsByUserIdRoleId(userRole.UserId, userRole.RoleId)) throw new AppException("Usuário já possui esta permissão!", HttpStatusCode.Conflict);

		return await repository.Add(userRole);
	}

	public async Task<UserRole> RemoveFromRole(UserRole userRole)
	{
		if (!await repository.ExistsByUserIdRoleId(userRole.UserId, userRole.RoleId)) throw new AppException("Usuário não está nesta função!", HttpStatusCode.BadRequest);
		userRole = await repository.GetByUserIdRoleId(userRole.UserId, userRole.RoleId);

		return await repository.Remove(userRole);
	}
}
