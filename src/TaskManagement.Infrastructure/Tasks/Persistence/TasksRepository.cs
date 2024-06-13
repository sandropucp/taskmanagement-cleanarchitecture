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
}