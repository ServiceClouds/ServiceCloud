using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.Staff.Queries.GetPagedStaff;

public sealed class GetPagedStaffQueryHandler
    : IQueryHandler<
        GetPagedStaffQuery,
        PagedResponse<Domain.Tenant.ServiceCloudTenant.Entities.Staff>>
{
    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> _repository;

    public GetPagedStaffQueryHandler(
        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<Domain.Tenant.ServiceCloudTenant.Entities.Staff>>> Handle(
        GetPagedStaffQuery request,
        CancellationToken cancellationToken)
    {
        // ============================================================
        // 1. Base Query
        // ============================================================

        var query = _repository
            .GetAll()
            .Where(x => !x.IsArchived);

        // ============================================================
        // 2. Search
        // ============================================================

        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            var search =
                request.Request.Search.Trim();

            query = query.Where(x =>
                x.FirstName.Contains(search)
                ||
                (x.LastName != null &&
                 x.LastName.Contains(search))
                ||
                (x.FullName != null &&
                 x.FullName.Contains(search))
                ||
                x.Email.Contains(search)
                ||
                (x.Mobile != null &&
                 x.Mobile.Contains(search))
                ||
                (x.Phone != null &&
                 x.Phone.Contains(search))
                ||
                (x.CardNumber != null &&
                 x.CardNumber.Contains(search)));
        }

        // ============================================================
        // 3. Count
        // ============================================================

        var totalRecords =
            await query.CountAsync(
                cancellationToken);

        // ============================================================
        // 4. Pagination
        // ============================================================

        var staff =
            await query
                .OrderBy(x => x.StaffId)
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
            new PagedResponse<Domain.Tenant.ServiceCloudTenant.Entities.Staff>
            {
                Items = staff,
                PageNumber =
                    request.Request.PageNumber,
                PageSize =
                    request.Request.PageSize,
                TotalRecords =
                    totalRecords,
                TotalPages =
                    totalPages
            };

        return Result<PagedResponse<Domain.Tenant.ServiceCloudTenant.Entities.Staff>>
            .Success(response);
    }
}