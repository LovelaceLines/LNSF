using LNSF.Domain.DTOs;
using LNSF.Domain.Repositories;

namespace LNSF.Application.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
        => _accountRepository = accountRepository;
    
    public async Task<bool> Exist(AccountFilters filters)
        => await _accountRepository.Exist(filters);
}
