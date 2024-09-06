using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Queries.GetTask;

public record GetTaskQuery(Guid TaskId)
    : IRequest<ErrorOr<Local.Task>>;
