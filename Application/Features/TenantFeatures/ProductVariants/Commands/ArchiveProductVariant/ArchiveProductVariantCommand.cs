using System;
using System.Collections.Generic;
using System.Text;
using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.ArchiveProductVariant
{
    public sealed record ArchiveProductVariantCommand(
     long ProductVariantId
 ) : ICommand;
}
