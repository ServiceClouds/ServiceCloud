using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public abstract class BaseDbContext : DbContext, IApplicationDbContext
    {
        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TEntity> GetDbSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true)
            where TEntity : class
        {
            IQueryable<TEntity> query = Set<TEntity>();

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        public void AddEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            Set<TEntity>().Add(entity);
        }

        public void UpdateEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            Set<TEntity>().Update(entity);
        }

        public void RemoveEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            Set<TEntity>().Remove(entity);
        }
    }
}