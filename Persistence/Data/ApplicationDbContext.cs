using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using  Domain.Tenant.ServiceCloudTenant.Entities;

namespace Persistence.Data
{
    public partial class ApplicationDbContext :  BaseDbContext
    {
        //public ApplicationDbContext()
        //{
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Staff> Staff { get; set; }

        public virtual DbSet<StaffBranch> StaffBranches { get; set; }

        public virtual DbSet<StaffPosition> StaffPositions { get; set; }

        public virtual DbSet<StateCountry> StateCountries { get; set; }

        
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}