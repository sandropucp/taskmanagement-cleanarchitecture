using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users.Events;
using MediatR;

namespace TaskManagement.Application.Categories.Events;

public class CategoryDeletedEventHandler(
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : INotificationHandler<CategoryDeletedEvent>
{
    private readonly ICategoriesRepository _categoriesRepository = categoriesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByIdAsync(notification.CategoryId) ?? throw new InvalidOperationException();

        await _categoriesRepository.RemoveCategoryAsync(category);
        await _unitOfWork.CommitChangesAsync();
    }
}
