using Microsoft.AspNetCore.Builder;
using TaskManagement.Infrastructure.Common.Middleware;

namespace TaskManagement.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();

        return builder;
    }
}
