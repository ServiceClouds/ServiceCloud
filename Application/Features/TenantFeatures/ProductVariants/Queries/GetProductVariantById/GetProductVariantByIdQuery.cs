using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Queries.GetProductVariantById
{
    public sealed record GetProductVariantByIdQuery(
     long ProductVariantId
 ) : IRequest<Result<ProductVariantResponse>>;
}
