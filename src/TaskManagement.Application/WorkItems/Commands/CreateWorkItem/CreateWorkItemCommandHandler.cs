using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.WorkItems;


namespace TaskManagement.Application.WorkItems.Commands.CreateWorkItem;

public class CreateWorkItemCommandHandler(IWorkItemsRepository workItemsRepository,
    ICategoriesRepository categoriesRepository,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<CreateWorkItemCommand, ErrorOr<WorkItem>>
{
    public async Task<ErrorOr<WorkItem>> Handle(
        CreateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);
        var assignedUser = await usersRepository.GetByIdAsync(request.UserAssignedToId);

        if (category is null)
        {
            return WorkItemErrors.CategoryNotFound;
        }
        if (assignedUser is null)
        {
            return WorkItemErrors.AssignedUserNotFound;
        }

        var task = new WorkItem(
            name: request.Name, description: request.Description,
            dueDate: request.DueDate, workItemStatus: request.TaskStatus,
            categoryId: request.CategoryId,
            categoryName: category.Name,
            assignedToId: request.UserAssignedToId,
            assignedToName: assignedUser.Name);

        await workItemsRepository.AddWorkItemAsync(task);
        await unitOfWork.CommitChangesAsync();

        return task;
    }
}
