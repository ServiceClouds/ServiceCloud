using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.ProductId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProductCategoryId)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .HasMaxLength(500);
            builder.Property(x => x.Description);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.CreatedOn)
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .IsRequired();

            builder.Property(x => x.ModifiedOn);

            builder.Property(x => x.ModifiedBy);

            builder.Property(x => x.IsArchived);

            builder.Property(x => x.AllowBranchTrackInventory)
                .IsRequired();

            builder.Property(x => x.HasBranchPermission)
                .IsRequired();

            builder.Property(x => x.AllowBranchEditPrice)
                .IsRequired();

            builder.Property(x => x.ProductClassificationId);

            builder.Property(x => x.BrandId);
            builder.Property(x => x.AppSourceTypeId);

            builder.Property(x => x.CompanyId);

            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ProductAttributes)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ProductVariants)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ProductBranchPermissions)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
