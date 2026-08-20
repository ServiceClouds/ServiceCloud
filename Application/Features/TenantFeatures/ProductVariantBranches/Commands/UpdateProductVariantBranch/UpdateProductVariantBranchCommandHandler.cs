using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.UpdateProductVariantBranch;


public sealed class UpdateProductVariantBranchCommandHandler
    : ICommandHandler<
        UpdateProductVariantBranchCommand,
        UpdateProductVariantBranchResponse>
{
    private readonly ITenantRepository<ProductVariantBranch> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateProductVariantBranchCommandHandler(
        ITenantRepository<ProductVariantBranch> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<UpdateProductVariantBranchResponse>> Handle(
        UpdateProductVariantBranchCommand request,
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
            return Result<UpdateProductVariantBranchResponse>.Failure(
                Error.NotFound(
                    "Product variant branch was not found."));
        }

        productVariantBranch.Update(
            _userContext.StaffId,
            request.IsActive,
            request.IsIncluded,
            request.Barcode,
            request.Sku,
            request.SupplierId,
            request.SupplierCode,
            request.ReorderThreshold,
            request.ReorderQuantity,
            request.SupplierPrice,
            request.Price,
            request.TotalTaxPercentage,
            request.TotalPrice);

        _repository.Update(productVariantBranch);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<UpdateProductVariantBranchResponse>
                .Failure(saveResult.Error);
        }

        return Result<UpdateProductVariantBranchResponse>.Success(
            new UpdateProductVariantBranchResponse(
                productVariantBranch.ProductVariantBranchId,
                productVariantBranch.ProductVariantId,
                productVariantBranch.BranchId));
    }
}