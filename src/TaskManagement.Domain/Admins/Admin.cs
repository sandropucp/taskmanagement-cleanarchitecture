using TaskManagement.Domain.Admins.Events;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Categories;
using Throw;

namespace TaskManagement.Domain.Admins;

public class Admin : Entity
{
    public Guid UserId { get; }
    public Guid? CategoryId { get; private set; }


    public Admin(
        Guid userId,
        Guid? categoryId = null,
        Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        CategoryId = categoryId;
    }

    private Admin() { }

    public void SetCategory(Category category)
    {
        CategoryId.HasValue.Throw().IfTrue();

        CategoryId = category.Id;
    }

    public void DeleteCategory(Guid categoryId)
    {
        CategoryId.ThrowIfNull().IfNotEquals(categoryId);

        CategoryId = null;

        domainEvents.Add(new CategoryDeletedEvent(categoryId));
    }
}