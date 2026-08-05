using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using Application.Common;

namespace Application.Abstractions.Repositories
{
    public interface  IServiceRepository
    {

        Task<Result> AddAsync( Service service, CancellationToken cancellationToken = default);
        Task<Result<Service?>> GetByIdAsync(int serviceid, CancellationToken cancellationToken = default);

        Task<Result<PagedResponse<Service>>> GetPagedAsync(
  
    PaginationRequest request,
    CancellationToken cancellationToken = default);
        Task<Result<bool>> ExistsAsync(
       int serviceId,
       CancellationToken cancellationToken = default);

        Task<Result<bool>> CategoryExistsAsync(
            int serviceCategoryId,
            CancellationToken cancellationToken = default);


        Task<Result> UpdateAsync(
     Service service);


    }
}
