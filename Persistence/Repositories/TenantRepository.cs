using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public sealed class TenantRepository<TEntity>
    : GenericRepository<IApplicationDbContext, TEntity>,
      ITenantRepository<TEntity>
    where TEntity : class
{
    public TenantRepository(IApplicationDbContext context)
        : base(context)
    {
    }
}