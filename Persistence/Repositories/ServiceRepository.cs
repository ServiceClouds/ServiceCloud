using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;


namespace Persistence.Repositories
{
    public  class ServiceRepository:IServiceRepository
    {
        private readonly LazyApplicationDbContext _lazyContext;

        public ServiceRepository(LazyApplicationDbContext lazyContext)
        {
            _lazyContext= lazyContext;
        }
        public async Task<Result> AddAsync(int companyId, Service service,
    CancellationToken cancellationToken = default)
        {
            var contextResult = await _lazyContext.GetAsync(companyId);
            if (contextResult.IsFailure)
            {
                return Result.Failure(contextResult.Error);
            }
            await contextResult.Value.Services.AddAsync(service, cancellationToken);

            return Result.Success();
        }
        public async Task<Result<Service>> GetByIdAsync(
    int companyId,
    int serviceId,
    CancellationToken cancellationToken = default)
        {
            var contextResult = await _lazyContext.GetAsync(companyId);

            if (contextResult.IsFailure)
            {
                return Result<Service>.Failure(contextResult.Error);
            }

            var service = await contextResult.Value.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.ServiceId == serviceId &&
                    !x.IsArchived,
                    cancellationToken);

            if (service is null)
            {
                return Result<Service>.Failure(
                    Error.NotFound("Service not found."));
            }

            return Result<Service>.Success(service);
        }
       public async Task<Result<bool>> ExistsAsync( int companyId,int serviceId,
    CancellationToken cancellationToken = default)
        {
            var contextResult = await _lazyContext.GetAsync(companyId);

            if (contextResult.IsFailure)
            {
                return Result<bool>.Failure(contextResult.Error);
            }

            var exists = await contextResult.Value.Services
                .AnyAsync(x =>
                    x.ServiceId == serviceId &&
                    !x.IsArchived,
                    cancellationToken);

            return Result<bool>.Success(exists);

        }
       public async Task<Result<bool>> CategoryExistsAsync(int companyId, int categoryId,
    CancellationToken cancellationToken = default)
        {
            var contextResult = await _lazyContext.GetAsync(companyId);

            if (contextResult.IsFailure)
            {
                return Result<bool>.Failure(contextResult.Error);
            }

            var exists = await contextResult.Value.ServiceCategories
                .AnyAsync(x =>
                    x.ServiceCategoryId == categoryId &&
                    !x.IsArchived,
                    cancellationToken);

            return Result<bool>.Success(exists);
        }



       public async  Task<Result> UpdateAsync(int companyId,Service service)
        {
            var contextresult=await _lazyContext.GetAsync(companyId);
            if (contextresult.IsFailure)
            {

                return Result.Failure(contextresult.Error);
            }
            contextresult.Value.Services.Update(service);
            return Result.Success();


        }
        public async Task<Result<PagedResponse<Service>>> GetPagedAsync(
    int companyId,
    PaginationRequest request,
    CancellationToken cancellationToken = default)
        {
            var contextResult = await _lazyContext.GetAsync(companyId);

            if (contextResult.IsFailure)
            {
                return Result<PagedResponse<Service>>.Failure(contextResult.Error);
            }

            var query = contextResult.Value.Services
                .AsNoTracking()
                .Where(x => !x.IsArchived);

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.ServiceName!.Contains(request.Search) || x.Description!.Contains(request.Search));
            }

            // Pagination
            var totalRecords = await query.CountAsync(cancellationToken);

            var services = await query .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var response = new PagedResponse<Service>
            {
                Items = services,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
            };

            return Result<PagedResponse<Service>>.Success(response);
        }

    }
}
