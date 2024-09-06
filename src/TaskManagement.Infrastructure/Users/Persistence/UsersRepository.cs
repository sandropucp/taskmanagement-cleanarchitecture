using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Users.Persistence;
public class UsersRepository(TaskManagementDbContext context) : IUsersRepository
{
    private readonly TaskManagementDbContext context = context;

    public async Task AddUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId) => await context.Users.FindAsync(userId);

    public async Task<List<User>> GetAllAsync() => await context.Users.ToListAsync();

    Task IUsersRepository.AddUserAsync(User user) => throw new NotImplementedException();

    Task<User> IUsersRepository.GetByIdAsync(Guid userId) => throw new NotImplementedException();

    Task<List<User>> IUsersRepository.GetAllAsync() => throw new NotImplementedException();

    Task IUsersRepository.UpdateUserAsync(User user) => throw new NotImplementedException();
}
