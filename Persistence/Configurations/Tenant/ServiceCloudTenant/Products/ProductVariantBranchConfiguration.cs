using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{


    public class ProductVariantBranchConfiguration : IEntityTypeConfiguration<ProductVariantBranch>
    {
        public void Configure(EntityTypeBuilder<ProductVariantBranch> builder)
        {
            builder.ToTable("ProductVariantBranch");

            builder.HasKey(x => x.ProductVariantBranchId);

            builder.Property(x => x.ProductVariantBranchId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProductVariantId)
                .IsRequired();

            builder.Property(x => x.BranchId)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.IsIncluded)
                .IsRequired();

            builder.Property(x => x.Barcode)
                .HasMaxLength(100);

            builder.Property(x => x.Sku)
                .HasMaxLength(100);

            builder.Property(x => x.SupplierId);

            builder.Property(x => x.SupplierCode)
                .HasMaxLength(100);

            builder.Property(x => x.ReorderThreshold);

            builder.Property(x => x.ReorderQuantity);

            builder.Property(x => x.SupplierPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Property(x => x.TotalTaxPercentage)
                .HasPrecision(18, 2);

            builder.Property(x => x.TotalPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.IsArchived)
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .IsRequired();

            builder.Property(x => x.ModifiedOn);

            builder.Property(x => x.ModifiedBy);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x => x.ProductVariantBranches)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
