using ErrorOr;
using MediatR;
using TaskManagement.Domain.Categories;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ErrorOr<Category>>
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoriesRepository categoriesRepository,
        IUnitOfWork unitOfWork)
    {
        _categoriesRepository = categoriesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Category>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category(request.Name);
        await _categoriesRepository.AddCategoryAsync(category);
        await _unitOfWork.CommitChangesAsync();
        return category;
    }
}