using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.MasterDbContext
{
    public partial class MasterTenantDbContext : DbContext, IDbContext
    {
        public MasterTenantDbContext(
            DbContextOptions<MasterTenantDbContext> options)
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

        

        public DbSet<TEntity> GetDbSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        public IQueryable<TEntity> Query<TEntity>(bool asNoTracking = true)
            where TEntity : class
        {
            IQueryable<TEntity> query = GetDbSet<TEntity>();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MasterTenantDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}