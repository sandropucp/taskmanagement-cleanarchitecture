using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Common.Interfaces;

public interface IUsersRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetByIdAsync(Guid userId);
    Task<List<User>> GetAllAsync();
    Task UpdateUserAsync(User user);
    Task RemoveUserAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByEmailAsync(string email);
}
