namespace TaskManagement.Domain.Users;
public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    private User() { }
    public User(string name, string email, Guid? id = null)
    {
        Name = name;
        Email = email;
        Id = id ?? Guid.NewGuid();
    }
}
