using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;


namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public record CreateTaskCommand(string Name, string Description, DateTime DueDate,
    Local.TaskStatus TaskStatus, Guid CategoryId, Guid UserId) : IRequest<ErrorOr<Local.Task>>;
