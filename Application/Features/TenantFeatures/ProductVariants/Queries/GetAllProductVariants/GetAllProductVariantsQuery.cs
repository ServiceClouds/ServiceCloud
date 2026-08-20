using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Queries.GetAllProductVariants
{
    public sealed record GetAllProductVariantsQuery
     : IRequest<Result<List<ProductVariantResponse>>>;
}
