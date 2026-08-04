using Application.Abstractions.Data;
using Shared.Response;

namespace Persistence.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LazyApplicationDbContext _lazyContext;

        public UnitOfWork(
            LazyApplicationDbContext lazyContext)
        {
            _lazyContext = lazyContext;
        }

        public async Task<Result<int>> SaveChangesAsync(
            int companyId,
            CancellationToken cancellationToken = default)
        {
            var contextResult = await _lazyContext.GetAsync(companyId);

            if (contextResult.IsFailure)
            {
                return Result<int>.Failure(contextResult.Error);
            }

            var affectedRows = await contextResult.Value
                .SaveChangesAsync(cancellationToken);

            return Result<int>.Success(affectedRows);
        }
    }
}