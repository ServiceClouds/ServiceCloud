using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;
using Shared.Response;

namespace Persistence.Repositories;

public sealed class ServiceRepository
    : GenericRepository<IApplicationDbContext, Service>,
      IServiceRepository
{
    public ServiceRepository(
        IApplicationDbContext context)
        : base(context)
    {
    }

    // ================================================================
    // SERVICE-SPECIFIC QUERY
    // ================================================================

    public async Task<Result<bool>> CategoryExistsAsync(
        int serviceCategoryId,
        CancellationToken cancellationToken = default)
    {
        var exists = await _context
            .GetDbSet<ServiceCategory>()
            .AsNoTracking()
            .AnyAsync(
                x =>
                    x.ServiceCategoryId == serviceCategoryId &&
                    !x.IsArchived,
                cancellationToken);

        return Result<bool>.Success(exists);
    }

    // ================================================================
    // SERVICE-SPECIFIC PAGED QUERY
    // ================================================================

    public async Task<Result<PagedResponse<Service>>> GetPagedAsync(
        PaginationRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = GetAll()
            .Where(x => !x.IsArchived);

        // ------------------------------------------------------------
        // Search
        // ------------------------------------------------------------

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x =>
                (x.ServiceName != null &&
                 x.ServiceName.Contains(request.Search))
                ||
                (x.Description != null &&
                 x.Description.Contains(request.Search)));
        }

        // ------------------------------------------------------------
        // Total records
        // ------------------------------------------------------------

        var totalRecords = await query
            .CountAsync(cancellationToken);

        // ------------------------------------------------------------
        // Pagination
        // ------------------------------------------------------------

        var services = await query
            .OrderBy(x => x.ServiceId)
            .Skip(
                (request.PageNumber - 1)
                * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        // ------------------------------------------------------------
        // Total pages
        // ------------------------------------------------------------

        var totalPages = request.PageSize > 0
            ? (int)Math.Ceiling(
                (double)totalRecords /
                request.PageSize)
            : 0;

        // ------------------------------------------------------------
        // Response
        // ------------------------------------------------------------

        var response = new PagedResponse<Service>
        {
            Items = services,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages
        };

        return Result<PagedResponse<Service>>
            .Success(response);
    }
}