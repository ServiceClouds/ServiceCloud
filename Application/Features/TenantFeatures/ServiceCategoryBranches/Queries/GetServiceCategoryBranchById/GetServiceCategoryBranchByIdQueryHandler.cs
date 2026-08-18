using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranchById

{
    public sealed class GetServiceCategoryBranchByIdQueryHandler
        : IQueryHandler<
            GetServiceCategoryBranchByIdQuery,
            GetServiceCategoryBranchByIdResponse>
    {
        private readonly ITenantRepository<ServiceCategoryBranch> _repository;

        public GetServiceCategoryBranchByIdQueryHandler(
            ITenantRepository<ServiceCategoryBranch> repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetServiceCategoryBranchByIdResponse>> Handle(
            GetServiceCategoryBranchByIdQuery request,
            CancellationToken cancellationToken)
        {
            var serviceCategoryBranch = await _repository.FirstOrDefaultAsync(
                x => x.ServiceCategoryBranchId == request.ServiceCategoryBranchId,
                cancellationToken: cancellationToken);

            if (serviceCategoryBranch is null)
            {
                return Result<GetServiceCategoryBranchByIdResponse>.Failure(
                    Error.NotFound(
                        "ServiceCategoryBranch.NotFound Service category branch not found."));
            }

            return Result<GetServiceCategoryBranchByIdResponse>.Success(
                new GetServiceCategoryBranchByIdResponse(
                    serviceCategoryBranch.ServiceCategoryBranchId,
                    serviceCategoryBranch.ServiceCategoryId,
                    serviceCategoryBranch.BranchId,
                    serviceCategoryBranch.IsActive,
                    serviceCategoryBranch.IsIncluded));
        }
    }
}
