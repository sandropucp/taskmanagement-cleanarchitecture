using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Infrastructure.Common.Persistence;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Tasks.Persistence;

public class TasksRepository : ITasksRepository
{
    private readonly TaskManagementDbContext _dbContext;

    public TasksRepository(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTaskAsync(Local.Task task)
    {
        await _dbContext.Tasks.AddAsync(task);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Local.Task>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Local.Task> GetByIdAsync(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTaskAsync(Local.Task task)
    {
        throw new NotImplementedException();
    }
}