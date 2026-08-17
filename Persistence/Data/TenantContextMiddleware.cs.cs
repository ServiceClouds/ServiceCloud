using Application.Abstractions.Data;
using Application.Common;
using Microsoft.AspNetCore.Http;

namespace API.Middleware;

public sealed class TenantDbContextMiddleware
{
    private readonly RequestDelegate _next;

    public TenantDbContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        ITenantDbContextAccessor accessor,
        IUserContext userContext)
    {
        // Tenant database is only required for authenticated users.
        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            // CompanyId comes from the authenticated JWT.
            var companyId = userContext.CompanyId;

            // Create and store the tenant DbContext
            // inside the scoped TenantDbContextAccessor.
            var result = await accessor.GetAsync(companyId);

            if (result.IsFailure)
            {
                throw new InvalidOperationException(
                    $"Unable to create Tenant DbContext: {result.Error}");
            }
        }

        // Continue the request pipeline.
        await _next(httpContext);
    }
}