using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Common.Interfaces;

public interface ITasksRepository
{
    Task AddTaskAsync(Local.Task task);
}