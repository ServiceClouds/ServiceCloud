using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.ArchiveProductVariantBranch
{


    public sealed class ArchiveProductVariantBranchCommandHandler
        : ICommandHandler<ArchiveProductVariantBranchCommand>
    {
        private readonly ITenantRepository<ProductVariantBranch> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public ArchiveProductVariantBranchCommandHandler(
            ITenantRepository<ProductVariantBranch> repository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result> Handle(
            ArchiveProductVariantBranchCommand request,
            CancellationToken cancellationToken)
        {
            var productVariantBranch =
                await _repository.FirstOrDefaultAsync(
                    x =>
                        x.ProductVariantBranchId ==
                            request.ProductVariantBranchId &&
                        !x.IsArchived,
                    cancellationToken: cancellationToken);

            if (productVariantBranch is null)
            {
                return Result.Failure(
                    Error.NotFound(
                        "Product variant branch was not found."));
            }

            productVariantBranch.Archive(_userContext.StaffId);

            _repository.Update(productVariantBranch);

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