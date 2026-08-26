using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductBranchPermissions.Commands.CreateProductBranchPermission;

public sealed class CreateProductBranchPermissionCommandHandler
    : ICommandHandler<
        CreateProductBranchPermissionCommand,
        CreateProductBranchPermissionResponse>
{
    private readonly ITenantRepository<ProductBranchPermission> _repository;
    private readonly ITenantRepository<Product> _productRepository;
    private readonly ITenantRepository<Branch> _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductBranchPermissionCommandHandler(
        ITenantRepository<ProductBranchPermission> repository,
        ITenantRepository<Product> productRepository,
        ITenantRepository<Branch> branchRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _productRepository = productRepository;
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProductBranchPermissionResponse>> Handle(
        CreateProductBranchPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var productExists =
            await _productRepository.ExistsAsync(
                x =>
                    x.ProductId == request.ProductId &&
                    x.IsArchived != true,
                cancellationToken);

        if (!productExists)
        {
            return Result<CreateProductBranchPermissionResponse>.Failure(
                Error.NotFound("Product was not found."));
        }

        var branchExists =
            await _branchRepository.ExistsAsync(
                x => x.BranchId == request.BranchId,
                cancellationToken);

        if (!branchExists)
        {
            return Result<CreateProductBranchPermissionResponse>.Failure(
                Error.NotFound("Branch was not found."));
        }

        var exists =
            await _repository.ExistsAsync(
                x =>
                    x.ProductId == request.ProductId &&
                    x.BranchId == request.BranchId,
                cancellationToken);

        if (exists)
        {
            return Result<CreateProductBranchPermissionResponse>.Failure(
                Error.Conflict(
                    "Product branch permission already exists."));
        }

        var permission =
            ProductBranchPermission.Create(
                request.productBranchePrmissionId,
                request.ProductId,
                request.BranchId,
                request.IsActive,
                request.IsOnline,
                request.IsHidePriceOnline,
                request.IsFeatured,
                request.HasTrackingventory,
                request.HasShipping,
                request.IsIncluded,
                request.BusinessUseOnly,
                request.IsVariantGenerated,
                request.IsSharedPrivately);

        _repository.Add(permission);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateProductBranchPermissionResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateProductBranchPermissionResponse>.Success(
            new CreateProductBranchPermissionResponse(
                permission.ProductBranchPermissionId,
                permission.ProductId,
                permission.BranchId));
    }
} 