using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.CreateProductVariantBranch
{
    public sealed record CreateProductVariantBranchCommand(
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
 ) : ICommand<CreateProductVariantBranchResponse>;
}
