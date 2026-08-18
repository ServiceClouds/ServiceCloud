using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffBranches.Queries.GetPagedStaffBranches;

public sealed class GetPagedStaffBranchesQueryHandler
    : IQueryHandler<
        GetPagedStaffBranchesQuery,
        PagedResponse<StaffBranch>>
{
    private readonly ITenantRepository<StaffBranch> _repository;

    public GetPagedStaffBranchesQueryHandler(
        ITenantRepository<StaffBranch> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<StaffBranch>>> Handle(
        GetPagedStaffBranchesQuery request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Active StaffBranches only
        // ============================================================

        var query = _repository
            .GetAll()
            .Where(x => x.IsActive);

        // ============================================================
        // 2. Search
        // ============================================================

        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            var search = request.Request.Search.Trim();

            query = query.Where(x =>
                (x.OnlineDisplayName != null &&
                 x.OnlineDisplayName.Contains(search)));
        }

        // ============================================================
        // 3. Count
        // ============================================================

        var totalRecords =
            await query.CountAsync(cancellationToken);

        // ============================================================
        // 4. Pagination
        // ============================================================

        var staffBranches =
            await query
                .OrderBy(x => x.StaffBranchId)
                .Skip(
                    (request.Request.PageNumber - 1)
                    * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .ToListAsync(cancellationToken);

        // ============================================================
        // 5. Total Pages
        // ============================================================

        var totalPages =
            request.Request.PageSize > 0
                ? (int)Math.Ceiling(
                    (double)totalRecords /
                    request.Request.PageSize)
                : 0;

        // ============================================================
        // 6. Response
        // ============================================================

        var response =
            new PagedResponse<StaffBranch>
            {
                Items = staffBranches,
                PageNumber = request.Request.PageNumber,
                PageSize = request.Request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };

        return Result<PagedResponse<StaffBranch>>
            .Success(response);
    }
}