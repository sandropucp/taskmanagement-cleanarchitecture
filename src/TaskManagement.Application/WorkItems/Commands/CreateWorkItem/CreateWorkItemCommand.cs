using ErrorOr;
using MediatR;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.WorkItems.Commands.CreateWorkItem;

public record CreateWorkItemCommand(string Name, string Description,
    DateTime DueDate, WorkItemStatus TaskStatus,
    Guid CategoryId, Guid UserAssignedToId) : IRequest<ErrorOr<WorkItem>>;
