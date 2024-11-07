using ErrorOr;
using MediatR;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.WorkItems.Queries.GetWorkItem;

public record GetWorkItemQuery(Guid WorkItemId)
    : IRequest<ErrorOr<WorkItem>>;
