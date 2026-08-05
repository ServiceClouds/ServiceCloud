using Application.Abstractions.Data;
using Application.Common;
using Shared.Response;

namespace Persistence.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LazyApplicationDbContext _lazyContext;
        private readonly IUserContext _userContext;

        public UnitOfWork(
            LazyApplicationDbContext lazyContext, IUserContext userContext)
        {
            _lazyContext = lazyContext;
            _userContext = userContext;
        }

        public async Task<Result<int>> SaveChangesAsync(
         
            CancellationToken cancellationToken = default)
        {
            var contextResult = await _lazyContext.GetAsync(_userContext.CompanyId);

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