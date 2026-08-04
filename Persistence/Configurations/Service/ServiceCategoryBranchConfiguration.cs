using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class ServiceCategoryBranchConfiguration : IEntityTypeConfiguration<ServiceCategoryBranch>
{
    public void Configure(EntityTypeBuilder<ServiceCategoryBranch> builder)
    {
        builder.ToTable("ServiceCategoryBranch");

        builder.HasKey(e => e.ServiceCategoryBranchId);

        builder.HasOne(d => d.ServiceCategory)
            .WithMany(p => p.ServiceCategoryBranches)
            .HasForeignKey(d => d.ServiceCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ServiceCategoryBranch_ServiceCategory");
    }
}
