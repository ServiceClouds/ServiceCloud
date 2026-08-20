using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariants.Commands.UpdateProductVariant
{


    public sealed class UpdateProductVariantCommandHandler
        : ICommandHandler<UpdateProductVariantCommand, UpdateProductVariantResponse>
    {
        private readonly ITenantRepository<ProductVariant> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public UpdateProductVariantCommandHandler(
            ITenantRepository<ProductVariant> repository,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<Result<UpdateProductVariantResponse>> Handle(
            UpdateProductVariantCommand request,
            CancellationToken cancellationToken)
        {
            var productVariant =
                await _repository.FirstOrDefaultAsync(
                    x =>
                        x.ProductVariantId == request.ProductVariantId &&
                        !x.IsArchived,
                    cancellationToken: cancellationToken);

            if (productVariant is null)
            {
                return Result<UpdateProductVariantResponse>.Failure(
                    Error.NotFound("Product variant was not found."));
            }

            var duplicateVariant =
                await _repository.ExistsAsync(
                    x =>
                        x.ProductVariantId != request.ProductVariantId &&
                        x.ProductId == productVariant.ProductId &&
                        x.ProductVariantName == request.ProductVariantName &&
                        !x.IsArchived,
                    cancellationToken);

            if (duplicateVariant)
            {
                return Result<UpdateProductVariantResponse>.Failure(
                    Error.Conflict(
                        "Product variant name already exists."));
            }

            productVariant.Update(
                request.ProductVariantName,
                _userContext.StaffId,
                request.AttributeValueIds,
                request.IsStandard,
                request.SortedAttributeIds,
                request.SortedAttributeValueIds);

            _repository.Update(productVariant);

            var saveResult =
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<UpdateProductVariantResponse>
                    .Failure(saveResult.Error);
            }

            return Result<UpdateProductVariantResponse>.Success(
                new UpdateProductVariantResponse(
                    productVariant.ProductVariantId,
                    productVariant.ProductId,
                    productVariant.ProductVariantName));
        }
    }
}