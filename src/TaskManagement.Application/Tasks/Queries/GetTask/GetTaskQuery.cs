using ErrorOr;
using Local = TaskManagement.Domain.Tasks;
using MediatR;

namespace TaskManagement.Application.Tasks.Queries.GetTask;

public record GetTaskQuery(Guid TaskId)
    : IRequest<ErrorOr<Local.Task>>;