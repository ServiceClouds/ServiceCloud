using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductVariantPackagings.Queries.GetPagedProductVariantPackagings;

public sealed class GetPagedProductVariantPackagingsQueryHandler
    : IQueryHandler<
        GetPagedProductVariantPackagingsQuery,
        PagedResponse<ProductVariantPackaging>>
{
    private readonly ITenantRepository<ProductVariantPackaging> _repository;

    public GetPagedProductVariantPackagingsQueryHandler(
        ITenantRepository<ProductVariantPackaging> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<ProductVariantPackaging>>> Handle(
        GetPagedProductVariantPackagingsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();

        var totalRecords =
            await query.CountAsync(cancellationToken);

        var packaging =
            await query
                .OrderBy(x => x.ProductVariantPackagingId)
                .Skip(
                    (request.Request.PageNumber - 1)
                    * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .ToListAsync(cancellationToken);

        var totalPages =
            request.Request.PageSize > 0
                ? (int)Math.Ceiling(
                    (double)totalRecords /
                    request.Request.PageSize)
                : 0;

        var response = new PagedResponse<ProductVariantPackaging>
        {
            Items = packaging,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<ProductVariantPackaging>>
            .Success(response);
    }
}