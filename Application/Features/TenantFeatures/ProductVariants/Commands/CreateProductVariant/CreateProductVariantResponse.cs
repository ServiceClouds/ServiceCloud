using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.CreateProductVariant
{
    public sealed record CreateProductVariantResponse(
    long ProductVariantId,
    int ProductId,
    string ProductVariantName
);
}
