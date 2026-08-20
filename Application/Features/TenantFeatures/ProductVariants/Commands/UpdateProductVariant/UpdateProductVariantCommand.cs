using Application.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.UpdateProductVariant
{
    public sealed record UpdateProductVariantCommand(
    long ProductVariantId,
    string ProductVariantName,
    string? AttributeValueIds,
    bool IsStandard,
    string? SortedAttributeIds,
    string? SortedAttributeValueIds
) : ICommand<UpdateProductVariantResponse>;
}
