using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.ToTable("ServiceCategory");

        builder.HasKey(e => e.ServiceCategoryId);

        builder.Property(e => e.ImagePath)
            .IsUnicode(false);

        builder.Property(e => e.Color)
            .IsUnicode(false);
    }
}
