using Application.Abstractions.Data;
using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;
using Shared.Response;

namespace Persistence.Repositories
{


    public sealed class ServiceCategoryRepository
        : GenericRepository<IApplicationDbContext, ServiceCategory>,
          IServiceCategoryRepository
    {
        public ServiceCategoryRepository(
            IApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<Result<PagedResponse<ServiceCategory>>> GetPagedAsync(
            PaginationRequest request,
            CancellationToken cancellationToken = default)
        {
            var query = GetAll()
                .Where(x => !x.IsArchived);

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    (x.ServiceCategoryName != null &&
                     x.ServiceCategoryName.Contains(request.Search))
                    ||
                    (x.Description != null &&
                     x.Description.Contains(request.Search)));
            }

            // Total records
            var totalRecords = await query
                .CountAsync(cancellationToken);

            // Pagination
            var categories = await query
                .OrderBy(x => x.ServiceCategoryId)
                .Skip(
                    (request.PageNumber - 1)
                    * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Total pages
            var totalPages = request.PageSize > 0
                ? (int)Math.Ceiling(
                    (double)totalRecords /
                    request.PageSize)
                : 0;

            var response = new PagedResponse<ServiceCategory>
            {
                Items = categories,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };

            return Result<PagedResponse<ServiceCategory>>
                .Success(response);
        }
    }
}