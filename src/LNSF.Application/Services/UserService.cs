using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LNSF.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly PasswordValidator _passwordValidator;
    private readonly UserValidator _userValidator;

    public UserService(IUserRepository userRepository,
        PasswordValidator passwordValidator,
        UserValidator userValidator,
        IUserRoleRepository userRoleRepository,
        IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _passwordValidator = passwordValidator;
        _userValidator = userValidator;
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
    }

    public Task<List<IdentityUser>> Query(UserFilter filter) => 
        _userRepository.Query(filter);

    public async Task<int> GetCount() =>
        await _userRepository.GetCount();

    public async Task<IdentityUser> Create(IdentityUser user, string password)
    {
        var validationResult = await _userValidator.ValidateAsync(user);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        validationResult = await _passwordValidator.ValidateAsync(password);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (await _userRepository.ExistsByUserName(user.UserName!)) throw new AppException("Nome de usuário já existe!", HttpStatusCode.BadRequest);
        if (await _userRepository.ExistsByEmail(user.Email!)) throw new AppException("Email já existe!", HttpStatusCode.BadRequest);
        if (await _userRepository.ExistsByPhoneNumber(user.PhoneNumber!)) throw new AppException("Telefone já existe!", HttpStatusCode.BadRequest);

        return await _userRepository.Add(user, password);
    }

    public async Task<IdentityUser> AddToRole(string userId, string role)
    {
        if (!await _userRepository.ExistsById(userId)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
        if (!await _roleRepository.ExistsByName(role)) throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

        var user = await _userRepository.GetById(userId);
        if (await _userRoleRepository.ExistsByUserAndRoleName(user, role)) throw new AppException("Usuário já possui este perfil!", HttpStatusCode.BadRequest);

        await _userRoleRepository.Add(user, role);
        
        return user;
    }

    public async Task<IdentityUser> Update(IdentityUser newUser)
    {
        var validationResult = _userValidator.Validate(newUser);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _userRepository.ExistsById(newUser.Id)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
        var oldUser = await _userRepository.GetById(newUser.Id);

        if (oldUser.UserName != newUser.UserName && await _userRepository.ExistsByUserName(newUser.UserName!)) throw new AppException("Nome de usuário já existe!", HttpStatusCode.BadRequest);
        if (oldUser.Email != newUser.Email && await _userRepository.ExistsByEmail(newUser.Email!)) throw new AppException("Email já existe!", HttpStatusCode.BadRequest);
        if (oldUser.PhoneNumber != newUser.PhoneNumber && await _userRepository.ExistsByPhoneNumber(newUser.PhoneNumber!)) throw new AppException("Telefone já existe!", HttpStatusCode.BadRequest);
        
        oldUser.UserName = newUser.UserName;
        oldUser.Email = newUser.Email;
        oldUser.PhoneNumber = newUser.PhoneNumber;

        return await _userRepository.Update(oldUser);
    }

    public async Task<IdentityUser> UpdatePassword(string id, string oldPassword, string newPassword)
    {
        var validationResult = await _passwordValidator.ValidateAsync(newPassword);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);

        if (!await _userRepository.CheckPassword(id, oldPassword))
            throw new AppException("Senha incorreta!", HttpStatusCode.BadRequest);
            
        return await _userRepository.UpdatePassword(id, oldPassword, newPassword);
    }

    public async Task<IdentityUser> Delete(string id)
    {
        if (!await _userRepository.ExistsById(id)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

        return await _userRepository.Remove(id);
    }

    public async Task<IdentityUser> RemoveFromRole(string userId, string role)
    {
        if (!await _userRepository.ExistsById(userId)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
        if (!await _roleRepository.ExistsByName(role)) throw new AppException("Perfil não encontrado!", HttpStatusCode.NotFound);

        var user = await _userRepository.GetById(userId);
        if (!await _userRoleRepository.ExistsByUserAndRoleName(user, role)) throw new AppException("Usuário não possui este perfil!", HttpStatusCode.BadRequest);

        await _userRoleRepository.Remove(user, role);

        return user;
    }
}
