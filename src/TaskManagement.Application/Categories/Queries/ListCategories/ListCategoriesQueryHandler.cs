using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Categories.Queries.ListCategories;

public class ListCategoriesQueryHandler(ICategoriesRepository categoriesRepository) : IRequestHandler<ListCategoriesQuery, ErrorOr<List<Category>>>
{
    private readonly ICategoriesRepository categoriesRepository = categoriesRepository;

    public async Task<ErrorOr<List<Category>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoriesRepository.GetAllAsync();
        return categories.ToList();
    }
}
