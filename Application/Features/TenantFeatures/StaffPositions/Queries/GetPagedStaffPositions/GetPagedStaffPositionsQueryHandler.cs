using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffPositions.Queries.GetPagedStaffPositions;

public sealed class GetPagedStaffPositionsQueryHandler
    : IQueryHandler<
        GetPagedStaffPositionsQuery,
        PagedResponse<StaffPosition>>
{
    private readonly ITenantRepository<StaffPosition> _repository;

    public GetPagedStaffPositionsQueryHandler(
        ITenantRepository<StaffPosition> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedResponse<StaffPosition>>> Handle(
        GetPagedStaffPositionsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository
            .GetAll()
            .Where(x => x.IsActive);

        if (!string.IsNullOrWhiteSpace(request.Request.Search))
        {
            var search = request.Request.Search.Trim();

            query = query.Where(x =>
                x.PositionName.Contains(search));
        }

        var totalRecords =
            await query.CountAsync(cancellationToken);

        var staffPositions =
            await query
                .OrderBy(x => x.StaffPositionId)
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

        var response = new PagedResponse<StaffPosition>
        {
            Items = staffPositions,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<StaffPosition>>
            .Success(response);
    }
}