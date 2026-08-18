using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Configurations.Tenant.ServiceCloudTenant.Products
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.ToTable("ProductAttributeValue");

            builder.HasKey(x => x.ProductAttributeValueId);

            builder.Property(x => x.ProductAttributeValueId)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ProductAttributeId)
                .IsRequired();

            builder.Property(x => x.AttributeValueId)
                .IsRequired();

            builder.HasOne(x => x.ProductAttribute)
                .WithMany(x => x.ProductAttributeValues)
                .HasForeignKey(x => x.ProductAttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
