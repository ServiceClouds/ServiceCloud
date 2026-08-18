using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{


    public class ProductVariantPackagingConfiguration : IEntityTypeConfiguration<ProductVariantPackaging>
    {
        public void Configure(EntityTypeBuilder<ProductVariantPackaging> builder)
        {
            builder.ToTable("ProductVariantPackaging");

            builder.HasKey(x => x.ProductVariantPackagingId);

            builder.Property(x => x.ProductVariantPackagingId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProductVariantId)
                .IsRequired();

            builder.Property(x => x.Weight)
                .HasPrecision(18, 2);

            builder.Property(x => x.WeightUnitId);

            builder.Property(x => x.DimensionUnitId);

            builder.Property(x => x.Length)
                .HasPrecision(18, 2);

            builder.Property(x => x.Width)
                .HasPrecision(18, 2);

            builder.Property(x => x.Height)
                .HasPrecision(18, 2);

            builder.Property(x => x.SizeVolume)
                .HasPrecision(18, 2);

            builder.Property(x => x.SizeVolumeUnitId);

            builder.HasOne(x => x.ProductVariant)
                .WithMany(x => x.ProductVariantPackagings)
                .HasForeignKey(x => x.ProductVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
