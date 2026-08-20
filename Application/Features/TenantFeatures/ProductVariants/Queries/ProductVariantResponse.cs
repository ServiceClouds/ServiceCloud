using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Queries
{
    public sealed record ProductVariantResponse(
    long ProductVariantId,
    int ProductId,
    string ProductVariantName,
    string? AttributeValueIds,
    bool IsStandard,
    bool IsArchived,
    DateTime CreatedOn,
    int CreatedBy,
    DateTime? ModifiedOn,
    int? ModifiedBy,
    string? SortedAttributeIds,
    string? SortedAttributeValueIds
);
}
