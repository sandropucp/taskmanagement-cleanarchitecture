using ErrorOr;
using MediatR;

namespace TaskManagement.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid CategoryId, Guid UserId) : IRequest<ErrorOr<Deleted>>;
