using Application.Abstractions.Repositories.Common;
using Persistence.Data.MasterDbContext;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public sealed class MasterRepository<TEntity>
    : GenericRepository<MasterTenantDbContext, TEntity>,
      IMasterRepository<TEntity>
    where TEntity : class
{
    public MasterRepository(MasterTenantDbContext context)
        : base(context)
    {
    }
}