using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.Currencies.Queries.GetPagedCurrencies;

public sealed class GetPagedCurrenciesQueryHandler
    : IQueryHandler<
        GetPagedCurrenciesQuery,
        PagedResponse<Currency>>
{
    private readonly ITenantRepository<Currency> _repository;

    public GetPagedCurrenciesQueryHandler(
        ITenantRepository<Currency> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<Currency>>> Handle(
        GetPagedCurrenciesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository
            .GetAll()
            .Where(x => x.IsActive);

        // ------------------------------------------------------------
        // Search
        // ------------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            var search = request.Request.Search.Trim();

            query = query.Where(x =>
                x.CurrencyName.Contains(search)
                ||
                x.CurrencyCode.Contains(search));
        }

        // ------------------------------------------------------------
        // Count
        // ------------------------------------------------------------

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // ------------------------------------------------------------
        // Pagination
        // ------------------------------------------------------------

        var currencies =
            await query
                .OrderBy(x => x.CurrencyId)
                .Skip(
                    (request.Request.PageNumber - 1)
                    * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .ToListAsync(cancellationToken);

        // ------------------------------------------------------------
        // Total pages
        // ------------------------------------------------------------

        var totalPages =
            request.Request.PageSize > 0
                ? (int)Math.Ceiling(
                    (double)totalRecords /
                    request.Request.PageSize)
                : 0;

        // ------------------------------------------------------------
        // Response
        // ------------------------------------------------------------

        var response = new PagedResponse<Currency>
        {
            Items = currencies,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<Currency>>
            .Success(response);
    }
}