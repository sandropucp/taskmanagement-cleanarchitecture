using TaskManagement.Domain.Common;
using TaskManagement.Domain.Common.Interfaces;
using TaskManagement.Domain.Users.Events;

namespace TaskManagement.Domain.Users;

public class User : Entity
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    private readonly string _passwordHash = null!;

    private User() { }
    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _passwordHash = passwordHash;
        Id = id ?? Guid.NewGuid();
    }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher) =>
        passwordHasher.IsCorrectPassword(password, _passwordHash);

    public void DeleteCategory(Guid categoryId) =>
        _domainEvents.Add(new CategoryDeletedEvent(categoryId));
}