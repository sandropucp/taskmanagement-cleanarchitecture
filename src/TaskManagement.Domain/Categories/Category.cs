namespace TaskManagement.Domain.Categories;
public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Category() { }
    public Category(string name, Guid? id = null)
    {
        Name = name;
        Id = id ?? Guid.NewGuid();
    }
}
