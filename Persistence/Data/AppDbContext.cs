using Application.Abstractions.Data; 
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Persistence.Data
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true)
            where TEntity : class
        {
            var query = Set<TEntity>().AsQueryable();

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
