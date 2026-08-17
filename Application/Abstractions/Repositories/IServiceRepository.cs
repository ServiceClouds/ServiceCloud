using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;

namespace Application.Abstractions.Repositories;

public interface IServiceRepository
    : IGenericRepository<Service>
{
    Task<Result<bool>> CategoryExistsAsync(
        int serviceCategoryId,
        CancellationToken cancellationToken = default);

    Task<Result<PagedResponse<Service>>> GetPagedAsync(
        PaginationRequest request,
        CancellationToken cancellationToken = default);
}