using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.WorkItems.Commands.DeleteWorkItem;

public class DeleteTaskCommandHandler(
    IUnitOfWork unitOfWork,
    IWorkItemsRepository workItemsRepository) : IRequestHandler<DeleteWorkItemCommand, ErrorOr<Deleted>>
{
    private readonly IWorkItemsRepository workItemsRepository = workItemsRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

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
