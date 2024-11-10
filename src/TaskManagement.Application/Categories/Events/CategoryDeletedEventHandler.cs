using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users.Events;

namespace TaskManagement.Application.Categories.Events;

public class CategoryDeletedEventHandler(
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : INotificationHandler<CategoryDeletedEvent>
{
    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(notification.CategoryId) ?? throw new InvalidOperationException();

        await categoriesRepository.RemoveCategoryAsync(category);
        await unitOfWork.CommitChangesAsync();
    }
}
