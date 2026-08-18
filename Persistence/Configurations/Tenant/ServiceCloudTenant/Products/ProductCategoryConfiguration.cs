using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{


public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategory");

        builder.HasKey(x => x.ProductCategoryId);

        builder.Property(x => x.ProductCategoryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ProductCategoryName)
            .HasMaxLength(500);

        builder.Property(x => x.Description);

        builder.Property(x => x.ImagePath)
            .HasMaxLength(500);

        builder.Property(x => x.HasBranchPermission)
            .IsRequired();

        builder.Property(x => x.CreatedOn)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.Property(x => x.ModifiedOn);

        builder.Property(x => x.ModifiedBy);

        builder.Property(x => x.IsArchived)
            .IsRequired();

        builder.Property(x => x.CompanyId);

        builder.Property(x => x.AppSourceTypeId);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.ProductCategory)
            .HasForeignKey(x => x.ProductCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}}
