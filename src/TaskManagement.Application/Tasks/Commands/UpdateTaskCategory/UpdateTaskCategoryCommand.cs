using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;


namespace TaskManagement.Application.Tasks.Commands.UpdateTaskCategory;

public record UpdateTaskCategoryCommand(Guid TaskId, Guid CategoryId) : IRequest<ErrorOr<Local.Task>>;
