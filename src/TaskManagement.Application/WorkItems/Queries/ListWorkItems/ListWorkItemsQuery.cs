using ErrorOr;
using MediatR;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.WorkItems.Queries.ListWorkItems;

public record ListWorkItemsQuery : IRequest<ErrorOr<List<WorkItem>>>;
