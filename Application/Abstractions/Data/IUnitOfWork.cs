using Shared.Response;

namespace Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task<Result<int>> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}