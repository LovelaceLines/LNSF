using System.Net;
using LNSF.Application.Interfaces;
using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly AccountValidator _accountValidator;

    public AccountService(IAccountRepository accountRepository,
        AccountValidator accountValidator)
    {
        _accountRepository = accountRepository;
        _accountValidator = accountValidator;
    }

    public async Task<List<Account>> Query(AccountFilter filter)
    {        
        var accounts = await _accountRepository.Query(filter);
        accounts.ForEach(x => x.Password = "");
        return accounts;
    }

    public async Task<Account> Get(string userName, string password)
    {
        var account = await _accountRepository.Get(userName, password);
        account.Password = "";
        return account;
    }

    public async Task<Account> Create(Account account)
    {
        var validationResult = await _accountValidator.ValidateAsync(account);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        var filter = new AccountFilter { UserName = account.UserName };
        var query = await _accountRepository.Query(filter);
        if (query.Count > 0) throw new AppException("Usuário já existe!", HttpStatusCode.Conflict);

        account.Id = Guid.NewGuid().ToString();
        account = await _accountRepository.Add(account);
        account.Password = "";
        return account;
    }

    public async Task<Account> Update(Account account)
    {
        var validationResult = await _accountValidator.ValidateAsync(account);
        if (!validationResult.IsValid) throw new AppException(validationResult.ToString(), HttpStatusCode.BadRequest);
        if (!await _accountRepository.Exists(account.Id)) throw new AppException("Conta não encontrada!", HttpStatusCode.NotFound); 

        account = await _accountRepository.Update(account);
        account.Password = "";
        return account;
    }

    public async Task<Account> Delete(string id)
    {
        if (!await _accountRepository.Exists(id)) throw new AppException("Conta não encontrada!", HttpStatusCode.NotFound);

        var account = await _accountRepository.Get(id);
        account = await _accountRepository.Remove(account);
        account.Password = "";
        return account;
    }
}
