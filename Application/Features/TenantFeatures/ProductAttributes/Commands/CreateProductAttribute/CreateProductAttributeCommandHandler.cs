using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributes.Commands.CreateProductAttribute;

public sealed class CreateProductAttributeCommandHandler
    : ICommandHandler<CreateProductAttributeCommand, CreateProductAttributeResponse>
{
    private readonly ITenantRepository<ProductAttribute> _repository;
    private readonly ITenantRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductAttributeCommandHandler(
        ITenantRepository<ProductAttribute> repository,
        ITenantRepository<Product> productRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateProductAttributeResponse>> Handle(
        CreateProductAttributeCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Validate Product Exists
        // ------------------------------------------------------------

        var productExists =
            await _productRepository.ExistsAsync(
                x =>
                    x.ProductId == request.ProductId &&
                    x.IsArchived!=true,
                cancellationToken);

        if (!productExists)
        {
            return Result<CreateProductAttributeResponse>.Failure(
                Error.NotFound("Product was not found."));
        }

        // ------------------------------------------------------------
        // 2. Check Duplicate Product Attribute
        // ------------------------------------------------------------

        var attributeExists =
            await _repository.ExistsAsync(
                x =>
                    x.ProductId == request.ProductId &&
                    x.EAttributeId == request.EAttributeId,
                cancellationToken);

        if (attributeExists)
        {
            return Result<CreateProductAttributeResponse>.Failure(
                Error.Conflict("This attribute is already assigned to the product."));
        }

        // ------------------------------------------------------------
        // 3. Create Domain Entity
        // ------------------------------------------------------------

        var productAttribute = ProductAttribute.Create(
            request.productAttributeId,
            request.ProductId,
            request.EAttributeId,
            request.SortOrder);

        // ------------------------------------------------------------
        // 4. Add Entity
        // ------------------------------------------------------------

        _repository.Add(productAttribute);

        // ------------------------------------------------------------
        // 5. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateProductAttributeResponse>
                .Failure(saveResult.Error);
        }

        // ------------------------------------------------------------
        // 6. Response
        // ------------------------------------------------------------

        return Result<CreateProductAttributeResponse>.Success(
            new CreateProductAttributeResponse(
                productAttribute.ProductAttributeId,
                productAttribute.ProductId,
                productAttribute.EAttributeId,
                productAttribute.SortOrder));
    }
}
