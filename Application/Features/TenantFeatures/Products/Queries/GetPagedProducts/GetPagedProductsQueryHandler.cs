using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.Products.Queries.GetPagedProducts;

public sealed class GetPagedProductsQueryHandler
    : IQueryHandler<GetPagedProductsQuery, PagedResponse<ProductResponse>>
{
    private readonly ITenantRepository<Product> _repository;

    public GetPagedProductsQueryHandler(
        ITenantRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<ProductResponse>>> Handle(
        GetPagedProductsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository
     .GetAll()
     .Where(x => x.IsArchived != true);

        // ------------------------------------------------------------
        // Search
        // ------------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            var search = request.Request.Search.Trim();

            query = query.Where(x =>
                (x.ProductName != null &&
                 x.ProductName.Contains(search))
                ||
                (x.Description != null &&
                 x.Description.Contains(search)));
        }

        // ------------------------------------------------------------
        // Total Records
        // ------------------------------------------------------------

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // ------------------------------------------------------------
        // Sorting
        // ------------------------------------------------------------

       

        // ------------------------------------------------------------
        // Pagination
        // ------------------------------------------------------------

        var products =
            await query
                .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .Select(x => new ProductResponse(
                    x.ProductId,
                    x.ProductCategoryId,
                    x.ProductName,
                    x.Description,
                    x.IsActive,
                    x.AllowBranchTrackInventory,
                    x.HasBranchPermission,
                    x.AllowBranchEditPrice,
                    x.ProductClassificationId,
                    x.BrandId,
                    x.AppSourceTypeId))
                .ToListAsync(cancellationToken);

        var totalPages =
            request.Request.PageSize > 0
                ? (int)Math.Ceiling((double)totalRecords / request.Request.PageSize)
                : 0;

        var response = new PagedResponse<ProductResponse>
        {
            Items = products,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<ProductResponse>>.Success(response);
    }
}
