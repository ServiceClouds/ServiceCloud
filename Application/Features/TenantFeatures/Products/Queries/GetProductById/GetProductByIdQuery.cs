using Application.Abstractions.Queries;

namespace Application.Features.TenantFeatures.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(
    int ProductId
) : IQuery<ProductResponse>;
