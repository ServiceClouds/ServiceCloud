using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributes.Queries.GetProductAttributeById;

public sealed class GetProductAttributeByIdQueryHandler
    : IQueryHandler<GetProductAttributeByIdQuery, ProductAttributeResponse>
{
    private readonly ITenantRepository<ProductAttribute> _repository;

    public GetProductAttributeByIdQueryHandler(
        ITenantRepository<ProductAttribute> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductAttributeResponse>> Handle(
        GetProductAttributeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var productAttribute =
            await _repository.FirstOrDefaultAsync(
                x => x.ProductAttributeId == request.ProductAttributeId,
                cancellationToken: cancellationToken);

        if (productAttribute is null)
        {
            return Result<ProductAttributeResponse>.Failure(
                Error.NotFound("Product attribute was not found."));
        }

        return Result<ProductAttributeResponse>.Success(
            new ProductAttributeResponse(
                productAttribute.ProductAttributeId,
                productAttribute.ProductId,
                productAttribute.EAttributeId,
                productAttribute.SortOrder));
    }
}
