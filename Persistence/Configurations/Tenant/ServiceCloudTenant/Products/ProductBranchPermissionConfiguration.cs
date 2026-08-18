using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{


    public class ProductBranchPermissionConfiguration : IEntityTypeConfiguration<ProductBranchPermission>
    {
        public void Configure(EntityTypeBuilder<ProductBranchPermission> builder)
        {
            builder.ToTable("ProductBranchPermission");

            builder.HasKey(x => x.ProductBranchPermissionId);

            builder.Property(x => x.ProductBranchPermissionId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.IsOnline)
                .IsRequired();

            builder.Property(x => x.IsHidePriceOnline)
                .IsRequired();

            builder.Property(x => x.IsFeatured)
                .IsRequired();

            builder.Property(x => x.HasTrackingventory)
                .IsRequired();

            builder.Property(x => x.HasShipping)
                .IsRequired();

            builder.Property(x => x.IsIncluded)
                .IsRequired();

            builder.Property(x => x.BusinessUseOnly)
                .IsRequired();

            builder.Property(x => x.IsVariantGenerated)
                .IsRequired();

            builder.Property(x => x.IsSharedPrivately)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductBranchPermissions)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}