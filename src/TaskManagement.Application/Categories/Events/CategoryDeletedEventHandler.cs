using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Admins.Events;
using MediatR;

namespace TaskManagement.Application.Categories.Events;

public class CategoryDeletedEventHandler(
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : INotificationHandler<CategoryDeletedEvent>
{
    private readonly ICategoriesRepository categoriesRepository = categoriesRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(notification.CategoryId);

        if (category is null)
        {
            // resilient error handling
            throw new InvalidOperationException();
        }

        await categoriesRepository.RemoveCategoryAsync(category);
        await unitOfWork.CommitChangesAsync();
    }
}
