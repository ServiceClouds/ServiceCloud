using Application.Abstractions.Repositories.Common;

namespace Application.Abstractions.Repositories.Common;

public interface IMasterRepository<TEntity>
    : IGenericRepository<TEntity>
    where TEntity : class
{
}