using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Persistence.Repositories.Common
{
    public interface IReadRepository<TEntity>
     where TEntity : class
    {
        IQueryable<TEntity> GetAll(bool asNoTracking = true);

        Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool asNoTracking = true,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<int> CountAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            CancellationToken cancellationToken = default);
    }
}

