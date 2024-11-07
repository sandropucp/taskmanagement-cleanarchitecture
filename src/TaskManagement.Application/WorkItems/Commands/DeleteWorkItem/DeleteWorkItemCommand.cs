using ErrorOr;
using MediatR;

namespace TaskManagement.Application.WorkItems.Commands.DeleteWorkItem;

public record DeleteWorkItemCommand(Guid WorkItemId) : IRequest<ErrorOr<Deleted>>;
