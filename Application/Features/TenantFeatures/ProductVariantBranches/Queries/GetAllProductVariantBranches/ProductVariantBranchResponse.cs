using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetAllProductVariantBranches
{
    public sealed record ProductVariantBranchResponse(
     long ProductVariantBranchId,
     long ProductVariantId,
     int BranchId,
     bool IsActive,
     bool IsIncluded,
     string? Barcode,
     string? Sku,
     int? SupplierId,
     string? SupplierCode,
     int? ReorderThreshold,
     int? ReorderQuantity,
     decimal? SupplierPrice,
     decimal Price,
     decimal TotalTaxPercentage,
     decimal TotalPrice
 );
}
