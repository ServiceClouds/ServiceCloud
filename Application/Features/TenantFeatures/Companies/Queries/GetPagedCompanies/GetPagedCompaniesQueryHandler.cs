using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.Companies.Queries.GetPagedCompanies;

public sealed class GetPagedCompaniesQueryHandler
    : IQueryHandler<
        GetPagedCompaniesQuery,
        PagedResponse<Company>>
{
    private readonly ITenantRepository<Company> _repository;

    public GetPagedCompaniesQueryHandler(
        ITenantRepository<Company> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<Company>>> Handle(
        GetPagedCompaniesQuery request,
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
                (x.CompanyName != null &&
                 x.CompanyName.Contains(search))
                ||
                x.CompanyCode.Contains(search)
                ||
                (x.Email != null &&
                 x.Email.Contains(search))
                ||
                (x.Phone != null &&
                 x.Phone.Contains(search)));
        }

        // ------------------------------------------------------------
        // Count
        // ------------------------------------------------------------

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // ------------------------------------------------------------
        // Pagination
        // ------------------------------------------------------------

        var companies =
            await query
                .OrderBy(x => x.CompanyId)
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

        var response = new PagedResponse<Company>
        {
            Items = companies,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<Company>>
            .Success(response);
    }
}