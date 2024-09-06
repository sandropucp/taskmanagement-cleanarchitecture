using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Infrastructure.Common.Persistence;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Tasks.Persistence;

public class TasksRepository(TaskManagementDbContext dbContext) : ITasksRepository
{
    private readonly TaskManagementDbContext _dbContext = dbContext;

    public async Task AddTaskAsync(Local.Task task) => await _dbContext.Tasks.AddAsync(task);

    public Task<bool> ExistsAsync(Guid id) => throw new NotImplementedException();

    public async Task<List<Local.Task>> GetAllAsync() => await _dbContext.Tasks.ToListAsync();

    public async Task<Local.Task?> GetByIdAsync(Guid taskId) => await _dbContext.Tasks.FirstOrDefaultAsync(task => task.Id == taskId);

    public Task UpdateTaskAsync(Local.Task task) => throw new NotImplementedException();

    Task ITasksRepository.RemoveTaskAsync(Local.Task task)
    {
        _dbContext.Tasks.Remove(task);
        return Task.CompletedTask;
    }
}
