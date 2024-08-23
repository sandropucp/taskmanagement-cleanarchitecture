using ErrorOr;
using MediatR;
using TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Categories.Queries.ListCategories;

public record ListCategoriesQuery : IRequest<ErrorOr<List<Category>>>;