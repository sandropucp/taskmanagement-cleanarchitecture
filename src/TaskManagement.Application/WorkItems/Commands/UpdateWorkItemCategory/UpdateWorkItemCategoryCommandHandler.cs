using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.WorkItems.Commands.UpdateWorkItemCategory;

public class UpdateWorkItemCategoryCommandHandler(IWorkItemsRepository workItemsRepository,
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateWorkItemCategoryCommand, ErrorOr<WorkItem>>
{
    public async Task<ErrorOr<WorkItem>> Handle(
        UpdateWorkItemCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);
        var workItem = await workItemsRepository.GetByIdAsync(request.WorkItemId);
        if (category is null)
        {
            return Error.NotFound("description: category not found");
        }
        if (workItem is null)
        {
            return Error.NotFound("description: task not found");
        }
        workItem.UpdateCategory(category);
        await workItemsRepository.UpdateWorkItemAsync(workItem);
        await unitOfWork.CommitChangesAsync();

        return workItem;
    }
}
