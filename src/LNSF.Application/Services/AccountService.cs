using LNSF.Application.Validators;
using LNSF.Domain.Entities;
using LNSF.Domain.Exceptions;
using LNSF.Domain.Filters;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly AccountValidator _accountValidator;
    private readonly AccountFilterValidator _accountFilterValidator;
    private readonly PasswordValidator _passwordValidator;

    public AccountService(IAccountRepository accountRepository,
        AccountValidator accountValidator,
        AccountFilterValidator accountFilterValidator,
        PasswordValidator passwordValidator)
    {
        _accountRepository = accountRepository;
        _accountValidator = accountValidator;
        _accountFilterValidator = accountFilterValidator;
        _passwordValidator = passwordValidator;
    }

    public async Task<List<Account>> Query(AccountFilter filter)
    {
        var validationResult = await _accountFilterValidator.ValidateAsync(filter);

        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());
        
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

        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        account.Id = 0;
        account = await _accountRepository.Post(account);
        account.Password = "";

        return account;
    }

    public async Task<Account> Update(Account account)
    {
        var validationResult = await _accountValidator.ValidateAsync(account);

        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        await _accountRepository.Get(account.Id);

        account = await _accountRepository.Put(account);
        account.Password = "";

        return account;
    }

    public async Task<Account> Update(Account account, string oldPassword)
    {
        var validationResult = await _passwordValidator.ValidateAsync(oldPassword);

        if (!validationResult.IsValid) throw new AppException(validationResult.ToString());

        account.Password = oldPassword;

        return await Update(account);
    }

    public async Task<Account> Delete(int id)
    {
        var account = await _accountRepository.Get(id);

        account = await _accountRepository.Delete(account);
        account.Password = "";

        return account;
    }
}
