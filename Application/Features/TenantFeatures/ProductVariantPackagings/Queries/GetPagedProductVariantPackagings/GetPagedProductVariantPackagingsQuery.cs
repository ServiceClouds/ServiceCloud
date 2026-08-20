using Application.Abstractions.Queries;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;

namespace Application.Features.TenantFeatures.ProductVariantPackagings.Queries.GetPagedProductVariantPackagings;

public sealed record GetPagedProductVariantPackagingsQuery(
    PaginationRequest Request
) : IQuery<PagedResponse<ProductVariantPackaging>>;