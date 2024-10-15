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

    public Task UpdateTaskAsync(Local.Task task)
    {
        dbContext.Tasks.Update(task);
        return Task.CompletedTask;
    }

    Task ITasksRepository.RemoveTaskAsync(Local.Task task)
    {
        dbContext.Tasks.Remove(task);
        return Task.CompletedTask;
    }

    public async Task<List<Local.Task>> GetTasksByCategoryIdAsync(Guid categoryId) => await dbContext.Tasks.Where(task => task.CategoryId == categoryId).ToListAsync();
    public Task RemoveRangeAsync(List<Local.Task> tasks)
    {
        dbContext.RemoveRange(tasks);

        return Task.CompletedTask;
    }
}
