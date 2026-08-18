using Application.Abstractions.Repositories.Common;

namespace Application.Abstractions.Repositories.Common;

public interface ITenantRepository<TEntity>
    : IGenericRepository<TEntity>
    where TEntity : class
{
}