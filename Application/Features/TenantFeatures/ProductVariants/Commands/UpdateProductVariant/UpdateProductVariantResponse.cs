using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.UpdateProductVariant
{
    public sealed record UpdateProductVariantResponse(
     long ProductVariantId,
     int ProductId,
     string ProductVariantName
 );
}
