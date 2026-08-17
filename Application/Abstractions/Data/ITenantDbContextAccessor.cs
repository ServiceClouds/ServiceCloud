using Shared.Response;

namespace Application.Abstractions.Data;

public interface ITenantDbContextAccessor
{
    IApplicationDbContext? Current { get; }

    Task<Result<IApplicationDbContext>> GetAsync(
        int companyId,
        CancellationToken cancellationToken = default);
}