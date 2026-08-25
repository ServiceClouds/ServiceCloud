using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.CreateProductVariant
{
    public sealed record CreateProductVariantCommand(
        int productvariantid,
    int ProductId,
    string ProductVariantName,
    string? AttributeValueIds,
    bool IsStandard,
    string? SortedAttributeIds,
    string? SortedAttributeValueIds
) : ICommand<CreateProductVariantResponse>;
}
