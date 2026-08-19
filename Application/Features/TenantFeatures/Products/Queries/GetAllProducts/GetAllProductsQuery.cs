using Application.Abstractions.Queries;

namespace Application.Features.TenantFeatures.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery
    : IQuery<List<ProductResponse>>;
