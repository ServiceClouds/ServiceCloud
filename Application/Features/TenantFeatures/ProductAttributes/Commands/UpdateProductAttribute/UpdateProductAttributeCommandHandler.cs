using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Products;
using Shared.Response;

namespace Application.Features.TenantFeatures.ProductAttributes.Commands.UpdateProductAttribute;

public sealed class UpdateProductAttributeCommandHandler
    : ICommandHandler<UpdateProductAttributeCommand, UpdateProductAttributeResponse>
{
    private readonly ITenantRepository<ProductAttribute> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductAttributeCommandHandler(
        ITenantRepository<ProductAttribute> repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateProductAttributeResponse>> Handle(
        UpdateProductAttributeCommand request,
        CancellationToken cancellationToken)
    {
        // ------------------------------------------------------------
        // 1. Get Product Attribute
        // ------------------------------------------------------------

        var productAttribute =
            await _repository.FirstOrDefaultAsync(
                x => x.ProductAttributeId == request.ProductAttributeId,
                cancellationToken);

        if (productAttribute is null)
        {
            return Result<UpdateProductAttributeResponse>.Failure(
                Error.NotFound("Product attribute was not found."));
        }

        // ------------------------------------------------------------
        // 2. Check Duplicate Attribute
        // ------------------------------------------------------------

        var duplicateAttribute =
            await _repository.ExistsAsync(
                x =>
                    x.ProductAttributeId != request.ProductAttributeId &&
                    x.ProductId == productAttribute.ProductId &&
                    x.EAttributeId == request.EAttributeId,
                cancellationToken);

        if (duplicateAttribute)
        {
            return Result<UpdateProductAttributeResponse>.Failure(
                Error.Conflict("This attribute is already assigned to the product."));
        }

        // ------------------------------------------------------------
        // 3. Update Domain Entity
        // ------------------------------------------------------------

        productAttribute.Update(
            request.EAttributeId,
            request.SortOrder);

        // ------------------------------------------------------------
        // 4. Update Repository
        // ------------------------------------------------------------

        _repository.Update(productAttribute);

        // ------------------------------------------------------------
        // 5. Save
        // ------------------------------------------------------------

        var saveResult =
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<UpdateProductAttributeResponse>
                .Failure(saveResult.Error);
        }

        // ------------------------------------------------------------
        // 6. Response
        // ------------------------------------------------------------

        return Result<UpdateProductAttributeResponse>.Success(
            new UpdateProductAttributeResponse(
                productAttribute.ProductAttributeId,
                productAttribute.ProductId,
                productAttribute.EAttributeId,
                productAttribute.SortOrder));
    }
}
