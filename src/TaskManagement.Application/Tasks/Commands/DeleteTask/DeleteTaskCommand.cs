using ErrorOr;
using MediatR;

namespace TaskManagement.Application.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand(Guid TaskId) : IRequest<ErrorOr<Deleted>>;