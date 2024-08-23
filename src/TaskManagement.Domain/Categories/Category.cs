namespace TaskManagement.Domain.Categories;
public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    // public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    // public string? CreatedBy { get; private set; }
    // public DateTime? UpdatedAt { get; private set; }
    // public string? UpdatedBy { get; private set; }

    public Category() { }
    public Category(string name, Guid? id = null)
    {
        Name = name;
        Id = id ?? Guid.NewGuid();
    }
}
