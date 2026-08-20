using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetAllProductAttributeValues;

public sealed class GetAllProductAttributeValuesQueryHandler
    : IQueryHandler<GetAllProductAttributeValuesQuery, List<ProductAttributeValueResponse>>
{
    private readonly ITenantRepository<ProductAttributeValue> _repository;

    public GetAllProductAttributeValuesQueryHandler(
        ITenantRepository<ProductAttributeValue> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ProductAttributeValueResponse>>> Handle(
        GetAllProductAttributeValuesQuery request,
        CancellationToken cancellationToken)
    {
        var items =
            await _repository
                .GetAll()
                .Select(x => new ProductAttributeValueResponse(
                    x.ProductAttributeValueId,
                    x.ProductAttributeId,
                    x.AttributeValueId))
                .ToListAsync(cancellationToken);

        return Result<List<ProductAttributeValueResponse>>
            .Success(items);
    }
}
