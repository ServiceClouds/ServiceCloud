using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetPagedProductAttributeValues;

public sealed class GetPagedProductAttributeValuesQueryHandler
    : IQueryHandler<GetPagedProductAttributeValuesQuery, PagedResponse<ProductAttributeValueResponse>>
{
    private readonly ITenantRepository<ProductAttributeValue> _repository;

    public GetPagedProductAttributeValuesQueryHandler(
        ITenantRepository<ProductAttributeValue> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<ProductAttributeValueResponse>>> Handle(
        GetPagedProductAttributeValuesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();

        if (!string.IsNullOrWhiteSpace(request.Request.Search) &&
            int.TryParse(request.Request.Search.Trim(), out var searchId))
        {
            query = query.Where(x =>
                x.ProductAttributeValueId == searchId ||
                x.ProductAttributeId == searchId ||
                x.AttributeValueId == searchId);
        }

        var totalRecords =
            await query.CountAsync(cancellationToken);

        var items =
            await query
                .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .Select(x => new ProductAttributeValueResponse(
                    x.ProductAttributeValueId,
                    x.ProductAttributeId,
                    x.AttributeValueId))
                .ToListAsync(cancellationToken);

        var totalPages =
            request.Request.PageSize > 0
                ? (int)Math.Ceiling(totalRecords / (double)request.Request.PageSize)
                : 0;

        var response = new PagedResponse<ProductAttributeValueResponse>
        {
            Items = items,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<ProductAttributeValueResponse>>
            .Success(response);
    }
}
