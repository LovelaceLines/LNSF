using LNSF.Domain.Entities;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
        => _accountRepository = accountRepository;
    
    public async Task<bool> Exist(Account account)
        => await _accountRepository.Exist(account);
    
    public async Task<Account> Create(Account account)
        => await _accountRepository.Post(account);
}
