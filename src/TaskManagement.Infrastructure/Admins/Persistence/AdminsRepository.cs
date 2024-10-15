using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Admins;
using TaskManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Admins.Persistence;

public class AdminsRepository(TaskManagementDbContext dbContext) : IAdminsRepository
{
    public async Task AddAdminAsync(Admin admin)
    {
        await dbContext.Admins.AddAsync(admin);
    }

    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return await dbContext.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == adminId);
    }

    public Task UpdateAsync(Admin admin)
    {
        dbContext.Admins.Update(admin);

        return Task.CompletedTask;
    }
}