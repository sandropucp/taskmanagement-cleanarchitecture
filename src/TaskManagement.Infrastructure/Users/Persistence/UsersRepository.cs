using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Users.Persistence;
public class UsersRepository : IUsersRepository
{
    private readonly TaskManagementDbContext _context;

    public UsersRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    Task IUsersRepository.AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    Task<User> IUsersRepository.GetByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    Task<List<User>> IUsersRepository.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task IUsersRepository.UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}