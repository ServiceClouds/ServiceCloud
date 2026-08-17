using Application.Abstractions.Data;
using Shared.Response;

namespace Persistence.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _context;

    public UnitOfWork(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var affectedRows =
            await _context.SaveChangesAsync(
                cancellationToken);

        return Result<int>.Success(affectedRows);
    }
}