using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranches
{
 

    public sealed class GetServiceCategoryBranchesQueryHandler
        : IQueryHandler<
            GetServiceCategoryBranchesQuery,
            GetServiceCategoryBranchesResponse>
    {
        private readonly ITenantRepository<ServiceCategoryBranch> _repository;

        public GetServiceCategoryBranchesQueryHandler(
            ITenantRepository<ServiceCategoryBranch> repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetServiceCategoryBranchesResponse>> Handle(
            GetServiceCategoryBranchesQuery request,
            CancellationToken cancellationToken)
        {
            var serviceCategoryBranches = await _repository
                .GetAll()
                .ToListAsync(cancellationToken);

            var response = serviceCategoryBranches
                .Select(x => new ServiceCategoryBranchItemResponse(
                    x.ServiceCategoryBranchId,
                    x.ServiceCategoryId,
                    x.BranchId,
                    x.IsActive,
                    x.IsIncluded))
                .ToList();

            return Result<GetServiceCategoryBranchesResponse>.Success(
                new GetServiceCategoryBranchesResponse(response));
        }
    }
}