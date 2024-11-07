using ErrorOr;
using MediatR;
using TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<ErrorOr<Category>>;
