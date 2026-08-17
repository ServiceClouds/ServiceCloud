using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.TempScaffold.Models;

namespace Persistence.TempScaffold.Context;

public partial class TempDbContext : DbContext
{
    public TempDbContext(DbContextOptions<TempDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

    public virtual DbSet<ProductBranchPermission> ProductBranchPermissions { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<ProductVariantBranch> ProductVariantBranches { get; set; }

    public virtual DbSet<ProductVariantPackaging> ProductVariantPackagings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDA8A3B8E7");

            entity.ToTable("Product");

            entity.HasIndex(e => e.BrandId, "IX_Product_BrandId");

            entity.HasIndex(e => e.CompanyId, "IX_Product_CompanyId");

            entity.HasIndex(e => e.CreatedOn, "IX_Product_CreatedOn");

            entity.HasIndex(e => e.IsActive, "IX_Product_IsActive");

            entity.HasIndex(e => e.IsArchived, "IX_Product_IsArchived");

            entity.HasIndex(e => e.ProductCategoryId, "IX_Product_ProductCategoryId");

            entity.HasIndex(e => e.ProductClassificationId, "IX_Product_ProductClassificationId");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1500);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => e.ProductAttributeId).HasName("PK__ProductA__00CE6727FE45D553");

            entity.ToTable("ProductAttribute");

            entity.HasIndex(e => e.ProductId, "IX_ProductAttribute_ProductID");

            entity.HasIndex(e => e.EAttributeId, "IX_ProductAttribute_eAttributeID");

            entity.Property(e => e.ProductAttributeId)
                .ValueGeneratedNever()
                .HasColumnName("ProductAttributeID");
            entity.Property(e => e.EAttributeId).HasColumnName("eAttributeID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductAt__Produ__2180FB33");
        });

        modelBuilder.Entity<ProductAttributeValue>(entity =>
        {
            entity.HasKey(e => e.ProductAttributeValueId).HasName("PK__ProductA__9FE4432170C2E70A");

            entity.ToTable("ProductAttributeValue");

            entity.HasIndex(e => e.AttributeValueId, "IX_ProductAttributeValue_AttributeValueID");

            entity.HasIndex(e => e.ProductAttributeId, "IX_ProductAttributeValue_ProductAttributeID");

            entity.Property(e => e.ProductAttributeValueId)
                .ValueGeneratedNever()
                .HasColumnName("ProductAttributeValueID");
            entity.Property(e => e.AttributeValueId).HasColumnName("AttributeValueID");
            entity.Property(e => e.ProductAttributeId).HasColumnName("ProductAttributeID");

            entity.HasOne(d => d.ProductAttribute).WithMany(p => p.ProductAttributeValues)
                .HasForeignKey(d => d.ProductAttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductAt__Produ__245D67DE");
        });

        modelBuilder.Entity<ProductBranchPermission>(entity =>
        {
            entity.HasKey(e => e.ProductBranchPermissionId).HasName("PK__ProductB__C23C77CAFA9905E8");

            entity.ToTable("ProductBranchPermission");

            entity.HasIndex(e => e.BranchId, "IX_ProductBranchPermission_BranchID");

            entity.HasIndex(e => e.IsActive, "IX_ProductBranchPermission_IsActive");

            entity.HasIndex(e => e.IsFeatured, "IX_ProductBranchPermission_IsFeatured");

            entity.HasIndex(e => e.IsOnline, "IX_ProductBranchPermission_IsOnline");

            entity.HasIndex(e => e.ProductId, "IX_ProductBranchPermission_ProductID");

            entity.Property(e => e.ProductBranchPermissionId)
                .ValueGeneratedNever()
                .HasColumnName("ProductBranchPermissionID");
            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductBranchPermissions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductBr__Produ__2739D489");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__ProductC__3224ECCEE7F57682");

            entity.ToTable("ProductCategory");

            entity.HasIndex(e => e.CompanyId, "IX_ProductCategory_CompanyId");

            entity.HasIndex(e => e.CreatedOn, "IX_ProductCategory_CreatedOn");

            entity.HasIndex(e => e.IsArchived, "IX_ProductCategory_IsArchived");

            entity.Property(e => e.ProductCategoryId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ImagePath).HasMaxLength(80);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.ProductCategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.HasKey(e => e.ProductVariantId).HasName("PK__ProductV__E4D667252221BDF2");

            entity.ToTable("ProductVariant");

            entity.HasIndex(e => e.IsArchived, "IX_ProductVariant_IsArchived");

            entity.HasIndex(e => e.IsStandard, "IX_ProductVariant_IsStandard");

            entity.HasIndex(e => e.ProductId, "IX_ProductVariant_ProductID");

            entity.Property(e => e.ProductVariantId)
                .ValueGeneratedNever()
                .HasColumnName("ProductVariantID");
            entity.Property(e => e.AttributeValueIds)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AttributeValueIDs");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductVariantName).HasMaxLength(200);
            entity.Property(e => e.SortedAttributeIds)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SortedAttributeIDs");
            entity.Property(e => e.SortedAttributeValueIds)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SortedAttributeValueIDs");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductVa__Produ__18EBB532");
        });

        modelBuilder.Entity<ProductVariantBranch>(entity =>
        {
            entity.HasKey(e => e.ProductVariantBranchId).HasName("PK__ProductV__D71BA004E276D5E1");

            entity.ToTable("ProductVariantBranch");

            entity.HasIndex(e => e.Barcode, "IX_ProductVariantBranch_Barcode");

            entity.HasIndex(e => e.BranchId, "IX_ProductVariantBranch_BranchID");

            entity.HasIndex(e => e.IsActive, "IX_ProductVariantBranch_IsActive");

            entity.HasIndex(e => e.IsArchived, "IX_ProductVariantBranch_IsArchived");

            entity.HasIndex(e => e.ProductVariantId, "IX_ProductVariantBranch_ProductVariantID");

            entity.HasIndex(e => e.Sku, "IX_ProductVariantBranch_SKU");

            entity.Property(e => e.ProductVariantBranchId)
                .ValueGeneratedNever()
                .HasColumnName("ProductVariantBranchID");
            entity.Property(e => e.Barcode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductVariantId).HasColumnName("ProductVariantID");
            entity.Property(e => e.Sku)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SKU");
            entity.Property(e => e.SupplierCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTaxPercentage).HasColumnType("decimal(18, 6)");

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.ProductVariantBranches)
                .HasForeignKey(d => d.ProductVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductVa__Produ__1BC821DD");
        });

        modelBuilder.Entity<ProductVariantPackaging>(entity =>
        {
            entity.HasKey(e => e.ProductVariantPackagingId).HasName("PK__ProductV__31DED11F25ABA0BE");

            entity.ToTable("ProductVariantPackaging");

            entity.HasIndex(e => e.ProductVariantId, "IX_ProductVariantPackaging_ProductVariantID");

            entity.Property(e => e.ProductVariantPackagingId)
                .ValueGeneratedNever()
                .HasColumnName("ProductVariantPackagingID");
            entity.Property(e => e.DimensionUnitId).HasColumnName("DimensionUnitID");
            entity.Property(e => e.Height).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Length).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductVariantId).HasColumnName("ProductVariantID");
            entity.Property(e => e.SizeVolume).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SizeVolumeUnitId).HasColumnName("SizeVolumeUnitID");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.WeightUnitId).HasColumnName("WeightUnitID");
            entity.Property(e => e.Width).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ProductVariant).WithMany(p => p.ProductVariantPackagings)
                .HasForeignKey(d => d.ProductVariantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductVa__Produ__1EA48E88");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
