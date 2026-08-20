using Application.Abstractions.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;

namespace Application.Features.TenantFeatures.ProductVariantPackagings.Queries.GetProductVariantPackagingById;

public sealed record GetProductVariantPackagingByIdQuery(
    long ProductVariantPackagingId
) : IQuery<ProductVariantPackaging>;