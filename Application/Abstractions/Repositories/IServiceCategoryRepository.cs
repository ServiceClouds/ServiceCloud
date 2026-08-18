using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Repositories
{
    public interface IServiceCategoryRepository
    : IGenericRepository<ServiceCategory>
    {
        Task<Result<PagedResponse<ServiceCategory>>> GetPagedAsync(
            PaginationRequest request,
            CancellationToken cancellationToken = default);
    }
}