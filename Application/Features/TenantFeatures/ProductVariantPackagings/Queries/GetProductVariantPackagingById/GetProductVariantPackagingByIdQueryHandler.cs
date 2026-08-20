using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductVariantPackagings.Queries.GetProductVariantPackagingById;

public sealed class GetProductVariantPackagingByIdQueryHandler
    : IQueryHandler<
        GetProductVariantPackagingByIdQuery,
        ProductVariantPackaging>
{
    private readonly ITenantRepository<ProductVariantPackaging> _repository;

    public GetProductVariantPackagingByIdQueryHandler(
        ITenantRepository<ProductVariantPackaging> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductVariantPackaging>> Handle(
        GetProductVariantPackagingByIdQuery request,
        CancellationToken cancellationToken)
    {
        var packaging =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.ProductVariantPackagingId ==
                    request.ProductVariantPackagingId,
                cancellationToken: cancellationToken);

        if (packaging is null)
        {
            return Result<ProductVariantPackaging>.Failure(
                Error.NotFound(
                    "Product variant packaging not found."));
        }

        return Result<ProductVariantPackaging>.Success(
            packaging);
    }
}