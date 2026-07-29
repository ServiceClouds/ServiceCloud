using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories.Common;

public interface IGenericRepository<TEntity>
    : IReadRepository<TEntity>
    where TEntity : class
{
    void Add(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}