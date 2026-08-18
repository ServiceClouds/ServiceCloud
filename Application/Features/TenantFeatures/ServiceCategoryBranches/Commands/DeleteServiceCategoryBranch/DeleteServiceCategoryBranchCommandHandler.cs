using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.ServiceEntities;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.DeleteServiceCategoryBranch

   
{
    public sealed class DeleteServiceCategoryBranchCommandHandler
        : ICommandHandler<DeleteServiceCategoryBranchCommand>
    {
        private readonly ITenantRepository<ServiceCategoryBranch> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceCategoryBranchCommandHandler(
            ITenantRepository<ServiceCategoryBranch> repository,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteServiceCategoryBranchCommand request,
            CancellationToken cancellationToken)
        {
            var serviceCategoryBranch = await _repository.FirstOrDefaultAsync(
                x => x.ServiceCategoryBranchId == request.ServiceCategoryBranchId,
                asNoTracking: false,
                cancellationToken: cancellationToken);

            if (serviceCategoryBranch is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "ServiceCategoryBranch.NotFound Service category branch not found."));
            }

            serviceCategoryBranch.SetActivationState(false);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result.Failure(saveResult.Error);
            }

            return Result.Success();
        }
    }
}