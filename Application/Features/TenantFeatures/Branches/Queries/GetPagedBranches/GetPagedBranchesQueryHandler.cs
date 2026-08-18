using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.Branches.Queries.GetPagedBranches;

public sealed class GetPagedBranchesQueryHandler
    : IQueryHandler<
        GetPagedBranchesQuery,
        PagedResponse<Branch>>
{
    private readonly ITenantRepository<Branch> _repository;

    public GetPagedBranchesQueryHandler(
        ITenantRepository<Branch> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<Branch>>> Handle(
        GetPagedBranchesQuery request,
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
                (x.BranchName != null &&
                 x.BranchName.Contains(search))
                ||
                x.BranchCode.Contains(search)
                ||
                (x.CityName != null &&
                 x.CityName.Contains(search))
                ||
                (x.Email != null &&
                 x.Email.Contains(search))
                ||
                (x.Phone != null &&
                 x.Phone.Contains(search))
                ||
                (x.Mobile != null &&
                 x.Mobile.Contains(search)));
        }

        // ------------------------------------------------------------
        // Count
        // ------------------------------------------------------------

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // ------------------------------------------------------------
        // Pagination
        // ------------------------------------------------------------

        var branches =
            await query
                .OrderBy(x => x.BranchId)
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

        var response = new PagedResponse<Branch>
        {
            Items = branches,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<Branch>>
            .Success(response);
    }
}