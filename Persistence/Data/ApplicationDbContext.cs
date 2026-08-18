using Application.Abstractions.Data;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;


namespace Persistence.Data
{
    public partial class ApplicationDbContext : BaseDbContext,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // --- Your Existing DbSets ---
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffBranch> StaffBranches { get; set; }
        public virtual DbSet<StaffPosition> StaffPositions { get; set; }
        public virtual DbSet<StateCountry> StateCountries { get; set; }

    
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }
        public virtual DbSet<ServiceCategoryBranch> ServiceCategoryBranches { get; set; }

        //Products
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

        public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

        public virtual DbSet<ProductBranchPermission> ProductBranchPermissions { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        public virtual DbSet<ProductVariant> ProductVariants { get; set; }

        public virtual DbSet<ProductVariantBranch> ProductVariantBranches { get; set; }

        public virtual DbSet<ProductVariantPackaging> ProductVariantPackagings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


            // ✅ APPLY ONLY TENANT CONFIGURATIONS
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly,
                ConfigurationScanner.IsTenantConfiguration  // Filter for Tenant only
            );
            base.OnModelCreating(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
