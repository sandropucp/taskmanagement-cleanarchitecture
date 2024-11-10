using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Categories;

public class Category : Entity
{

    public string Name { get; private set; } = null!;
    public Category() { }
    public Category(
        string name,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Id = id ?? Guid.NewGuid();
    }
}
