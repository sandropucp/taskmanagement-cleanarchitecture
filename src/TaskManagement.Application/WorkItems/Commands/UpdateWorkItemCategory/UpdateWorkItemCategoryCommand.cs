using ErrorOr;
using MediatR;
using TaskManagement.Domain.WorkItems;


namespace TaskManagement.Application.WorkItems.Commands.UpdateWorkItemCategory;

public record UpdateWorkItemCategoryCommand(Guid WorkItemId, Guid CategoryId) : IRequest<ErrorOr<WorkItem>>;
