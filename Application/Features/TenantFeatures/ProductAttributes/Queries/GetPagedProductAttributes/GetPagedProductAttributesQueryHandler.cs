using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Application.Features.TenantFeatures.ProductAttributes.Queries;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributes.Queries.GetPagedProductAttributes;

public sealed class GetPagedProductAttributesQueryHandler
    : IQueryHandler<GetPagedProductAttributesQuery, PagedResponse<ProductAttributeResponse>>
{
    private readonly ITenantRepository<ProductAttribute> _repository;

    public GetPagedProductAttributesQueryHandler(
        ITenantRepository<ProductAttribute> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<ProductAttributeResponse>>> Handle(
        GetPagedProductAttributesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();

        // ------------------------------------------------------------
        // Search
        // ------------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            var search = request.Request.Search.Trim();

            if (int.TryParse(search, out var searchId))
            {
                query = query.Where(x =>
                    x.ProductAttributeId == searchId ||
                    x.ProductId == searchId ||
                    x.EAttributeId == searchId);
            }
        }

        // ------------------------------------------------------------
        // Total Records
        // ------------------------------------------------------------

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // ------------------------------------------------------------
        // Sorting
        // ------------------------------------------------------------

        query = request.Request.SortDescending
            ? query.OrderByDescending(x => x.SortOrder)
            : query.OrderBy(x => x.SortOrder);

        // ------------------------------------------------------------
        // Paging
        // ------------------------------------------------------------

        var products =
            await query
                .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .Select(x => new ProductAttributeResponse(
                    x.ProductAttributeId,
                    x.ProductId,
                    x.EAttributeId,
                    x.SortOrder))
                .ToListAsync(cancellationToken);

        // ------------------------------------------------------------
        // Total Pages
        // ------------------------------------------------------------

        var totalPages =
            request.Request.PageSize > 0
                ? (int)Math.Ceiling(totalRecords / (double)request.Request.PageSize)
                : 0;

        var response = new PagedResponse<ProductAttributeResponse>
        {
            Items = products,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<ProductAttributeResponse>>
            .Success(response);
    }
}
