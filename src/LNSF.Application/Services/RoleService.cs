using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class RoleService(IRoleRepository repository,
	RoleValidator validator) : IRoleService
{
	public Task<Role> GetById(int id) =>
		repository.GetById(id);

	public Task<Role> GetByName(string name) =>
		repository.GetByName(name);

	public Task<List<Role>> GetByUser(int id) =>
		repository.GetByUser(id);

	public async Task<Role> Create(Role role)
	{
		var validationResult = validator.Validate(role);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (await repository.ExistsByName(role.Name)) throw new AppException("Permissão já existe!", HttpStatusCode.Conflict);

		return await repository.Add(role);
	}

	public async Task<Role> Update(Role newRole)
	{
		if (newRole.Name == "Desenvolvedor") throw new AppException("Permissão Desenvolvedor não pode ser removido!", HttpStatusCode.BadRequest);
		if (newRole.Name == "Administrador") throw new AppException("Permissão Administrador não pode ser removido!", HttpStatusCode.BadRequest);
		if (newRole.Name == "Assistente Social") throw new AppException("Permissão Assistente Social não pode ser removido!", HttpStatusCode.BadRequest);
		if (newRole.Name == "Secretário") throw new AppException("Permissão Secretário não pode ser removido!", HttpStatusCode.BadRequest);
		if (newRole.Name == "Voluntário") throw new AppException("Permissão Voluntário não pode ser removido!", HttpStatusCode.BadRequest);

		var validationResult = validator.Validate(newRole);
		if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

		if (!await repository.ExistsById(newRole.Id)) throw new AppException("Permissão não encontrada!", HttpStatusCode.NotFound);
		var oldRole = await repository.GetById(newRole.Id);

		if (oldRole.Name != newRole.Name && await repository.ExistsByName(newRole.Name)) throw new AppException("Permissão já existe!", HttpStatusCode.Conflict);

		oldRole.Name = newRole.Name;

		return await repository.Update(oldRole);
	}

	public async Task<Role> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Permissão não encontrada!", HttpStatusCode.NotFound);
		var role = await repository.GetById(id);

		if (role.Name == "Desenvolvedor") throw new AppException("Permissão Desenvolvedor não pode ser removido!", HttpStatusCode.BadRequest);
		if (role.Name == "Administrador") throw new AppException("Permissão Administrador não pode ser removido!", HttpStatusCode.BadRequest);
		if (role.Name == "Assistente Social") throw new AppException("Permissão Assistente Social não pode ser removido!", HttpStatusCode.BadRequest);
		if (role.Name == "Secretário") throw new AppException("Permissão Secretário não pode ser removido!", HttpStatusCode.BadRequest);
		if (role.Name == "Voluntário") throw new AppException("Permissão Voluntário não pode ser removido!", HttpStatusCode.BadRequest);

		return await repository.Remove(role);
	}
}
