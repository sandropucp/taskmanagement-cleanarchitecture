namespace TaskManagement.Domain.Users;
public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string? CreatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }
    private User() { }
    public User(string name, string email, Guid? id = null)
    {
        Name = name;
        Email = email;
        Id = id ?? Guid.NewGuid();
    }
}
