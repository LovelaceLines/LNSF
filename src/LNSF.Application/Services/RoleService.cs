using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace LNSF.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly RoleValidator _roleValidator;

    public RoleService(IRoleRepository roleRepository, 
        RoleValidator roleValidator)
    {
        _roleRepository = roleRepository;
        _roleValidator = roleValidator;
    }

    public Task<List<IdentityRole>> Query(RoleFilter filter) => 
        _roleRepository.Query(filter);

    public async Task<int> GetCount() => 
        await _roleRepository.GetCount();

    public async Task<IdentityRole> Add(IdentityRole role)
    {
        var validationResult = _roleValidator.Validate(role);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (await _roleRepository.ExistsByName(role.Name!)) throw new AppException("Perfil já existe!", HttpStatusCode.Conflict);

        return await _roleRepository.Add(role);
    }

    public async Task<IdentityRole> Update(IdentityRole newRole)
    {
        if (newRole.Name == "Desenvolvedor") throw new AppException("Perfil Desenvolvedor não pode ser removido!", HttpStatusCode.BadRequest);
        if (newRole.Name == "Administrador") throw new AppException("Perfil Administrador não pode ser removido!", HttpStatusCode.BadRequest);
        if (newRole.Name == "Assistente Social") throw new AppException("Perfil Assistente Social não pode ser removido!", HttpStatusCode.BadRequest);
        if (newRole.Name == "Secretário") throw new AppException("Perfil Secretário não pode ser removido!", HttpStatusCode.BadRequest);
        if (newRole.Name == "Voluntário") throw new AppException("Perfil Voluntário não pode ser removido!", HttpStatusCode.BadRequest);

        var validationResult = _roleValidator.Validate(newRole);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _roleRepository.ExistsByName(newRole.Name!)) throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

        var oldRole = await _roleRepository.GetByName(newRole.Id);
        oldRole.Name = newRole.Name;

        return await _roleRepository.Update(oldRole);
    }

    public async Task<IdentityRole> Delete(string name)
    {
        if (name == "Desenvolvedor") throw new AppException("Perfil Desenvolvedor não pode ser removido!", HttpStatusCode.BadRequest);
        if (name == "Administrador") throw new AppException("Perfil Administrador não pode ser removido!", HttpStatusCode.BadRequest);
        if (name == "Assistente Social") throw new AppException("Perfil Assistente Social não pode ser removido!", HttpStatusCode.BadRequest);
        if (name == "Secretário") throw new AppException("Perfil Secretário não pode ser removido!", HttpStatusCode.BadRequest);
        if (name == "Voluntário") throw new AppException("Perfil Voluntário não pode ser removido!", HttpStatusCode.BadRequest);

        if (!await _roleRepository.ExistsByName(name)) throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);
        
        return await _roleRepository.Remove(name);
    }
}
