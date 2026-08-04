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

        Task<Result> AddAsync(int companyid, Service service, CancellationToken cancellationToken = default);
        Task<Result<Service?>> GetByIdAsync(int companyid, int serviceid, CancellationToken cancellationToken = default);

        Task<Result<PagedResponse<Service>>> GetPagedAsync(
    int companyId,
    PaginationRequest request,
    CancellationToken cancellationToken = default);
        Task<Result<bool>> ExistsAsync(
       int companyId,
       int serviceId,
       CancellationToken cancellationToken = default);

        Task<Result<bool>> CategoryExistsAsync(
            int companyId,
            int serviceCategoryId,
            CancellationToken cancellationToken = default);


        Task<Result> UpdateAsync(
     int companyId,
     Service service);


    }
}
