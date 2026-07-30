using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Data
{
    public partial class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<BranchIntegration> BranchIntegrations { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffBranch> StaffBranches { get; set; }
        public virtual DbSet<StaffLoggedInBranch> StaffLoggedInBranches { get; set; }
        public virtual DbSet<StaffLogin> StaffLogins { get; set; }

        // IDbContext Implementation

        public DbSet<TEntity> GetDbSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true)
            where TEntity : class
        {
            var query = GetDbSet<TEntity>().AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return query;
        }

        public void AddEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            GetDbSet<TEntity>().Add(entity);
        }

        public void UpdateEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            GetDbSet<TEntity>().Update(entity);
        }

        public void RemoveEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            GetDbSet<TEntity>().Remove(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}