using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.TenantFeatures.ProductVariantBranches.Commands.CreateProductVariantBranch;


public sealed class CreateProductVariantBranchCommandHandler
    : ICommandHandler<
        CreateProductVariantBranchCommand,
        CreateProductVariantBranchResponse>
{
    private readonly ITenantRepository<ProductVariantBranch> _repository;
    private readonly ITenantRepository<ProductVariant> _productVariantRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateProductVariantBranchCommandHandler(
        ITenantRepository<ProductVariantBranch> repository,
        ITenantRepository<ProductVariant> productVariantRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _productVariantRepository = productVariantRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateProductVariantBranchResponse>> Handle(
        CreateProductVariantBranchCommand request,
        CancellationToken cancellationToken)
    {
        var variantExists =
            await _productVariantRepository.ExistsAsync(
                x =>
                    x.ProductVariantId == request.ProductVariantId &&
                    !x.IsArchived,
                cancellationToken);

        if (!variantExists)
        {
            return Result<CreateProductVariantBranchResponse>.Failure(
                Error.NotFound("Product variant was not found."));
        }

        var branchExists =
            await _repository.ExistsAsync(
                x =>
                    x.ProductVariantId == request.ProductVariantId &&
                    x.BranchId == request.BranchId &&
                    !x.IsArchived,
                cancellationToken);

        if (branchExists)
        {
            return Result<CreateProductVariantBranchResponse>.Failure(
                Error.Conflict(
                    "Product variant is already assigned to this branch."));
        }

        var productVariantBranch =
            ProductVariantBranch.Create(
                request.ProductVariantId,
                request.BranchId,
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

        _repository.Add(productVariantBranch);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateProductVariantBranchResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateProductVariantBranchResponse>.Success(
            new CreateProductVariantBranchResponse(
                productVariantBranch.ProductVariantBranchId,
                productVariantBranch.ProductVariantId,
                productVariantBranch.BranchId));
    }
}