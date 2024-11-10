using System.Security.Claims;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Application.Common.Models;
using Throw;

namespace TaskManagement.Api.Services;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        httpContextAccessor.HttpContext.ThrowIfNull();

        var id = GetClaimValues("id")
            .Select(Guid.Parse)
            .First();

        var permissions = GetClaimValues("permissions");
        var roles = GetClaimValues(ClaimTypes.Role);

        return new CurrentUser(Id: id, Permissions: permissions, Roles: roles);
    }

    private IReadOnlyList<string> GetClaimValues(string claimType) =>
        httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();
}
