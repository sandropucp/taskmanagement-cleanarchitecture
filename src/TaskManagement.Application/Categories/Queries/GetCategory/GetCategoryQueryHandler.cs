using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler(ICategoriesRepository categoriesRepository) : IRequestHandler<GetCategoryQuery, ErrorOr<Category>>
{
    private readonly ICategoriesRepository _categoriesRepository = categoriesRepository;

    public async Task<ErrorOr<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByIdAsync(request.CategoryId);
        return category is null
            ? Error.NotFound(description: "Category not found")
            : category;
    }
}
