using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Features.TenantFeatures.ProductAttributes.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributes.Queries.GetAllProductAttributes;

public sealed class GetAllProductAttributesQueryHandler
    : IQueryHandler<GetAllProductAttributesQuery, List<ProductAttributeResponse>>
{
    private readonly ITenantRepository<ProductAttribute> _repository;

    public GetAllProductAttributesQueryHandler(
        ITenantRepository<ProductAttribute> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ProductAttributeResponse>>> Handle(
        GetAllProductAttributesQuery request,
        CancellationToken cancellationToken)
    {
        var productAttributes =
            await _repository
                .GetAll()
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.ProductAttributeId)
                .Select(x => new ProductAttributeResponse(
                    x.ProductAttributeId,
                    x.ProductId,
                    x.EAttributeId,
                    x.SortOrder))
                .ToListAsync(cancellationToken);

        return Result<List<ProductAttributeResponse>>
            .Success(productAttributes);
    }
}
