using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
//using Domain.Entities.Tenant.ServiceCloudTenant.Products.ProductCategory;
using Shared.Response;

namespace Application.Features.TenantFeatures.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly ITenantRepository<Product> _repository;
    private readonly ITenantRepository<ProductCategory> _productCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateProductCommandHandler(
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

    public async Task<Result<CreateProductResponse>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Validate Category Exists
        // ------------------------------------------------------------

        var categoryExists =
            await _productCategoryRepository.ExistsAsync(
                x =>
                    x.ProductCategoryId == request.ProductCategoryId &&
                    !x.IsArchived,
                cancellationToken);

        if (!categoryExists)
        {
            return Result<CreateProductResponse>.Failure(
                Error.NotFound("Product category was not found."));
        }

        // ------------------------------------------------------------
        // 2. Check duplicate Product Name
        // ------------------------------------------------------------

        var productExists =
            await _repository.ExistsAsync(
                x =>
                    x.ProductName == request.ProductName &&
                    x.IsArchived!=true,
                cancellationToken);

        if (productExists)
        {
            return Result<CreateProductResponse>.Failure(
                Error.Conflict("Product name already exists."));
        }

        // ------------------------------------------------------------
        // 3. Create Domain Entity
        // ------------------------------------------------------------

        var product = Product.Create(
            request.ProductId,
            request.ProductCategoryId,
            _userContext.StaffId,
            request.ProductName,
            request.Description,
            request.IsActive,
            false,
            request.AllowBranchTrackInventory,
            request.HasBranchPermission,
            request.AllowBranchEditPrice,
            request.ProductClassificationId,
            request.BrandId,
            request.AppSourceTypeId,
            _userContext.CompanyId);

        // ------------------------------------------------------------
        // 4. Add Entity
        // ------------------------------------------------------------

        _repository.Add(product);

        // ------------------------------------------------------------
        // 5. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateProductResponse>
                .Failure(saveResult.Error);
        }

        // ------------------------------------------------------------
        // 6. Response
        // ------------------------------------------------------------

        return Result<CreateProductResponse>.Success(
            new CreateProductResponse(
                product.ProductId,
                product.ProductName));
    }
}
