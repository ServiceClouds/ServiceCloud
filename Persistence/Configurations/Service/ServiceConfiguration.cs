using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Service");

        builder.HasKey(e => e.ServiceId);

        builder.HasOne(d => d.ServiceCategory)
            .WithMany(p => p.Services)
            .HasForeignKey(d => d.ServiceCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Service_ServiceCategory");
    }
}
