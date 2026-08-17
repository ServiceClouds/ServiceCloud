using Application.Abstractions.Data;
using Shared.Response;

namespace Persistence.Data;

public sealed class TenantDbContextAccessor
    : ITenantDbContextAccessor, IAsyncDisposable
{
    private readonly ITenantDbContextFactory _factory;

    private IApplicationDbContext? _context;
    private int? _companyId;

    public TenantDbContextAccessor(
        ITenantDbContextFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Gets the tenant DbContext that has already been
    /// initialized for the current request.
    /// </summary>
    public IApplicationDbContext? Current => _context;

    /// <summary>
    /// Creates or reuses the DbContext for the specified company.
    /// The same context is reused throughout the current DI scope/request.
    /// </summary>
    public async Task<Result<IApplicationDbContext>> GetAsync(
        int companyId,
        CancellationToken cancellationToken = default)
    {
        // Reuse the existing tenant context when the requested
        // company is the same as the current company.
        if (_context is not null &&
            _companyId == companyId)
        {
            return Result<IApplicationDbContext>
                .Success(_context);
        }

        // A different company should not normally be requested
        // within the same request. Dispose the previous context
        // before creating a new one if it happens.
        if (_context is not null)
        {
            await DisposeCurrentAsync();
        }

        // Factory gets the tenant connection string from the
        // Master DB and creates the appropriate tenant context.
        var result = await _factory.CreateAsync(companyId);

        if (result.IsFailure)
        {
            return Result<IApplicationDbContext>
                .Failure(result.Error);
        }

        _context = result.Value;
        _companyId = companyId;

        return Result<IApplicationDbContext>
            .Success(_context);
    }

    /// <summary>
    /// Disposes the current tenant DbContext and clears
    /// the associated company information.
    /// </summary>
    private async ValueTask DisposeCurrentAsync()
    {
        if (_context is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
        else if (_context is IDisposable disposable)
        {
            disposable.Dispose();
        }

        _context = null;
        _companyId = null;
    }

    /// <summary>
    /// Called automatically when the scoped accessor is disposed.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeCurrentAsync();
    }
}