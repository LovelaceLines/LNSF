using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Repositories;
using System.Net;

namespace LNSF.Application.Services;

public class UserService(IUserRepository repository,
	PasswordValidator passwordValidator,
	UserValidator validator) : IUserService
{
	public Task<User> GetById(int id) =>
		repository.GetById(id);

	public Task<User> GetByUserName(string userName) =>
		repository.GetByUserName(userName);

	public async Task<User> Create(User user, string password)
	{
		var validationResult = validator.Validate(user);
		if (!validationResult.IsValid)
			throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

		validationResult = passwordValidator.Validate(password);
		if (!validationResult.IsValid)
			throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

		if (await repository.ExistsByUserName(user.UserName)) throw new AppException("Nome de usuário já existe!", HttpStatusCode.Conflict);
		if (await repository.ExistsByEmail(user.Email)) throw new AppException("Email já existe!", HttpStatusCode.Conflict);
		if (await repository.ExistsByPhoneNumber(user.PhoneNumber)) throw new AppException("Telefone já existe!", HttpStatusCode.Conflict);

		return await repository.Add(user, password);
	}

	public async Task<User> Update(User newUser)
	{
		var validationResult = validator.Validate(newUser);
		if (!validationResult.IsValid)
			throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

		if (!await repository.ExistsById(newUser.Id)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);

		var oldUser = await repository.GetById(newUser.Id);

		if (oldUser.UserName != newUser.UserName && await repository.ExistsByUserName(newUser.UserName)) throw new AppException("Nome de usuário já existe!", HttpStatusCode.Conflict);
		if (oldUser.Email != newUser.Email && await repository.ExistsByEmail(newUser.Email)) throw new AppException("Email já existe!", HttpStatusCode.Conflict);
		if (oldUser.PhoneNumber != newUser.PhoneNumber && await repository.ExistsByPhoneNumber(newUser.PhoneNumber)) throw new AppException("Telefone já existe!", HttpStatusCode.Conflict);

		oldUser.Name = newUser.Name;
		oldUser.UserName = newUser.UserName;
		oldUser.Email = newUser.Email;
		oldUser.PhoneNumber = newUser.PhoneNumber;

		return await repository.Update(oldUser);
	}

	public async Task<bool> UpdatePassword(User user, string oldPassword, string newPassword)
	{
		var validationResult = passwordValidator.Validate(newPassword);
		if (!validationResult.IsValid)
			throw new AppException(validationResult.Errors.First().ErrorMessage, HttpStatusCode.BadRequest);

		return await repository.UpdatePassword(user, oldPassword, newPassword);
	}

	public async Task<User> Delete(int id)
	{
		if (!await repository.ExistsById(id)) throw new AppException("Usuário não encontrado!", HttpStatusCode.NotFound);
		var user = await repository.GetById(id);

		return await repository.Remove(user);
	}
}
