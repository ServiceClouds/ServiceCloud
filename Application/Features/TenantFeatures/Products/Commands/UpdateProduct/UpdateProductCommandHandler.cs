using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;

using Shared.Response;

namespace Application.Features.TenantFeatures.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly ITenantRepository<Product> _repository;
    private readonly ITenantRepository<ProductCategory> _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateProductCommandHandler(
        ITenantRepository<Product> repository,
        ITenantRepository<ProductCategory> productCategoryRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _productCategoryRepository = productCategoryRepository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<UpdateProductResponse>> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Get Product
        // ------------------------------------------------------------

        var product =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.ProductId == request.ProductId &&
                    x.IsArchived!=true,
                cancellationToken: cancellationToken);

        if (product is null)
        {
            return Result<UpdateProductResponse>.Failure(
                Error.NotFound("Product was not found."));
        }

        // ------------------------------------------------------------
        // 2. Validate Category Exists
        // ------------------------------------------------------------

        var categoryExists =
            await _productCategoryRepository.ExistsAsync(
                x =>
                    x.ProductCategoryId == request.ProductCategoryId &&
                    !x.IsArchived,
                cancellationToken);

        if (!categoryExists)
        {
            return Result<UpdateProductResponse>.Failure(
                Error.NotFound("Product category was not found."));
        }

        // ------------------------------------------------------------
        // 3. Check duplicate Product Name
        // ------------------------------------------------------------

        var duplicateProduct =
            await _repository.ExistsAsync(
                x =>
                    x.ProductId != request.ProductId &&
                    x.ProductName == request.ProductName &&
                    x.IsArchived!=true,
                cancellationToken);

        if (duplicateProduct)
        {
            return Result<UpdateProductResponse>.Failure(
                Error.Conflict("Product name already exists."));
        }

        // ------------------------------------------------------------
        // 4. Update Domain Entity
        // ------------------------------------------------------------

        product.Update(
            request.ProductCategoryId,
            _userContext.StaffId,
            request.ProductName,
            request.Description,
            request.IsActive,
            request.AllowBranchTrackInventory,
            request.HasBranchPermission,
            request.AllowBranchEditPrice,
            request.ProductClassificationId,
            request.BrandId,
            request.AppSourceTypeId,
            _userContext.CompanyId);

        // ------------------------------------------------------------
        // 5. Update Entity
        // ------------------------------------------------------------

        _repository.Update(product);

        // ------------------------------------------------------------
        // 6. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<UpdateProductResponse>
                .Failure(saveResult.Error);
        }

        // ------------------------------------------------------------
        // 7. Response
        // ------------------------------------------------------------

        return Result<UpdateProductResponse>.Success(
            new UpdateProductResponse(
                product.ProductId,
                product.ProductName));
    }
}
