using TaskManagement.Domain.Common;
using TaskManagement.Domain.Users.Events;

namespace TaskManagement.Domain.Users;
public class User: Entity
{
    //public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Role { get; private set; } = null!;
    private User() { }
    public User(string name, string email, string role, Guid? id = null)
    {
        Name = name;
        Email = email;
        Role = role;
        Id = id ?? Guid.NewGuid();
    }

    public void DeleteCategory(Guid categoryId) =>
        domainEvents.Add(new CategoryDeletedEvent(categoryId));
}
