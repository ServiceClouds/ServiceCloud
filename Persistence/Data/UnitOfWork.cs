using Application.Abstractions.Data;

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

        public async Task<int> SaveChangesAsync(
            int companyId,
            CancellationToken cancellationToken = default)
        {
            var context = await _lazyContext.GetAsync(companyId);

            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}