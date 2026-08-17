using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;

namespace Persistence.Repositories.Common;

public abstract class GenericRepository<TContext, TEntity>
    : ReadRepository<TContext, TEntity>,
      IGenericRepository<TEntity>
    where TContext : IDbContext
    where TEntity : class
{
    protected GenericRepository(TContext context)
        : base(context)
    {
    }

    public void Add(TEntity entity)
    {
        _context.AddEntity(entity);
    }

    public void Update(TEntity entity)
    {
        _context.UpdateEntity(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.RemoveEntity(entity);
    }
}