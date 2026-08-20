using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.StateCountries.Queries.GetPagedStateCountries;

public sealed class GetPagedStateCountriesQueryHandler
    : IQueryHandler<
        GetPagedStateCountriesQuery,
        PagedResponse<StateCountry>>
{
    private readonly ITenantRepository<StateCountry> _repository;

    public GetPagedStateCountriesQueryHandler(
        ITenantRepository<StateCountry> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<StateCountry>>> Handle(
        GetPagedStateCountriesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository
            .GetAll()
            .Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            var search = request.Request.Search.Trim();

            query = query.Where(x =>
                x.StateCountryName.Contains(search));
        }

        var totalRecords =
            await query.CountAsync(cancellationToken);

        var stateCountries =
            await query
                .OrderBy(x => x.StateCountryId)
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

        var response = new PagedResponse<StateCountry>
        {
            Items = stateCountries,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<StateCountry>>
            .Success(response);
    }
}