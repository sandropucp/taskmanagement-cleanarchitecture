using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Infrastructure.Common.Persistence;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Tasks.Persistence;

public class TasksRepository(TaskManagementDbContext dbContext) : ITasksRepository
{
    private readonly TaskManagementDbContext dbContext = dbContext;

    public async Task AddTaskAsync(Local.Task task) => await dbContext.Tasks.AddAsync(task);

    public Task<bool> ExistsAsync(Guid id) => throw new NotImplementedException();

    public async Task<List<Local.Task>> GetAllAsync() => await dbContext.Tasks.ToListAsync();

    public async Task<Local.Task?> GetByIdAsync(Guid taskId) => await dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == taskId);

    public Task UpdateTaskAsync(Local.Task task) => throw new NotImplementedException();

    Task ITasksRepository.RemoveTaskAsync(Local.Task task)
    {
        dbContext.Tasks.Remove(task);
        return Task.CompletedTask;
    }
}
