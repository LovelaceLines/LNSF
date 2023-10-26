using System.Net;
using System.Text;
using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace LNSF.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly AccountValidator _accountValidator;
    private readonly PasswordValidator _passwordValidator;

    public AccountService(IAccountRepository accountRepository,
        AccountValidator accountValidator,
        PasswordValidator passwordValidator)
    {
        _accountRepository = accountRepository;
        _accountValidator = accountValidator;
        _passwordValidator = passwordValidator;
    }

    public async Task<List<Account>> Query(AccountFilter filter)
    {        
        var accounts = await _accountRepository.Query(filter);
        accounts.ForEach(x => x.Password = "");
        return accounts;
    }

    public async Task<Account> Get(string userName, string password)
    {
        var account = await _accountRepository.GetByUserName(userName);
        account.Password = "";
        return account;
    }

    public async Task<Account> Create(Account account)
    {
        var validationResult = await _accountValidator.ValidateAsync(account);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        if (await _accountRepository.Exists(account.UserName)) throw new AppException("Usuário já cadastrado!", HttpStatusCode.Conflict);

        account.Id = Guid.NewGuid().ToString();
        account.Password = _accountRepository.HashPassword(account.Password);
        account = await _accountRepository.Add(account);
        return account;
    }

    public async Task<Account> Update(Account newAccount)
    {
        var validationResult = await _accountValidator.ValidateAsync(newAccount);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        if (!await _accountRepository.ExistsId(newAccount.Id)) throw new AppException("Conta não encontrada!", HttpStatusCode.NotFound); 
        var oldAccount = await _accountRepository.GetById(newAccount.Id);
        if (oldAccount.UserName != newAccount.UserName && await _accountRepository.Exists(newAccount.UserName)) throw new AppException("Usuário já cadastrado!", HttpStatusCode.Conflict);
 
        newAccount.Password = oldAccount.Password;
        newAccount = await _accountRepository.Update(newAccount);
        return newAccount;
    }

    public async Task<Account> Update(string id, string newPassword, string oldPassword)
    {
        var validationResult = await _passwordValidator.ValidateAsync(newPassword);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        if (!await _accountRepository.ExistsId(id)) throw new AppException("Conta não encontrada!", HttpStatusCode.NotFound); 
        var account = await _accountRepository.GetById(id);
        if (!_accountRepository.VerifyPassword(oldPassword, account.Password)) throw new AppException("Senha antiga inválida!", HttpStatusCode.BadRequest);

        account.Password = _accountRepository.HashPassword(newPassword);
        account = await _accountRepository.Update(account);
        return account;
    }

    public async Task<Account> Delete(string id)
    {
        if (!await _accountRepository.ExistsId(id)) throw new AppException("Conta não encontrada!", HttpStatusCode.NotFound);

        var account = await _accountRepository.GetById(id);
        account = await _accountRepository.Remove(account);
        account.Password = "";
        return account;
    }
}
