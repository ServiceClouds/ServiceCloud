using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.Countries.Queries.GetPagedCountries;

public sealed class GetPagedCountriesQueryHandler
    : IQueryHandler<
        GetPagedCountriesQuery,
        PagedResponse<Country>>
{
    private readonly ITenantRepository<Country> _repository;

    public GetPagedCountriesQueryHandler(
        ITenantRepository<Country> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<Country>>> Handle(
        GetPagedCountriesQuery request,
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
                x.CountryName.Contains(search)
                ||
                (x.CountryCode != null &&
                 x.CountryCode.Contains(search)));
        }

        // ------------------------------------------------------------
        // Count
        // ------------------------------------------------------------

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // ------------------------------------------------------------
        // Pagination
        // ------------------------------------------------------------

        var countries =
            await query
                .OrderBy(x => x.CountryId)
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

        var response = new PagedResponse<Country>
        {
            Items = countries,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<Country>>
            .Success(response);
    }
}