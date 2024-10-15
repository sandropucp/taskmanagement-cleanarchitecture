namespace TaskManagement.Domain.Categories;
public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Guid AdminId { get; set; } = Guid.Empty;
    public Category() { }
    public Category(string name, Guid adminId, Guid? id = null)
    {
        Name = name;
        AdminId = adminId;
        Id = id ?? Guid.NewGuid();
    }
}
