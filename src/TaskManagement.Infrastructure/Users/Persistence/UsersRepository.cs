using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Users.Persistence;

public class UsersRepository(TaskManagementDbContext dbContext) : IUsersRepository
{
    public async Task AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId) => await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

    public async Task<List<User>> GetAllAsync() => await dbContext.Users.ToListAsync();

    public Task UpdateUserAsync(User user)
    {
        dbContext.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task RemoveUserAsync(User user)
    {
        dbContext.Users.Remove(user);
        return Task.CompletedTask;
    }
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }
}
