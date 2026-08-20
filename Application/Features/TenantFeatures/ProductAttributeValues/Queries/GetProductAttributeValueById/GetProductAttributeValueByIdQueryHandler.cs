using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetProductAttributeValueById;

public sealed class GetProductAttributeValueByIdQueryHandler
    : IQueryHandler<GetProductAttributeValueByIdQuery, ProductAttributeValueResponse>
{
    private readonly ITenantRepository<ProductAttributeValue> _repository;

    public GetProductAttributeValueByIdQueryHandler(
        ITenantRepository<ProductAttributeValue> repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductAttributeValueResponse>> Handle(
        GetProductAttributeValueByIdQuery request,
        CancellationToken cancellationToken)
    {
        var item =
            await _repository.FirstOrDefaultAsync(
                x => x.ProductAttributeValueId == request.ProductAttributeValueId,
                cancellationToken: cancellationToken);

        if (item is null)
        {
            return Result<ProductAttributeValueResponse>.Failure(
                Error.NotFound("Product attribute value was not found."));
        }

        return Result<ProductAttributeValueResponse>.Success(
            new ProductAttributeValueResponse(
                item.ProductAttributeValueId,
                item.ProductAttributeId,
                item.AttributeValueId));
    }
}
