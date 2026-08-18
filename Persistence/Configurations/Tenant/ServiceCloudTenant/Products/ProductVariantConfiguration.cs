using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{
  

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("ProductVariant");

        builder.HasKey(x => x.ProductVariantId);

        builder.Property(x => x.ProductVariantId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.ProductVariantName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.AttributeValueIds);

        builder.Property(x => x.IsStandard)
            .IsRequired();

        builder.Property(x => x.IsArchived)
            .IsRequired();

        builder.Property(x => x.CreatedOn)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.Property(x => x.ModifiedOn);

        builder.Property(x => x.ModifiedBy);

        builder.Property(x => x.SortedAttributeIds);

        builder.Property(x => x.SortedAttributeValueIds);

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductVariants)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ProductVariantBranches)
            .WithOne(x => x.ProductVariant)
            .HasForeignKey(x => x.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ProductVariantPackagings)
            .WithOne(x => x.ProductVariant)
            .HasForeignKey(x => x.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}}