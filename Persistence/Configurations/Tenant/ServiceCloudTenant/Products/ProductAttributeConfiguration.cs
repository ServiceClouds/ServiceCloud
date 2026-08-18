using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{



    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttribute");

            builder.HasKey(x => x.ProductAttributeId);

            builder.Property(x => x.ProductAttributeId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.EAttributeId)
                .IsRequired();

            builder.Property(x => x.SortOrder)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductAttributes)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ProductAttributeValues)
                .WithOne(x => x.ProductAttribute)
                .HasForeignKey(x => x.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}