using System;
using System.Collections.Generic;

namespace Persistence.TempScaffold.Models;

public partial class ProductVariantBranch
{
    public long ProductVariantBranchId { get; set; }

    public long ProductVariantId { get; set; }

    public int BranchId { get; set; }

    public bool IsActive { get; set; }

    public bool IsIncluded { get; set; }

    public string? Barcode { get; set; }

    public string? Sku { get; set; }

    public int? SupplierId { get; set; }

    public string? SupplierCode { get; set; }

    public int? ReorderThreshold { get; set; }

    public int? ReorderQuantity { get; set; }

    public decimal? SupplierPrice { get; set; }

    public decimal Price { get; set; }

    public decimal TotalTaxPercentage { get; set; }

    public decimal TotalPrice { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
