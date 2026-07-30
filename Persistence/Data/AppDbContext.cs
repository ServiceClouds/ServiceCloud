using Application.Abstractions.Data; 
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Persistence.Data
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add the Staff table here so Entity Framework knows it exists!
        public DbSet<Staff> Staff { get; set; } = null!;
        // You can add DbSet<StaffLogin> or others here later

        public IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true) where TEntity : class
        {
            return asNoTracking ? Set<TEntity>().AsNoTracking() : Set<TEntity>();
        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : class => Add(entity);
        public void AddRange<TEntity>(TEntity[] entities) where TEntity : class => AddRange(entities);
        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class => Update(entity);
        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class => Remove(entity);
    }
}
