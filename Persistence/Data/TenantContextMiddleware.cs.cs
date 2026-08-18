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
        Console.WriteLine(
            "===== TenantDbContextMiddleware START =====");

        Console.WriteLine(
            $"Authenticated: {httpContext.User.Identity?.IsAuthenticated}");

        if (httpContext.User.Identity?.IsAuthenticated == true)
        {
            Console.WriteLine(
                $"CompanyId: {userContext.CompanyId}");

            var result = await accessor.GetAsync(
                userContext.CompanyId);

            Console.WriteLine(
                $"Tenant Context Result: {result.IsSuccess}");

            if (result.IsFailure)
            {
                throw new InvalidOperationException(
                    $"Unable to create Tenant DbContext: {result.Error}");
            }

            Console.WriteLine(
                "Tenant DbContext initialized successfully.");
        }
        else
        {
            Console.WriteLine(
                "User is NOT authenticated. Tenant DbContext was NOT created.");
        }

        Console.WriteLine(
            "===== TenantDbContextMiddleware END =====");

        await _next(httpContext);
    }
}