using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.WorkItems.Commands.DeleteWorkItem;

public class DeleteWorkItemCommandHandler(
    IWorkItemsRepository workItemsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteWorkItemCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
    {
        var workItem = await workItemsRepository.GetByIdAsync(request.WorkItemId);

        if (workItem == null)
        {
            return Error.NotFound("description: task not found");
        }

        await workItemsRepository.RemoveWorkItemAsync(workItem);
        await unitOfWork.CommitChangesAsync();

        return new Deleted();
    }
}
