namespace TaskManagement.Contracts.Users;

public record CreateUserRequest(string Name, string Email, string Role);
