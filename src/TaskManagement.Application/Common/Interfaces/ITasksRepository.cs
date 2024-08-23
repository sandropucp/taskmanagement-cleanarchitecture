using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Common.Interfaces;

public interface ITasksRepository
{
    Task AddTaskAsync(Local.Task task);
    Task<Local.Task> GetByIdAsync(Guid taskId);
    Task<List<Local.Task>> GetAllAsync();
    Task UpdateTaskAsync(Local.Task task);
    Task<bool> ExistsAsync(Guid id);
}