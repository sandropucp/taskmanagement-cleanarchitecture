using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}