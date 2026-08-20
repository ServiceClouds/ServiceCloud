using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.UpdateProductVariantBranch
{
    public sealed record UpdateProductVariantBranchCommand(
     long ProductVariantBranchId,
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
 ) : ICommand<UpdateProductVariantBranchResponse>;
}
