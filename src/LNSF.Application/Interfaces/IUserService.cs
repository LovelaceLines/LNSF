using LNSF.Domain.Entities;

namespace LNSF.Application.Interfaces;

public interface IUserService
{
	Task<User> GetById(int id);
	Task<User> GetByUserName(string userName);
	Task<User> Create(User user, string password);
	Task<User> Update(User user);
	Task<bool> UpdatePassword(User user, string oldPassword, string newPassword);
	Task<User> Delete(int id);
}
